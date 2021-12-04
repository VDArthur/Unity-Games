using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    [SerializeField] private float maxMoveLeft = -5f;
    [SerializeField] private float maxMoveRight = 5f;
    [SerializeField] private float speed = 1.5f;

    private bool moveRight = true;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position.x > maxMoveRight)
        {
            moveRight = false;
        }
        else if (transform.position.x < maxMoveLeft)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
}
