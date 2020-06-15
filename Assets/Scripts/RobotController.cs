using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    bool broken = true;
    int direction = 1;
    public float moveDuration = 3.0f;
    float moveTimer;
    public ParticleSystem smokeEffect;
    Rigidbody2D rigidbody2d;
    public float speed = 3.0f;
    public bool vertical;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        moveTimer = moveDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(!broken)
        {
            return;
        }

        moveTimer -= Time.deltaTime;

        if(moveTimer <= 0) {
            direction = -direction;
            moveTimer = moveDuration;
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }

    void FixedUpdate() {
        if(!broken)
        {
            return;
        }

        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
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
