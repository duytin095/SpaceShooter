using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject _tripleShotPowerupPrefabs;
    private bool _isSpawning = true;
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRountine());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_isSpawning == true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 9), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRountine()
    {
        while (_isSpawning == true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 9), 7, 0);
            Instantiate(_tripleShotPowerupPrefabs, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4, 8));
        }
    }
    public void OnPlayerDeath()
    {
        _isSpawning = false;
    }
}
