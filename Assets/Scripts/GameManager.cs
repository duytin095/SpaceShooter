using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isGameOver && Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            _isGameOver = false;
        }
    }
    
    public void RestartGame()
    {
        _isGameOver = true;
    }
}
