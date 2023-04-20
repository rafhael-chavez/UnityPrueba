using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float initialVelocity = 4f;
    [SerializeField] private float velocityMultiplier = 1.6f;
    [SerializeField] private ParticleSystem collisionParticle;
    private Rigidbody2D ballRb;
    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void Launch()
    {
        float xVelocity = Random.Range(0,2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        ballRb.velocity = new Vector2(xVelocity, yVelocity) * initialVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("paddle"))
        {
            ballRb.velocity *= velocityMultiplier;
            EmitParticle(50);
            GameManager.Instance.screenShake.StartShake(Mathf.Sqrt(ballRb.velocity.magnitude)*0.02f,0.075f);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            EmitParticle(50);
            GameManager.Instance.screenShake.StartShake(0.033f, 0.033f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("goal1")){
            GameManager.Instance.Paddle2Scored();
            GameManager.Instance.Restart();
            Launch();
            EmitParticle(200);
            GameManager.Instance.screenShake.StartShake(0.33f, 0.1f);

        }
        else
        {
            GameManager.Instance.Paddle1Scored();
            GameManager.Instance.Restart();
            Launch();
            EmitParticle(200);
            GameManager.Instance.screenShake.StartShake(0.33f, 0.1f);

        }
        
        
    }

    private void EmitParticle(int amount)
    {
        collisionParticle.Emit(amount);

    }
 
}
