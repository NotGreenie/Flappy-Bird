using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public float soundVolume = 1.0f; // TODO: replace value with slider value on title screen

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
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(pipePrefab);
        }
    }

    IEnumerator WaitForKeyDown()
    {
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
        titleScreen.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);

        StartCoroutine(WaitForKeyDown());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
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
        score++;
        scoreText.text = "Score: " + score;
        birdAudio.PlayOneShot(scoreSound, soundVolume);
    }
}
