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
    private Player _player;

    private Animator _anim;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();

        if (_player == null)
        {
            Debug.LogError("Player Componmet is NULL");
        }
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }
    }

    private void Update()
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
            Destroy(other.gameObject);
            _player.AddScore(10);
            _speed = 0;
            _anim.SetTrigger("onEnemyDestroyed");
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
            Destroy(this.gameObject, 2.4f);
        }
    }
}
