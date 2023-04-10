using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameManager gameManager;
    public Rigidbody2D rigid;
    public GameObject[] itemObjs;
    public GameObject[] enemyBulletObjs;
    public GameObject[] bossBulletObjs;
    public GameObject player;

    public float curShotDelay;
    public float maxShotDelay;
    public float bulletSpeed;
    public float speed;
    public float life;
    public float dmg;

    static public int difficulty;
    public int bulletCount;
    public int score;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.Find("Player");

        rigid.velocity = Vector2.down * speed;
    }
    private void FixedUpdate()
    {
        Shot();
        if(CompareTag("Boss") && transform.position.y <= 4)
        {
            rigid.velocity = Vector2.down * 0;
        }
    }

    void Difficulty()
    {
        switch (difficulty)
        {
            case 0:
                life /= 2;
                break;
            case 2:
                life *= 2;
                break;
        }
    }
    void Shot()
    {
        curShotDelay += Time.deltaTime;
        if(curShotDelay >= maxShotDelay && !CompareTag("Astroid"))
        {
            FireBullet();
            curShotDelay = 0;
        }
        else if (CompareTag("Astroid"))
        {
            transform.rotation = new Quaternion(0, 0, 0, curShotDelay * 10);
        }
    }
    void FireBullet()
    {
        if (CompareTag("Enemy"))
        {
            switch (GameManager.stageIndex)
            {
                case 0:
                    for (float i = bulletCount / -2; i <= bulletCount / 2; i++)
                    {
                        GameObject bullet0 = Instantiate(enemyBulletObjs[GameManager.stageIndex], transform.position, transform.rotation);
                        Rigidbody2D rigid0 = bullet0.GetComponent<Rigidbody2D>();

                        Vector2 dirvec0 = new Vector2(transform.position.x - transform.position.x + i, -5 - transform.position.y);
                        rigid0.velocity = dirvec0.normalized * bulletSpeed;
                    }
                    break;
                case 1:
                    GameObject bullet1 = Instantiate(enemyBulletObjs[GameManager.stageIndex], transform.position, transform.rotation);
                    Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();

                    Vector2 dirvec1 = player.transform.position - transform.position;
                    rigid1.velocity = dirvec1.normalized * bulletSpeed;
                    break;
                case 2:
                    for (float i = bulletCount / -2; i <= bulletCount / 2; i++)
                    {
                        GameObject bullet2 = Instantiate(enemyBulletObjs[GameManager.stageIndex], transform.position,transform.rotation);
                        Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();

                        Vector2 dirvec2 = player.transform.position - transform.position;
                        rigid2.velocity = dirvec2.normalized * bulletSpeed;
                    }
                    break;
            }
        }
        else if (CompareTag("Boss")) 
        {
            switch (GameManager.stageIndex)
            {
                case 0:
                    for (int i = -bulletCount; i <= bulletCount; i+=4)
                    {
                        GameObject bullet0 = Instantiate(bossBulletObjs[GameManager.stageIndex], transform.position, transform.rotation);
                        Rigidbody2D rigid0 = bullet0.GetComponent<Rigidbody2D>();

                        Vector2 dirVec0 = new Vector2(player.transform.position.x + i, -10 - transform.position.y);
                        rigid0.velocity = dirVec0.normalized * bulletSpeed;
                    }
                    break;
                case 1:
                    for (int i = -bulletCount; i <= bulletCount; i += 2)
                    {
                        GameObject bullet0 = Instantiate(bossBulletObjs[GameManager.stageIndex], transform.position, transform.rotation);
                        Rigidbody2D rigid0 = bullet0.GetComponent<Rigidbody2D>();

                        Vector2 dirVec0 = new Vector2(player.transform.position.x + i, -10 - transform.position.y);
                        rigid0.velocity = dirVec0.normalized * bulletSpeed;
                    }
                    break;
                case 2:
                    for (int i = -bulletCount; i <= bulletCount; i++)
                    {
                        GameObject bullet0 = Instantiate(bossBulletObjs[GameManager.stageIndex], transform.position, transform.rotation);
                        Rigidbody2D rigid0 = bullet0.GetComponent<Rigidbody2D>();

                        Vector2 dirVec0 = new Vector2(player.transform.position.x + i, -10 - transform.position.y);
                        rigid0.velocity = dirVec0.normalized * bulletSpeed;
                    }
                    break;
            }
        }
    }
    void Hit(float dmg)
    {
        life -= dmg;
        if(life <= 0)
        {
            Player.score += score;
            if (CompareTag("Boss"))
            {
                gameManager.StageClear();
            }
            ItemSpawn();
            Destroy(gameObject);
        }
        spriteRenderer.color = new Color(1, 1, 1, 0.2f);
        Invoke("ReturnSprite", 0.1f);
    }
    void ReturnSprite()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }
    void ItemSpawn()
    {
        int itemSpawn = Random.Range(0, 10);
        if (itemSpawn < 3)
        {
            int itemKind = Random.Range(0, 5);
            GameObject item = Instantiate(itemObjs[itemKind], transform.position, Quaternion.identity);
            Rigidbody2D rigid = item.GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.down * 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Hit(collision.gameObject.GetComponent<Bullet>().bulletDamage);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Skill"))
        {
            Hit(200);
        }
        else if (collision.gameObject.CompareTag("CheatKey"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}