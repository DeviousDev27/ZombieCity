using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;


    [SerializeField]
    private float enemyInterval = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnmey(enemyInterval, enemyPrefab));
        
    }

    
    private IEnumerator SpawnEnmey(float interval, GameObject enemy)
    {

            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6, 6), 0), Quaternion.identity);
            StartCoroutine(SpawnEnmey(interval, enemy));
    }

}
