using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Sprite[] _liveSprites;
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
            Debug.LogError("GameManager is NULL");
    }

    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: "+ playerScore;
    }

    public void UpdateLives(int currentLive)
    {
        _liveImage.sprite = _liveSprites[currentLive];

        if(currentLive == 0)
            GameOverSequence();
    }

    void GameOverSequence()
    {
        _gameManager.RestartGame();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
    }
}
