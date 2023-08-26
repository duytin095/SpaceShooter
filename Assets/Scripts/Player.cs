using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private Transform _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private Transform _tripleLaserPrefab;
    void Start()
    {
        transform.position = Vector3.zero;

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if( _spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
    }


    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float rightPosBound = 11.2f;
        float leftPosBound = -11.2f;
        float topPosBound = 0;
        float botPosBound = -3.9f;
        float currentPosX = transform.position.x;
        float currentPosY = transform.position.y;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(_speed * Time.deltaTime * direction);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, botPosBound, topPosBound), 0);

        if (currentPosX > rightPosBound)
        {
            transform.position = new Vector3(leftPosBound, currentPosY, 0);
        }
        else if (currentPosX < leftPosBound)
        {
            transform.position = new Vector3(rightPosBound, currentPosY, 0);
        }
    }

    void FireLaser()
    {
        float offSet = 1.05f;
        Vector3 laserSpawnPos =
            new Vector3(transform.position.x, transform.position.y + offSet, transform.position.z);

        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive)
        {
            Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, laserSpawnPos, Quaternion.identity);
        }
    }
    public void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActiveTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(DeactiveTripleShot());
    }

    IEnumerator DeactiveTripleShot()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive=false;
    }
}
