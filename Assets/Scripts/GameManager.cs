using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    public bool isPaused = false;

    private GameObject player;
    private GameObject paused;

    public float spawnTime = 4f;
    public int spawnCount = 0;

    private float spawnTimer = 0f;
    private List<GameObject> enemies = new List<GameObject>();
    private System.Random RNG = new System.Random();

    void Start()
    {
        player = GameObject.Find("player");
        paused = GameObject.Find("paused");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            paused.GetComponent<SpriteRenderer>().enabled = true;
            foreach (GameObject enemy in enemies)
            {
                if (enemy == null)
                    continue;
                enemy.GetComponent<EnemyBehaviour>().Pause();
            }
            player.GetComponent<PlayerBehaviour>().Pause();
            spawnTimer = Time.time;
        }
        else
        {
            paused.GetComponent<SpriteRenderer>().enabled = false;
            foreach (GameObject enemy in enemies)
            {
                if (enemy == null)
                    continue;
                enemy.GetComponent<EnemyBehaviour>().Play();
            }
            player.GetComponent<PlayerBehaviour>().Play();
        }

        if (Time.time - spawnTimer >= spawnTime && !isPaused)
        {
            spawnTimer = Time.time;
            spawnTime = 4 - spawnCount * 0.1f;
            Spawn();
        }
    }

    private void Spawn()
    {
        int enemyType = RNG.Next(1, 101);
        Vector3 spawnVector = new Vector3(0, 0, 0);

        spawnVector.x = RNG.Next(-5, 6);
        if (Mathf.Abs(player.transform.position.x - spawnVector.x) <= 1)
        {
            int x = RNG.Next(0, 2);
            if (x == 0)
            {
                spawnVector.x -= 1;
            }
            else
            {
                spawnVector.x += 1;
            };
        }

        spawnVector.y = RNG.Next(-4, 5);
        if (Mathf.Abs(player.transform.position.y - spawnVector.y) <= 1)
        {
            int x = RNG.Next(0, 2);
            if (x == 0)
            {
                spawnVector.y -= 1;
            }
            else
            {
                spawnVector.y += 1;
            }
        }

        if (enemyType <= 33)
        {
            enemies.Add(Instantiate(enemy1, spawnVector, Quaternion.identity));
        }
        else if (enemyType <= 60)
        {
            enemies.Add(Instantiate(enemy2, spawnVector, Quaternion.identity));
        }
        else if (enemyType <= 85)
        {
            enemies.Add(Instantiate(enemy3, spawnVector, Quaternion.identity));
        }
        else
        {
            enemies.Add(Instantiate(enemy4, spawnVector, Quaternion.identity));
        }

        spawnCount++;
    }
}
