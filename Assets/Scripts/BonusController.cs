using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    public int BonusType = 0;
    [SerializeField]
    float moveSpeed;
    public List<GameObject> listBonusPrefab;
    // Start is called before the first frame update
    void Start()
    {
        BonusType = Random.Range(1, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("wall"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
    }
}
