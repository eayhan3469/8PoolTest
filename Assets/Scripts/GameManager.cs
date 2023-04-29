using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameBoard gameBoard;
    [SerializeField] private List<Material> ballMaterials;
    [SerializeField] private GameObject ballPrefab;

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

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    public List<Ball> Balls => _balls;
    public GameBoard GameBoard => gameBoard;
    public List<Material> BallMaterials => ballMaterials;

    public Action OnGameStarted;
    public Action OnGameFinished;
    public Action<int> OnScoreUpdated;

    private GameStatus _currentStatus;
    private List<Ball> _balls;
    private int _score = 0;

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

    public void CheckGameIsFinished()
    {
        if (_balls.Count == 0)
        {
            GameFinished();
        }
    }

    private void GameStarted()
    {
        _currentStatus = GameStatus.Started;
        gameBoard.Activate();
        CreateBalls();
    }

    private void GameFinished()
    {
        _currentStatus = GameStatus.Finished;
        gameBoard.Deactivate();
        OnGameFinished?.Invoke();
    }

    private void CreateBalls()
    {
        for (int i = 0; i < ballMaterials.Count; i++)
        {
            var ball = Instantiate(ballPrefab).GetComponent<Ball>();
            ball.Number = i;
            _balls.Add(ball);
        }
    }
}
