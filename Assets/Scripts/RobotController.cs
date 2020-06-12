﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    int direction = 1;
    public float moveDuration = 3.0f;
    float moveTimer;
    Rigidbody2D rigidbody2d;
    public float speed = 3.0f;
    public bool vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        moveTimer = moveDuration;
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer -= Time.deltaTime;

        if(moveTimer <= 0) {
            direction = -direction;
            moveTimer = moveDuration;
        }
    }

    void FixedUpdate() {
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController controller = other.gameObject.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }

}
