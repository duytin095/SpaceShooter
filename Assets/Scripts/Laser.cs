using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private float _posToDestroy = 8;

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);
        if(transform.position.y >= _posToDestroy)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
