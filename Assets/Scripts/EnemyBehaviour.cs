using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int maxHealth = 3;
    public int health    = 3;
    public int speed         = 10;
    public int rotationSpeed = 10;
    public float shootPower  = 1f;
    public float attackTime;

    public bool canMove = true;

    public GameObject bullet;

    private GameObject player;
    private Rigidbody2D   rb;
    private System.Random RNG;
    private float prevTime;

    void Start()
    {
        prevTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("player");
    }

    void Update()
    {
        if (canMove)
        {
            rb.AddForce(new Vector2(Mathf.Min(player.transform.position.x - transform.position.x, 1), Mathf.Min(player.transform.position.y - transform.position.y, 1)) * speed * Time.deltaTime);
            if (Time.time - prevTime > attackTime)
            {
                prevTime = Time.time;
                Shoot();
            }
        }
        else
        {
            prevTime = Time.time;
        }
    }

    public void Hit()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        Vector3 offset = new Vector3(Smoother(player.transform.position.x - transform.position.x), Smoother(player.transform.position.y - transform.position.y), 0) * 1;
        GameObject bulletInstance = Instantiate(bullet, transform.position + offset, Quaternion.identity);
        bulletInstance.GetComponent<BulletBehaviour>().SetVelocity(shootPower/4);
        bulletInstance.GetComponent<BulletBehaviour>().SetDirection(new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y));
    }

    public float Smoother(float x)
    {
        if (x <= -0.5f)
        {
            return -0.5f;
        }
        else if (x >= 0.5f)
        {
            return 0.5f;
        }
        else
        {
            return x;
        }
    }

    public void Pause()
    {
        canMove = false;
    }

    public void Play()
    {
        canMove = true;
    }
}
