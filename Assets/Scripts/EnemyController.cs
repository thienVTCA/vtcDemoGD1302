using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float timeBullet = 0, timeBulletRespawn = 2;
    [SerializeField]
    Transform gunTransform;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    int health = 2;
    [SerializeField]
    GameObject ExplosionPrefab, HitEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IESpawningBullets());
    }
    IEnumerator IESpawningBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBulletRespawn);
            Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
            //timeBullet = 0;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("EnemyController take dam");
    //    if (collision.gameObject.tag.Equals("bullet") || collision.gameObject.tag.Equals("Player"))
    //    {
    //        Debug.Log("Destroy");
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }
    //    else
    //    if (collision.gameObject.tag.Equals("wall"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnemyController take dam");
        if (other.gameObject.tag.Equals("bullet") || other.gameObject.tag.Equals("Player"))
        {
            if (other.gameObject.tag.Equals("bullet") && health > 0)
            {
                Destroy(other.gameObject);
                Instantiate(HitEffectPrefab, transform.position, Quaternion.identity);
                health--;
            }
            else if (other.gameObject.tag.Equals("Player") || health == 0)
            {
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                Debug.Log("Enemy Destroy");
                UIManager.uiManagerInstance.UpdateEnemiesKilledNumber();
                if (other.gameObject.tag.Equals("Player"))
                {
                    UIManager.uiManagerInstance.GameOver();
                    Destroy(other.gameObject);
                }
                    
                Destroy(gameObject);
                
            }
        }
        if (other.gameObject.tag.Equals("wall"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
