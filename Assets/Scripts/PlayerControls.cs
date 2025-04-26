using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float attackForce = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Rigidbody rb;

    private PlayerManager playerManager;

    [SerializeField] private Transform body;

    [SerializeField] private Animator animator;

    [SerializeField] private float attackHitboxCooldown = 1f;

    [SerializeField] private float groundCheckDistance = 0.1f;
    public bool isGrounded = true;

    private Vector3 attackDirection;
    private Vector2 moveInput;
    [SerializeField]private bool emoteInput, attackInput, attacking = false;

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

    void Start()
    {
        attackHitboxCooldown = 0f;
        attackPrefab = gameObject.CompareTag("P1") ? attackPrefabP1 : attackPrefabP2; // Set the attack prefab based on the player's tag

        playerManager = FindFirstObjectByType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
        Emote();
        isGrounded = IsGrounded();
    }

    [SerializeField] private GameObject attackPrefabP1;
    [SerializeField] private GameObject attackPrefabP2;
    private GameObject attackPrefab;
    [SerializeField] private Transform attackSpawnPoint;

    private void Attack()
    {
        if (attackInput && attackHitboxCooldown <= 0f)
        {
            attackDirection = new Vector3(transform.localScale.x / (moveInput.y + 0.01f), moveInput.y, 0f); // Get the attack direction based on movement input

            Instantiate(attackPrefab, attackSpawnPoint.position, attackSpawnPoint.rotation); // Instantiate the prefab
            attackHitboxCooldown = 1f; // Reset cooldown

            attacking = true;
            animator.SetBool("attacking", true);
        }
        else if (attackHitboxCooldown > 0f)
        {
            attackHitboxCooldown -= Time.deltaTime; // Decrease cooldown over time
        }
        else
        {
            attacking = false;
            animator.SetBool("attacking", false);
        }

        if (attacking){
            Instantiate(attackPrefab, attackSpawnPoint.position, attackSpawnPoint.rotation); // Instantiate the prefab
            transform.Translate(new Vector3(transform.localScale.x * attackForce * Time.deltaTime, 0f, 0f), Space.World); // Move the player in the attack direction
        }
    }

    private void Emote(){
        if(attacking){return;}
        if (emoteInput)
        {
            animator.SetBool("emoting", true);
        }else
        {
            animator.SetBool("emoting", false);
        }}

        private void Movement(){
            if (attacking){return;}

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
        //Debug.Log(move);
        if(move.x > 0 || move.x < 0)
            {
                animator.SetBool("moving", true);
            }
            else
            {
                animator.SetBool("moving", false);
            }

            if(move.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if(move.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
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
            animator.SetBool("isInAir", !isGrounded);
            return isGrounded;
        }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with: " + other.gameObject.name);

        if (gameObject.CompareTag("P1") && other.CompareTag("Attack Hitbox P2"))
        {
            playerManager.player1Health -= 1f; // Decrease player 1's health by 10

            // Apply explosion force
            Vector3 forceDirection = (transform.position - other.transform.position).normalized;
            rb.AddForce(forceDirection * attackForce, ForceMode.Impulse);
        }
        else if (gameObject.CompareTag("P2") && other.CompareTag("Attack Hitbox P1"))
        {
            playerManager.player2Health -= 1f; // Decrease player 2's health by 10

            // Apply explosion force
            Vector3 forceDirection = (transform.position - other.transform.position).normalized;
            rb.AddForce(forceDirection * attackForce, ForceMode.Impulse);
        }
    }
}
