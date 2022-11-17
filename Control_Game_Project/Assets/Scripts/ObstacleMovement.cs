using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5.0f;

    private Transform camera;

    private void Awake()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void Update()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);

        if (transform.position.z < camera.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}
