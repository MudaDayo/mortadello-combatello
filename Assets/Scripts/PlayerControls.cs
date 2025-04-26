using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform body;

    [SerializeField] private Animator animator;


    [SerializeField] private float groundCheckDistance = 0.1f;
    public bool isGrounded = false;

    private Vector2 moveInput;
    [SerializeField]private bool emoteInput, attackInput = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    public void OnEmote(InputAction.CallbackContext context)
    {
        emoteInput = context.performed;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        attackInput = context.performed;
    }


    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
        Emote();
    }

    private void Attack(){
        if (attackInput)
        {
            animator.SetBool("attacking", true);
        }
        else
        {
            animator.SetBool("attacking", false);
    }}
    private void Emote(){
        if (emoteInput)
        {
            animator.SetBool("emoting", true);
        }else
        {
            animator.SetBool("emoting", false);
        }}

        private void Movement(){
            // Apply gravity
            if(!IsGrounded())
            {
                rb.AddForce(new Vector3(0, -gravity, 0), ForceMode.Acceleration);
            }

            Vector3 move = new Vector3(moveInput.x, 0, 0) * moveSpeed * Time.deltaTime;

            //jump
            if (IsGrounded() && moveInput.y > 0.3f)
            {
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }

        transform.Translate(move, Space.World);
        Debug.Log(move);
        if(move.x > 0 || move.x < 0)
            {
                animator.SetBool("moving", true);
            }
            else
            {
                animator.SetBool("moving", false);
            }
        }

        private bool IsGrounded()
        {
            if (body.position.y <= groundCheckDistance)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            animator.SetBool("grounded", isGrounded);
            return isGrounded;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
        }
}
