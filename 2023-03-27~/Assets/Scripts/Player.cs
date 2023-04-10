using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] bulletObjs;
    public Sprite[] spriteObjs;
    public GameObject skillObj;
    public AudioClip shotSound;
    public AudioClip hitSound;
    public GameManager gameManager;
    public GameObject cheatKeyObj;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    Rigidbody2D rigid;

    public bool absolute;

    public float movementSpeed;
    public float maxSkill1Delay;
    public float curSkill1Delay;
    public float maxSkill2Delay;
    public float curSkill2Delay;
    public float maxShotDelay;
    public float curShotDelay;
    public float attackLevel;
    public float maxLife;
    public float life;
    public float fuel;

    static public int score;
    public int[] bulletSpeed;
    public int skill1Count;
    public int skill2Count;
    public int spriteNumber;
    public int horizontalMovement;
    public int verticalMovement;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();

        spriteRenderer.sprite = spriteObjs[spriteNumber];
    }

    private void FixedUpdate()
    {
        Move();
        Shot();
        Skill();
        Fuel();
        if (Input.GetKey(KeyCode.F1))
        {
            GameObject cheatKey = Instantiate(cheatKeyObj, new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(cheatKey, 0.1f);
        }
        else if (Input.GetKey(KeyCode.F2))
        {
            attackLevel = 3;
        }
        else if (Input.GetKey(KeyCode.F3))
        {
            skill1Count = 3;
            skill2Count = 3;

            curSkill1Delay = 10;
            curSkill2Delay = 10;
        }
        else if (Input.GetKey(KeyCode.F4))
        {
            life = 5;
        }
        else if (Input.GetKey(KeyCode.F5))
        {
            fuel = 100;
        }
    }

    void Fuel()
    {
        fuel -= Time.deltaTime;
        if(fuel <= 0)
        {
            gameManager.GameOver();
        }
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMovement = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMovement = -1;
        }
        else
        {
            horizontalMovement = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalMovement = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalMovement = -1;
        }
        else
        {
            verticalMovement = 0;
        }

        Vector2 dirVec = new Vector2(horizontalMovement, verticalMovement);
        rigid.velocity = dirVec * movementSpeed;
    }
    void Shot()
    {
        curShotDelay += Time.deltaTime;
        if(curShotDelay >= maxShotDelay && Input.GetKey(KeyCode.D))
        {
            audioSource.clip = shotSound;
            audioSource.Play();
            FireBullet();
            curShotDelay = 0;
        }
    }
    void FireBullet()
    {
        for (float i = attackLevel / -2; i <= attackLevel / 2; i++)
        {
            GameObject bullet = Instantiate(bulletObjs[spriteNumber], new Vector2(transform.position.x + i / 3, transform.position.y), transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.up * bulletSpeed[spriteNumber];
        }
    }
    void Skill()
    {
        curSkill1Delay += Time.deltaTime;
        curSkill2Delay += Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && life < 5 && curSkill1Delay >= maxSkill1Delay && skill1Count != 0)
        {
            life++;
            skill1Count--;
            curSkill1Delay = 0;
        }
        else if (Input.GetKey(KeyCode.E) && curSkill2Delay >= maxSkill2Delay && skill2Count != 0)
        {
            GameObject skill = Instantiate(skillObj, new Vector2(0, 0), Quaternion.identity);
            Destroy(skill, 0.2f);
            skill2Count--;
            curSkill2Delay = 0;
        }
    }
    void Hit(float dmg, float absolutetime, bool trans)
    {
        if (!absolute)
        {
            life -= dmg;
            audioSource.clip = hitSound;
            audioSource.Play();
            if (trans)
            {
                transform.position = new Vector2(0, 0);
            }
            if (life <= 0)
            {
                gameManager.GameOver();
            }
            spriteRenderer.color = new Color(1, 1, 1, 0.2f);
            absolute = true;
            Invoke("ReturnSprite", absolutetime);
        }
    }
    void ReturnSprite()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        absolute = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") ||  collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("Astroid"))
        {
            Hit(1, 2f, true);
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Hit(1, 2f, true);
        }
        else if (collision.gameObject.CompareTag("Item"))
        {
            switch (collision.gameObject.GetComponent<Item>().ItemNumber)
            {
                case 0:
                    Hit(0, 3f, false);
                    break;
                case 1:
                    if (fuel + 40 > 100)
                    {
                        fuel = 100;
                    }
                    else
                    {
                        fuel += 40;
                    }
                    break;
                case 2:
                    if(life < 5)
                    {
                        life++;
                    }
                    break;
                case 3:
                    if(attackLevel < 3)
                    {
                        attackLevel++;
                    }
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}