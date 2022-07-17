using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float       Speed;
    public AudioClip   BulletSound;
    public GameObject  Particles;

    private Rigidbody2D rb;
    private Vector2     direction;

    void Start()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(BulletSound);
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * Speed;
    }

    public void SetDirection(Vector2 newdir)
    {
        direction = newdir;
    }

    public void SetVelocity(float speed)
    {
        Speed = speed;
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerBehaviour p = collision.collider.GetComponent<PlayerBehaviour>();
        EnemyBehaviour  e = collision.collider.GetComponent<EnemyBehaviour>();

        if      (p != null) {p.Hit(); Instantiate(Particles, transform.position, Quaternion.identity); SelfDestruct();}
        else if (e != null) {e.Hit(); Instantiate(Particles, transform.position, Quaternion.identity); ScoreManager.instance.AddPoints(10); SelfDestruct();}

        else {
            Instantiate(Particles, transform.position, Quaternion.identity);
            SelfDestruct();
        }
    }
}
