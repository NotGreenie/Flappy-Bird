using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// GameManager is a base class that makes everything, from sounds, UI, and even telling other scripts if the game is active, work.
/// </summary>
public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI finalScoreText;

    public Button restartButton;
    public GameObject titleScreen;
    public GameObject gameOverText;
    public GameObject titleBound;

    private AudioSource birdAudio;
    public AudioClip hitSound;
    public AudioClip scoreSound;

    [SerializeField] private GameObject pipePrefab;

    private int score;
    [SerializeField] private float spawnRate;
    public float soundVolume = 1.0f;

    public bool isGameActive;
    public bool startingJump;
    
    // Start is called before the first frame update
    void Start()
    {
        birdAudio = GameObject.Find("Bird").GetComponent<AudioSource>();

        isGameActive = false;
        score = 0;
        scoreText.text = "Score: " + score;
        titleBound.gameObject.SetActive(true);
    }

    IEnumerator SpawnPipes()
    {
        // While game is active, keep spawning pipes, then after pipe has been spawned wait for spawnRate seconds
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(pipePrefab);
        }
    }

    IEnumerator WaitForKeyDown()
    {
        // Wait until space is down or left mouse button has been clicked, then start the game
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));

        isGameActive = true;
        startingJump = true;
        scoreText.gameObject.SetActive(true);
        titleBound.gameObject.SetActive(false);
        warningText.gameObject.SetActive(false);
        StartCoroutine(SpawnPipes());
    }

    public void StartGame()
    {
        // Sets up warning text, telling player to click or press space bar
        titleScreen.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);

        // Starts a coroutine waiting for the player to click or press space bar before starting the game
        StartCoroutine(WaitForKeyDown());
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the scene

    public void GameOver()
    {
        // Stops the game by setting isGameActive to false, then setting the game over UI to be active
        isGameActive = false;
        StopCoroutine(SpawnPipes());

        restartButton.gameObject.SetActive(true);
        gameOverText.SetActive(true);
        scoreText.gameObject.SetActive(false);
        finalScoreText.text = "Your final score: " + score;
        finalScoreText.gameObject.SetActive(true);
    }

    public void IncreaseScore()
    {
        // Increments the score, then plays a short sound
        score++;
        scoreText.text = "Score: " + score;
        birdAudio.PlayOneShot(scoreSound, soundVolume);
    }
}
