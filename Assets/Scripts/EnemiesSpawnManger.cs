using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnManger : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    float timeSpawn = 2;
    [SerializeField]
    List<Transform> listSpawnTransformPostions;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeSpawn);
            int indexPos = Random.Range(0, listSpawnTransformPostions.Count);
            Vector3 pos = listSpawnTransformPostions[indexPos].position;
            Instantiate(enemyPrefab, pos,enemyPrefab.transform.rotation);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
