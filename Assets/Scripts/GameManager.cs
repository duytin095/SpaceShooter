﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    [SerializeField]
    private bool _isCoopMode;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isGameOver && Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(1);
            _isGameOver = false;
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    
    public void RestartGame()
    {
        _isGameOver = true;
    }
    public bool IsCoopMode()
    {
        return _isCoopMode;
    }
}
