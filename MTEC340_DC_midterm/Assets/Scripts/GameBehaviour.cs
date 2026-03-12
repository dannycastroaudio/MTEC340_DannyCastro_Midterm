using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public static GameBehaviour Instance;
    private Utilities.GameState _gameMode;
    public Utilities.GameState GameMode
    {
        get => _gameMode;
        set
        {
            _gameMode = value;
            _pauseUI.enabled = GameMode != Utilities.GameState.Play;
        }
    }

    [SerializeField] private ScoreBehaviour _playerScore;
    [SerializeField] private TMP_Text _pauseUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        GameMode = Utilities.GameState.Play;
        _playerScore.Score = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //press p to pause
        {
            GameMode = GameMode == Utilities.GameState.Play ? Utilities.GameState.Pause :  Utilities.GameState.Play;
        }
        
        if (GameMode == Utilities.GameState.GameOver)
        {
            //SceneManager.LoadScene("GameOverScreen");
            //SceneManager.UnloadSceneAsync("snake_midterm");
        }
        
    }

    public void Score()
    {
        _playerScore.Score++; //take the player score and add 1
    }

    public void ResetScore()
    {
        _playerScore.Score = 0;
    }
}

