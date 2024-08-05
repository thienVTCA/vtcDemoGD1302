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
    // Start is called before the first frame update
    void Start()
    {
        jump = new Vector3(0.0f, 2.0f, 0.0f);
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
            //timeBullet = 0;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("player  take dam");
        if (collision.gameObject.tag.Equals("bulletEnemy") || collision.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("Destroy");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
            }
            if (timeBullet < timeBulletRespawn)
            {
                timeBullet += Time.deltaTime;
            }
            else
            {
                Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
                timeBullet = 0;
            }
        }
    }
}