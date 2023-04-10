using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    GameObject player;
    Rigidbody2D rigid;
    public GameObject[] itemObjs;
    public GameObject gameManager;
    public GameObject bulletObj;

    public float dmg;
    public float life;
    public float curSpawnDelay;
    public float maxSpawnDelay;
    public float speed;
    public float bulletSpeed;

    public int enemyScore;
    public int enemyN; 

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager");

        rigid.velocity = Vector3.down * speed;
    }
    void Update()
    {
        Fire();
        if (CompareTag("Boss") && transform.position.y <= 8)
        {
            speed = 0;
            rigid.velocity = Vector3.down * speed;
        }
    }
    void Fire()
    {
        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay > maxSpawnDelay)
        {
            switch (enemyN)
            {
                case 0:
                    GameObject bullet0 = Instantiate(bulletObj, transform.position, transform.rotation);
                    Rigidbody2D rigid0 = bullet0.GetComponent<Rigidbody2D>();

                    Vector3 dirvec0 = player.transform.position - transform.position;
                    rigid0.AddForce(dirvec0.normalized * bulletSpeed, ForceMode2D.Impulse);
                    curSpawnDelay = 0;
                    break;
                case 1:
                    for (int i = -2; i < 3; i += 4)
                    {
                        GameObject bullet1 = Instantiate(bulletObj, transform.position, transform.rotation);
                        Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();

                        Vector3 dirvec1 = new Vector3(transform.position.x + i, -12f, 0) - transform.position;
                        rigid1.AddForce(dirvec1.normalized * bulletSpeed, ForceMode2D.Impulse);
                    }
                    break;
                case 2:
                    for (int i = -3; i < 4; i += 2)
                    {
                        GameObject bullet1 = Instantiate(bulletObj, transform.position, transform.rotation);
                        Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();

                        Vector3 dirvec1 = new Vector3(transform.position.x + i, -12f, 0) - transform.position;
                        rigid1.AddForce(dirvec1.normalized * bulletSpeed, ForceMode2D.Impulse);
                    }
                    break;
                case 3:
                    for (int i = -20; i < 21; i += 6)
                    {
                        GameObject bullet1 = Instantiate(bulletObj, transform.position, transform.rotation);
                        Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();

                        Vector3 dirvec1 = new Vector3(transform.position.x + i, -12f, 0) - transform.position;
                        rigid1.AddForce(dirvec1.normalized * bulletSpeed, ForceMode2D.Impulse);
                    }
                    break;
                case 4:
                    for (int i = -20; i < 21; i += 4)
                    {
                        GameObject bullet1 = Instantiate(bulletObj, transform.position, transform.rotation);
                        Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();

                        Vector3 dirvec1 = new Vector3(transform.position.x + i, -12f, 0) - transform.position;
                        rigid1.AddForce(dirvec1.normalized * bulletSpeed, ForceMode2D.Impulse);
                    }
                    break;
            }
            curSpawnDelay = 0f;
        }
    }

    void Hit(float dmg, float time)
    {
        life -= dmg;
        spriteRenderer.color = new Color(1, 1, 1, 0f);
        Invoke("ReturnSprite", time);
        if(life <= 0)
        {
            Player.score += enemyScore;
            ItemSpawn();
            if (CompareTag("Boss"))
            {
                gameManager.GetComponent<GameManager>().StageClear();
            }
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    void ItemSpawn()
    {
        int itemRange = Random.Range(0, 5);

        GameObject item = Instantiate(itemObjs[itemRange], transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border") && CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Hit(collision.gameObject.GetComponent<Bullet>().dmg, 0.1f);
            Destroy(collision.gameObject);
        }
    }
}   