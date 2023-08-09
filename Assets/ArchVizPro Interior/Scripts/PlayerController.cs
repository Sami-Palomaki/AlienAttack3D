using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    PlayerInput Controller;
    public GameObject TextObject;
    public GameObject Portal;
    private Animator DoorAnim;
    public GameObject SecurityLight;
    public GameObject Cinema;
    float zMove;
    float xMove;
    bool CanTelaport = false;
    public bool toDayTime;
    public float TurnSpeed;
    public float MoveSpeed;
    float TurnRate;
    public float RayDistance;
    public LayerMask Hitlayer;
    float LooKUpRate;
    CharacterController ctrl;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(Cinema, 90);

        Controller = new PlayerInput();
        Controller.GamePlay.MoveForward.performed += ctx => zMove = ctx.ReadValue<float>();
        Controller.GamePlay.MoveForward.canceled += ctx => zMove = 0.0f;

        Controller.GamePlay.MoveRight.performed += ctx => xMove = ctx.ReadValue<float>();
        Controller.GamePlay.MoveRight.canceled += ctx => xMove = 0.0f;

        Controller.GamePlay.Turn.performed += ctx => TurnRate = ctx.ReadValue<float>();
        Controller.GamePlay.Turn.canceled += ctx => TurnRate = 0.0f;

        Controller.GamePlay.LookUp.performed += ctx => LooKUpRate = ctx.ReadValue<float>();
        Controller.GamePlay.LookUp.canceled += ctx => LooKUpRate = 0.0f;

        Controller.GamePlay.Exit.performed += ctx => Quit();
        Controller.GamePlay.Telaport.performed += ctx => Telaport();
    }

    void Start()
    {
        ctrl = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 yRot = new Vector3(.0f, TurnRate * TurnSpeed, .0f);
        Vector3 xRot = new Vector3(LooKUpRate * TurnSpeed, .0f, .0f);

        Camera.main.transform.Rotate(-xRot * Time.deltaTime);
        transform.Rotate(yRot * Time.deltaTime);

        Vector3 _zMove = transform.forward * zMove;
        Vector3 _xMove = transform.right * xMove;

        Vector3 Move = (_zMove + _xMove)* MoveSpeed * Time.deltaTime;
        ctrl.Move(Move);

        Ray ray = Camera.main.ScreenPointToRay(transform.forward);

        if(Physics.Raycast(ray,RayDistance,Hitlayer))
        {
            Debug.DrawRay(Camera.main.transform.position, transform.forward * RayDistance, Color.yellow);
            CanTelaport = true;
            TextObject.GetComponent<UnityEngine.UI.Text>().color = Color.red;
        }
        else
        {
            CanTelaport = false;
            TextObject.GetComponent<UnityEngine.UI.Text>().color = Color.white;
        }

        if(Vector3.Distance(this.transform.position,Portal.transform.position)<10)
        {
            Portal.transform.LookAt(this.transform.position);
        }

    }

    void Telaport()
    {
        if(CanTelaport)
        {
            StartCoroutine(Load());
        }
    }
    IEnumerator Load()
    {
        AsyncOperation operation = toDayTime?
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0):
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    void Quit()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void OnEnable()
    {
        Controller.GamePlay.Enable();
    }
    public void OnDisable()
    {
        Controller.GamePlay.Disable();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            if(toDayTime)
            {
                SecurityLight.SetActive(true);
            }
            GameObject Door = other.gameObject;
            DoorAnim = Door.GetComponent<Animator>();
            DoorAnim.SetBool("Open", true);
            //Door.GetComponent<UnityEngine.UI.Text>().enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag=="Door")
        {
            if(toDayTime)
            {
                SecurityLight.SetActive(false);
            }
            GameObject Door = other.gameObject;
            DoorAnim = Door.GetComponent<Animator>();
            DoorAnim.SetBool("Open", false);

        }
    }
}
