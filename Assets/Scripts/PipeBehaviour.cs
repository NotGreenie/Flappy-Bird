using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PipeBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private float spawnX = 12.0f;
    private float spawnY = 3.0f;
    
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        transform.position = new Vector2(spawnX, Random.Range(-spawnY, spawnY));
    }

    // Update is called once per frame
    void Update()
    {
        // If game is active, keep moving pipe to the left
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        // If offscreen, destroy pipe
        if (transform.position.x < -spawnX)
        {
            Destroy(gameObject);
        }
    }
}
