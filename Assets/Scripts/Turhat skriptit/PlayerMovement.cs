// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class PlayerMovement : MonoBehaviour
// {
//     [Header("Movement")]
//     public float moveSpeed;

//     public float groundDrag;

//     public float jumpForce;
//     public float jumpCooldown;
//     public float airMultiplier;
//     bool readyToJump;

//     [HideInInspector] public float walkSpeed;
//     [HideInInspector] public float sprintSpeed;

//     [Header("Keybinds")]
//     public KeyCode jumpKey = KeyCode.Space;

//     [Header("Ground Check")]
//     public float playerHeight;
//     public LayerMask whatIsGround;
//     bool grounded;

//     public Transform orientation;
    
//     Animator anim;
//     float horizontalInput;
//     float verticalInput;
//     float forwardAmount;
//     float turnAmount;

//     Vector3 moveDirection;

//     Rigidbody rb;

//     [HideInInspector] public TextMeshProUGUI text_speed;

//     private void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         rb.freezeRotation = true;
//         SetupAnimator();
//         readyToJump = true;
//     }

//     private void Update()
//     {
//         // ground check
//         grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

//         MyInput();
//         SpeedControl();
//         UpdateAnimator();

//         // Käännä pelaajan suunta hiiren osoittamaan suuntaan
//         Plane groundPlane = new Plane(Vector3.up, transform.position); // Oletetaan maan olevan tasainen
//         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//         float rayDistance;

//         if (groundPlane.Raycast(ray, out rayDistance))
//         {
//             Vector3 point = ray.GetPoint(rayDistance);
//             orientation.LookAt(point);
//         }

//         // handle drag
//         if (grounded)
//             rb.drag = groundDrag;
//         else
//             rb.drag = 0;
//     }

//     private void FixedUpdate()
//     {
//         MovePlayer();
//     }

//     private void MyInput()
//     {
//         horizontalInput = Input.GetAxisRaw("Horizontal");
//         verticalInput = Input.GetAxisRaw("Vertical");

//         // when to jump
//         if(Input.GetKey(jumpKey) && readyToJump && grounded)
//         {
//             readyToJump = false;

//             Jump();

//             Invoke(nameof(ResetJump), jumpCooldown);
//         }
//     }

//     private void MovePlayer()
//     {
//         // calculate movement direction
//         moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

//         // on ground
//         if(grounded)
//             rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

//         // in air
//         else if(!grounded)
//             rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        
        
//     }

//     void UpdateAnimator()
//     {
//         anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
//         anim.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
//     }

//     private void SpeedControl()
//     {
//         Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

//         // limit velocity if needed
//         if(flatVel.magnitude > moveSpeed)
//         {
//             Vector3 limitedVel = flatVel.normalized * moveSpeed;
//             rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
//         }

//         // text_speed.SetText("Speed: " + flatVel.magnitude);
//     }

//     private void Jump()
//     {
//         // reset y velocity
//         rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

//         rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
//     }
//     private void ResetJump()
//     {
//         readyToJump = true;
//     }

//     void SetupAnimator()
//     {
//         anim = GetComponent<Animator>();

//         foreach (var childAnimator in GetComponentsInChildren<Animator>())
//         {
//             if (childAnimator != anim)
//             {
//                 anim.avatar = childAnimator.avatar;
//                 Destroy(childAnimator);
//                 break;
//             }
//         }

//     }
// }