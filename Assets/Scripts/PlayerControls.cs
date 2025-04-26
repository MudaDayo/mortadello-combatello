using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;


    [SerializeField] private float GroundCheckDistance = 0.1f;
    public bool isGrounded = false;

    private Vector2 moveInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
        }

        private void Movement(){



            // Apply gravity
            if(!IsGrounded())
            {
                transform.Translate(new Vector3(0, gravity, 0) * Time.deltaTime);
            }

            Vector3 move = new Vector3(moveInput.x, 0, 0) * moveSpeed * Time.deltaTime;

            if (IsGrounded() && moveInput.y > 0)
            {
                move += new Vector3(0, jumpForce, 0) * Time.deltaTime;
            }

        transform.Translate(move, Space.World);
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, GroundCheckDistance);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * GroundCheckDistance);
        }
}
