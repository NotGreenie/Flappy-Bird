using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private AudioSource birdAudio;
    public AudioClip fallSound;
    public AudioClip jumpSound;
    
    [SerializeField] private float jumpForce;

    private Rigidbody2D birdRb;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        birdRb = GetComponent<Rigidbody2D>();
        birdAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the player jump with a sound effect if space key is pressed or if player has left clicked AND if the game is still going or if first jump is true
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && gameManager.isGameActive || gameManager.startingJump)
        {
            birdRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            birdAudio.PlayOneShot(jumpSound, gameManager.soundVolume);
            gameManager.startingJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player collided with pipe, call GameOver() and play a hit sound
        if (collision.gameObject.CompareTag("Pipe"))
        {
            gameManager.GameOver();
            birdAudio.PlayOneShot(gameManager.hitSound, gameManager.soundVolume);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player has triggered score trigger, increase score...
        if (collision.gameObject.CompareTag("Trigger"))
        {
            gameManager.IncreaseScore();
        }
        // ...else if player is not on the screen, call GameOver() and play a falling sound
        else if (collision.gameObject.CompareTag("FallTrigger") && gameManager.isGameActive)
        {
            gameManager.GameOver();
            birdAudio.PlayOneShot(fallSound, gameManager.soundVolume);
        }
    }
}
