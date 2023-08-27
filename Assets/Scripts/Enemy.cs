﻿using System.Collections;
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

    void Update()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        transform.Translate(_speed * Time.deltaTime * Vector3.down);  
        
        float randomX = Random.Range(-_randomRange, _randomRange);
        if (transform.position.y < _endYPos)
        {
            transform.position = new Vector3(randomX, _startYPos, transform.position.z);
        }
        if(_player == null)
        {
            Debug.LogError("Player Componmet is NULL");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            Destroy(this.gameObject);
        }else if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }
    }
}
