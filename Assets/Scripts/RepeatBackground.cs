using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;

    [SerializeField] private float scrollSpeed = 1;
    private float repeatWidth;

    private GameManager gameManager;

    public Transform birdTransform;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
        Physics2D.IgnoreCollision(birdTransform.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector2.left * Time.deltaTime * scrollSpeed);

            if (transform.position.x < startPos.x - repeatWidth)
            {
                transform.position = startPos;
            }
        }
    }
}
