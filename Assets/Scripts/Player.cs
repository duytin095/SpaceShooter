using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private float _fireRate = 0.5f;

    [SerializeField]
    private Transform _laserPrefab;

    [SerializeField] 
    GameObject _shieldPrefab;

    [SerializeField]
    private Transform _tripleLaserPrefab;

    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private AudioClip _fireClip;

    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;
    private float _canFire = -1f;
    private float _speedMultiplier = 2;
    private int _score;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private AudioSource _audioSource;

    void Start()
    {
        transform.position = Vector3.zero;

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        if ( _spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        if(_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }
        if(_audioSource == null)
        {
            Debug.LogError("AudioSource on player is NULL");
        }
        else
        {
            _audioSource.clip = _fireClip;
        }
    }


    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire )
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float rightPosBound = 11.2f;
        float leftPosBound = -11.2f;
        float topPosBound = 1;
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

        _audioSource.Play();
    }
    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldPrefab.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);
        PlayerHurtVisualizer();

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            transform.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }

    void PlayerHurtVisualizer()
    {
        if (_lives == 2)
        {
            _engines[Random.Range(0, 2)].SetActive(true);
        }
        else if (_lives == 1)
        {
            foreach (var engine in _engines)
            {
                engine.SetActive(true);
            }
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
        _isTripleShotActive = false;
    }
    public void ActiveSpeedBoots()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(DeactiveSpeedBoots());
    }
    IEnumerator DeactiveSpeedBoots()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
    }

    public void ActiveShield()
    {
        _isShieldActive = true;
        _shieldPrefab.SetActive(true);
    }

    public void AddScore(int score)
    {
        _score += score;
        _uiManager.UpdateScore(_score);
    }
 
}
