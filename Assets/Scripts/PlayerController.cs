using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody mRigidbody;
    public float deltaMove;
    [SerializeField]
    float rotateSpeed = 2;
    [SerializeField]
    Transform gunTransform;
    [SerializeField]
    GameObject bulletPrefab;
    Vector3 jump;
    [SerializeField]
    float jumpForce = 2.0f;
    [SerializeField]
    float timeBullet = 0, timeBulletRespawn = 2;
    [SerializeField]
    bool isAutoBullet = false;
    [SerializeField]
    int health = 5;
    int maxHealth = 5;
    AudioSource shootingSound;
    [SerializeField]
    List<AudioClip> listAudios;
    [SerializeField]
    GameObject ExplosionPrefab, HitEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        shootingSound = GetComponent<AudioSource>();
        UIManager.uiManagerInstance.UpdatePlayerHealthSlider((float)health / (float)maxHealth);
        //Application.targetFrameRate = 24;
        //if(isAutoBullet)
        //{
        //    StartCoroutine(IESpawningBullets());
        //}
    }

    IEnumerator IESpawningBullets()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBulletRespawn);
            Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("player take dam");
        if (collision.gameObject.tag.Equals("bulletEnemy") || collision.gameObject.tag.Equals("Enemy"))
        {
            if(collision.gameObject.tag.Equals("bulletEnemy") && health > 0)
            {
                Instantiate(HitEffectPrefab, transform.position, Quaternion.identity);
                shootingSound.clip = listAudios[0];
                shootingSound.Play();
                health--;
                UIManager.uiManagerInstance.UpdatePlayerHealthSlider((float)health / (float)maxHealth);
                Destroy(collision.gameObject);
            }
            else if (health == 0)
            {
                Debug.Log("Player Destroy");
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                UIManager.uiManagerInstance.GameOver();
                Destroy(gameObject);
                //Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag.Equals("gold"))
        {
            Debug.Log("Player get golds");
            // add coin to player infor
            UIManager.uiManagerInstance.PlayerGetCoins(1);
            Destroy(collision.gameObject);
        } 
        else if (collision.gameObject.tag.Equals("bonus"))
        {
            // player get bonus
            GetBonus(collision.gameObject);
        }

    }

    void GetBonus(GameObject bonus)
    {
        Debug.Log("Player get  GetBonus");
        BonusController bController = bonus.GetComponent<BonusController>();
        bulletPrefab = bController.listBonusPrefab[bController.BonusType];
        Destroy(bonus);
    }

    IEnumerator IEPlayerDie(Collision col)
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Debug.Log("Destroy");
        yield return new WaitForSeconds(0.1f);
        Destroy(col.gameObject);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float h = -Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //MoveCharacter();
        //mRigidbody.MovePosition(transform.position + new Vector3(h, 0, v) * deltaMove * Time.deltaTime);
        Vector3 movement = new Vector3(v, 0, h);
        transform.Translate(movement * deltaMove * Time.deltaTime);
        //transform.Rotate(new Vector3(0f, h * rotateSpeed * Time.deltaTime, 0f));
        //transform.Translate(0, 0, 2);
        if (Input.GetMouseButtonDown(0) && !isAutoBullet)
        {
            //Debug.Log("create bullet");
            Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        }
        else if(isAutoBullet)
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    mRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
            //}
            if (timeBullet < timeBulletRespawn)
            {
                timeBullet += Time.deltaTime;
            }
            else
            {
                shootingSound.clip = listAudios[1];
                shootingSound.Play();
                Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
                timeBullet = 0;
            }
        }
    }
}
