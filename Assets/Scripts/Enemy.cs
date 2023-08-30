using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    [SerializeField]
    private float _randomRange = 10.5f;
    [SerializeField]
    private float _startYPos = 7.3f;
    [SerializeField]
    private float _endYPos = -5.3f;
    [SerializeField]
    private GameObject _enemyLaser;

    private Player _player;
    private float _fireRate;
    private float _canFire = -1;
    private bool _isEnemyDestroyed;

    private Animator _anim;
    private AudioSource _audioSource;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player Componmet is NULL");
        }
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on Enemy is NULL");
        }
    }

    private void Update()
    {
        CaculateMovement();

        if(Time.time > _canFire && !_isEnemyDestroyed)
        {
            _fireRate = Random.Range(3, 6);
            _canFire = Time.time + _fireRate;
            GameObject newEnemyLaser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            Laser[] enemyLasers = newEnemyLaser.GetComponentsInChildren<Laser>();

            foreach (var laser in enemyLasers)
            {
                laser.AssignEnemyLaser();
            }
        }
    }

    void CaculateMovement()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);
        float randomX = Random.Range(-_randomRange, _randomRange);
        if (transform.position.y < _endYPos)
        {
            transform.position = new Vector3(randomX, _startYPos, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Laser"))
        {
            Laser laser = other.GetComponent<Laser>();
            if(laser != null && laser.IsEnemyLaser()){
                return;
            }

            Destroy(other.gameObject);
            _player.AddScore(10);
            _speed = 0;
            _anim.SetTrigger("onEnemyDestroyed");
            _audioSource.Play();
            _isEnemyDestroyed = true;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.4f);
        }else if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            _speed = 0;
            _anim.SetTrigger("onEnemyDestroyed");
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.4f);
        }


    }
}
