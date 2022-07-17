using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public int maxHealth = 3;
    public int health    = 3;
    public int speed         = 10;
    public int rotationSpeed = 10;
    public bool canMove = true;
    public GameObject bullet;
    public AudioClip healSound;
    
    private bool moving = false;
    private int  pprevValue = 0;
    private int  prevValue  = 0;
    private int  value      = 4;
    private float prevTime  = 0f;

    private Rigidbody2D   rb;
    private Animator      anim;
    private System.Random RNG;

    void Start()
    {
        RNG = new System.Random();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Screen.fullScreen = !Screen.fullScreen;
            }

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            if ((horizontal == 0 && vertical == 0) && moving)
            {
                pprevValue = prevValue;
                prevValue = value;
                value = RNG.Next(1, 7);
            }

            moving = !(horizontal == 0 && vertical == 0);
            anim.SetBool("moving", moving);
            anim.SetInteger("value", value);

            rb.AddForce(new Vector2(horizontal, vertical) * speed * Time.deltaTime);

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            
            if (Input.GetMouseButtonDown(0) && !moving && Time.time - prevTime > 0.33f)
            {
                if (pprevValue == 6 && prevValue == 6 && value == 6)
                {
                    Die();
                }
                else if (value == 1)
                {
                    ShootSlow(angle);
                }
                else if (value == 2)
                {
                    ShootMedium(angle);
                }
                else if (value == 3)
                {
                    Heal();
                }
                else if (value == 4)
                {
                    ShootFast(angle);
                }
                else if (value == 5)
                {
                    ShootMedium(angle);
                }
                else if (value == 6)
                {
                    ShootMultiple();
                }
                prevTime = Time.time;
                rb.AddForce(-(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * 40);
                moving = true;
                anim.SetBool("moving", moving);
            }
        }
    }

    public void ShootSlow(float angle)
    {
        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * 1;
        var Bullet = Instantiate(bullet, transform.position + offset, Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed*10));
        Bullet.GetComponent<BulletBehaviour>().SetDirection(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)));
        Bullet.GetComponent<BulletBehaviour>().SetVelocity(2f);
    }

    public void ShootMedium(float angle)
    {
        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * 1;
        var Bullet = Instantiate(bullet, transform.position + offset, Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed*10));
        Bullet.GetComponent<BulletBehaviour>().SetDirection(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)));
        Bullet.GetComponent<BulletBehaviour>().SetVelocity(4f);
    }

    public void ShootFast(float angle)
    {
        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * 1;
        var Bullet = Instantiate(bullet, transform.position + offset, Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed*10));
        Bullet.GetComponent<BulletBehaviour>().SetDirection(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)));
        Bullet.GetComponent<BulletBehaviour>().SetVelocity(8f);
    }

    public void ShootMultiple()
    {
        for (int i = 0; i < 10; i++)
        {
            float angle = 360 / 10 * i;
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * 1.33f;
            var b = Instantiate(bullet, transform.position + offset, Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), Time.deltaTime * rotationSpeed*10));
            b.GetComponent<BulletBehaviour>().SetDirection(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)));
            b.GetComponent<BulletBehaviour>().SetVelocity(8f);
        }
    }

    public void Hit()
    {
        health--;

        if (health <= 0)
        {
            Die();
        }

        ScoreManager.instance.UpdateHealth(health);
    }

    public void Heal()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(healSound);
        health = Mathf.Min(health + 1, maxHealth);
        ScoreManager.instance.UpdateHealth(health);
    }

    public void Die()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void Pause()
    {
        canMove = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Play()
    {
        canMove = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
