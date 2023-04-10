using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public GameManager gameManager;
    public GameObject bulletObj;

    public int h;
    public int v;
    public int attackLevel;
    static public int score = 3;

    public float fuel;
    public float life;
    public float curShotDelay;
    public float maxShotDelay;
    public float bulletSpeed;
    public float speed;

    public bool inv;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        score = 0;
    }

    void Update()
    {
        Move();
        Fire();
        Fuel();
        Skill();
    }

    void Skill()
    {
        
    }
    void Fuel()
    {
        fuel -= Time.deltaTime;
        if(fuel < 0)
        {
            gameManager.GameOver();
        }
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            h = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            h = -1;
        }
        else
        {
            h = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            v = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            v = -1;
        }
        else
        {
            v = 0;
        }

        Vector3 dirvec = new Vector3(h, v, 0);
        rigid.velocity = dirvec.normalized * speed;
    }
    void Fire()
    {
        curShotDelay += Time.deltaTime;
        if(curShotDelay >= maxShotDelay && Input.GetKeyDown(KeyCode.D))
        {
            for (float i = -1 * attackLevel; i < attackLevel + 1; i++)
            {
                GameObject bullet = Instantiate(bulletObj, new Vector3(transform.position.x + i * 0.3f, transform.position.y, transform.position.z), transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

                rigid.velocity = Vector3.up * bulletSpeed;
            }
            curShotDelay = 0;
        }
    }
    void Hit(float dmg, float time)
    {
        if (!inv)
        {
            life -= dmg;
            inv = true;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            Invoke("ReturnSprite", time);
            if(life <= 0)
            {
                gameManager.GameOver();
            }
        }
    }
    void ReturnSprite()
    {
        inv = false;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!inv)
        {
            if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("BossBullet"))
            {
                Hit(collision.gameObject.GetComponent<Bullet>().dmg, 1.5f);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
            {
                Hit(collision.gameObject.GetComponent<Enemy>().dmg, 1.5f);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("Item"))
            {
                switch (collision.gameObject.GetComponent<Item>().itemN)
                {
                    case 0:
                        if (attackLevel < 4)
                        {
                            attackLevel++;
                        }
                        break;
                    case 1:
                        Hit(0, 3f);
                        break;
                    case 2:
                        if(life + 40 >= 100)
                        {
                            life = 100;
                        }
                        else
                        {
                            life += 40;
                        }
                        break;
                    case 3:
                        if(fuel + 40 >= 100)
                        {
                            fuel = 100;
                        }
                        else
                        {
                            fuel += 40;
                        }
                        break;

                }
            }
        }
    }
}