using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float timeDestroy = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IEDestroyOBject());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    IEnumerator IEDestroyOBject()
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(this.gameObject);
    }
}
