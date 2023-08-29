using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private float _topPosToDestroy = 8.0f;
    private float _bottomPosToDestroy = -5.0f;
    [SerializeField]
    private bool _isEnemyLaser;
    void Update()
    {
        if( _isEnemyLaser)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }

        
    }


    void MoveUp()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);
        if (transform.position.y >= _topPosToDestroy)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);
        if (transform.position.y < _bottomPosToDestroy)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    public bool IsEnemyLaser()
    {
        return _isEnemyLaser;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isEnemyLaser)
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
        }
    }
}
