using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Sprite[] _liveSprites;
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
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
            _gameOverText.gameObject.SetActive(true);
    }
}
