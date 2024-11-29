using UnityEngine;
using UnityEngine.UI;

public class DinoPlayerController : MonoBehaviour
{
    public AudioSource backgroundMusicSource; // Assign the background music AudioSource in the Inspector
    public AudioSource gameOverSoundSource;  // Assign the game over sound AudioSource in the Inspector
    [Header("Player Settings")]
    public float jumpForce = 10f; // Force applied when the player jumps
    public LayerMask groundLayer; // Layer representing the ground
    public Transform groundCheck; // Transform for checking ground collision
    public float groundCheckRadius = 0.2f; // Radius for ground check
    public Animator animator; // Animator for controlling animations

    [Header("Game Over UI")]
    public GameObject gameOverPanel; // Reference to the Game Over Panel

    private Rigidbody2D rb; // Rigidbody for physics-based movement
    private bool isGrounded = false; // Check if the player is on the ground

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        backgroundMusicSource.Play();
    }

    void Update()
    {
        
        
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Play running animation if grounded
        if (isGrounded)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("jump"); // Play jump animation
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Show Game Over Panel
            gameOverPanel.SetActive(true);
            
            backgroundMusicSource.Stop();

            // Play game over sound
            gameOverSoundSource.Play();

            // Stop the game (optional)
            Time.timeScale = 0f; // Pause the game
        }
    }
}
