using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject[] powerups;
    private bool _isSpawning = false;

    public void StartSpawning()
    {
        _isSpawning = true;
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRountine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        while (_isSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 9), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRountine()
    {
        yield return new WaitForSeconds(3.5f);
        while (_isSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 9), 7, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4, 8));
        }
    }
    public void OnPlayerDeath()
    {
        _isSpawning = false;

    }
}
