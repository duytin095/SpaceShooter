using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 9), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
}
