using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls playerControls;
    public ObstacleSpawner obstacleSpawner;

    public float speed;

    public int lives = 3;

    Vector3 playerPos = new Vector3(0, 0, 0);

    private float horizontalRange;
    private float verticalRange;

    private void Awake()
    {
        playerControls = new PlayerControls();

        horizontalRange = obstacleSpawner.spawnRangeX;
        verticalRange = obstacleSpawner.spawnRangeY;

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

        /*float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        float horizontalOffSet = horizontalMove * speed * Time.deltaTime;
        float verticalOffSet = verticalMove * speed * Time.deltaTime;

        float rawHorizontalPosition = transform.position.x + x;
        float clampedHorizontalPosition = Mathf.Clamp(rawHorizontalPosition, -horizontalRange / 2, horizontalRange / 2);

        float rawVerticalPosition = transform.position.y + y;
        float clampedVerticalPosition = Mathf.Clamp(rawVerticalPosition, -verticalRange / 2, verticalRange / 2);

        transform.position = new Vector3(clampedHorizontalPosition, clampedVerticalPosition, transform.position.z);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(other.gameObject);
            lives--;
        }

        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
        }
    }
}
