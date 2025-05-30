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
    private bool wasGrounded = true;

    private Vector3 attackDirection;
    private Vector2 moveInput;
    [SerializeField] private bool emoteInput, attackInput, attacking = false;
    
    // VFX 
    [SerializeField] private GameObject attackVFXPrefab;
    [SerializeField] private GameObject emoteVFXPrefab;
    [SerializeField] private GameObject hitVFXPrefab;
    [SerializeField] private GameObject jumpVFXPrefab;
    [SerializeField] private GameObject landVFXPrefab;
    [SerializeField] private GameObject deathVFXPrefab_Skull;
    [SerializeField] private GameObject deathVFXPrefab_Explosion;
    
    // audio
    [SerializeField] private AudioClip jumpAudio;
    [SerializeField] private AudioClip attackAudio;
    [SerializeField] private AudioClip emoteAudio;
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private AudioClip landAudio;
    [SerializeField] private AudioClip deathAudio;
    
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        attackPrefab =
            gameObject.CompareTag("P1")
                ? attackPrefabP1
                : attackPrefabP2; // Set the attack prefab based on the player's tag

        playerManager = FindFirstObjectByType<PlayerManager>();

        playerManager.player1 = gameObject.CompareTag("P1") ? gameObject : playerManager.player1;
        playerManager.player2 = gameObject.CompareTag("P2") ? gameObject : playerManager.player2;
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
            attackDirection =
                new Vector3(transform.localScale.x / (moveInput.y + 0.01f), moveInput.y,
                    0f); // Get the attack direction based on movement input

            Instantiate(attackPrefab, attackSpawnPoint.position, attackSpawnPoint.rotation); // Instantiate the prefab
            attackHitboxCooldown = 1f; // Reset cooldown

            attacking = true;
            animator.SetBool("attacking", true);
            
            // Play attack audio
            if (attackAudio != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(attackAudio);
            }
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

        if (attacking)
        {
            Instantiate(attackPrefab, attackSpawnPoint.position, attackSpawnPoint.rotation); // Instantiate the prefab
            transform.Translate(new Vector3(transform.localScale.x * attackForce * Time.deltaTime, 0f, 0f),
                Space.World); // Move the player in the attack direction
            
            // attack vfx
            Instantiate(attackVFXPrefab, attackSpawnPoint.position, attackSpawnPoint.rotation);
        }
    }

    private void Emote()
    {
        if (attacking)
        {
            return;
        }

        if (emoteInput)
        {
            animator.SetBool("emoting", true);
            
            // emote vfx
           Instantiate(emoteVFXPrefab, transform.position, Quaternion.identity);
           
            // Play emote audio
            if (emoteAudio != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(emoteAudio);
            }
        }
        else
        {
            animator.SetBool("emoting", false);
        }
    }

    private void Movement()
    {
        if (attacking)
        {
            return;
        }

        // Apply gravity
        if (!IsGrounded())
        {
            rb.AddForce(new Vector3(0, -gravity, 0), ForceMode.Acceleration);
        }

        Vector3 move = new Vector3(moveInput.x, 0, 0) * moveSpeed * Time.deltaTime;

        //jump
        if (IsGrounded() && moveInput.y > 0.3f)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            
            // jump vfx
            Instantiate(jumpVFXPrefab, transform.position, Quaternion.identity);
            
            // Play jump audio
            if (jumpAudio != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(jumpAudio);
            }
        }

        transform.Translate(move, Space.World);
        //Debug.Log(move);
        if (move.x > 0 || move.x < 0)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        if (move.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private bool IsGrounded()
    {
        bool currentlyGrounded = body.position.y <= groundCheckDistance;

        if (!wasGrounded && currentlyGrounded)
        {
            // Play landing VFX
            Instantiate(landVFXPrefab, transform.position, Quaternion.identity);
            
            // Play landing audio
            // landAudio.Play();
        }
        wasGrounded = currentlyGrounded;

        animator.SetBool("isInAir", !currentlyGrounded);
        return currentlyGrounded;
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
            
            // hit vfx
            // offset location, a bit above the player and in front of the player
            Vector3 offset = new Vector3(0, 2, -1);
            Instantiate(hitVFXPrefab, transform.position + offset, Quaternion.identity);
            
            // Play hit audio
            if (hitAudio != null)
            {
                audioSource.PlayOneShot(hitAudio);
            }
            
            // i know this is not the best way to do this, but i am lazy
            if (playerManager.player1Health <= 0)
            {
                playerManager.OnPlayerDeath("P1");
                
                // Play death VFX
                Instantiate(deathVFXPrefab_Skull, transform.position + new Vector3(0, 1.5f, -2), Quaternion.identity);
                Instantiate(deathVFXPrefab_Explosion, transform.position, Quaternion.identity);
                
                // Play death audio
                if (deathAudio != null && !audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(deathAudio);
                }
            }
        }
        else if (gameObject.CompareTag("P2") && other.CompareTag("Attack Hitbox P1"))
        {
            playerManager.player2Health -= 1f; // Decrease player 2's health by 10

            // Apply explosion force
            Vector3 forceDirection = (transform.position - other.transform.position).normalized;
            rb.AddForce(forceDirection * attackForce, ForceMode.Impulse);
            
            // hit vfx
            // offset location, a bit above the player and in front of the player
            Vector3 offset = new Vector3(0, 2, -1);
            Instantiate(hitVFXPrefab, transform.position + offset, Quaternion.identity);
            
            // Play hit audio
            if (hitAudio != null) 
            {
                audioSource.PlayOneShot(hitAudio);
            }
            
            if (playerManager.player2Health <= 0)
            {
                playerManager.OnPlayerDeath("P2");
                
                // Play death VFX
                Instantiate(deathVFXPrefab_Skull, transform.position + new Vector3(0, 1.5f, -2), Quaternion.identity);
                Instantiate(deathVFXPrefab_Explosion, transform.position, Quaternion.identity);
                
                // Play death audio
                if (deathAudio != null)
                {
                    audioSource.PlayOneShot(deathAudio);
                }
            }
        }
    }
}