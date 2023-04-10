using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage;
    public float tempDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("PlayerBullet") && collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("PlayerBullet") && CompareTag("EnemyBullet"))
        {
            tempDamage = bulletDamage;
            bulletDamage -= collision.gameObject.GetComponent<Bullet>().tempDamage;
            if (bulletDamage <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (CompareTag("EnemyBullet") && collision.gameObject.CompareTag("Skill"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}