using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameBoard gameBoard;
    [SerializeField] private List<GameObject> ballPrefabs;


    public GameStatus CurrentStatus
    {
        get
        {
            return _currentStatus;
        }
        set
        {
            _currentStatus = value;
        }
    }

    public List<Ball> Balls => _balls;
    public GameBoard GameBoard => gameBoard;

    public static Action OnGameStarted;

    private GameStatus _currentStatus;
    private List<Ball> _balls;

    private void Start()
    {
        _currentStatus = GameStatus.NotStarted;
        _balls = new List<Ball>();
    }

    private void OnEnable()
    {
        OnGameStarted += GameStarted;
    }

    private void OnDisable()
    {
        OnGameStarted -= GameStarted;
    }

    private void GameStarted()
    {
        _currentStatus = GameStatus.Started;
        gameBoard.Activate();
        CreateBalls();
    }

    private void CreateBalls()
    {
        foreach (var ball in ballPrefabs)
        {
            var b = Instantiate(ball).GetComponent<Ball>();
            _balls.Add(b);
        }
    }
}
