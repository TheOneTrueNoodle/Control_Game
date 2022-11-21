using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls playerControls;
    private BoxCollider boxCollider;

    Vector3 playerPos = new Vector3(0, 0, 0);

    private float horizontalRange;
    private float verticalRange;

    public ObstacleSpawner obstacleSpawner;
    public ScoreTracker scoreTracker;

    [Header("Player Stats")]
    public float speed;

    public float invulnerableTimer = 0.01f;

    public int coins = 0;

    [Header("Lives")]
    public int lives = 3;
    public GameObject [] hearts;

    private bool Dead;


    private void Awake()
    {
        playerControls = new PlayerControls();

        horizontalRange = obstacleSpawner.spawnRangeX / 2;
        verticalRange = obstacleSpawner.spawnRangeY / 2;

        boxCollider = FindObjectOfType<BoxCollider>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        float x = playerControls.Player.Horizontal.ReadValue<float>();

        float y = playerControls.Player.Vertical.ReadValue<float>();

        transform.Translate(speed * x * Time.deltaTime, 0, 0);

        transform.Translate(0, speed * y * Time.deltaTime, 0);

        if (transform.position.x >= horizontalRange)
        {
            transform.Translate(-0.02f, 0, 0);
        }

        if (transform.position.x <= -horizontalRange)
        {
            transform.Translate(0.02f, 0, 0);
        }

        if (transform.position.y >= verticalRange)
        {
            transform.Translate(0, -0.02f, 0);
        }

        if (transform.position.y <= -verticalRange)
        {
            transform.Translate(0, 0.02f, 0);
        }

        if (lives <= -1 & Dead != true)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(other.gameObject);
            StartCoroutine(Invulnerable());
            hearts[lives].SetActive(false);
            lives--;
        }

        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            coins++;
        }
    }

    IEnumerator Invulnerable()
    {
        boxCollider.isTrigger = true;
        //Animation moment???
        yield return new WaitForSeconds(invulnerableTimer);
        boxCollider.isTrigger = false;
    }

    public void GameOver()
    {
        StartCoroutine(scoreTracker.MainMenu());
        Dead = true;
    }
}
