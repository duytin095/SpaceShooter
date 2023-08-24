﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);
    }
}
