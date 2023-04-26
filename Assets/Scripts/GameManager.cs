using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject gameBoard;

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

    public static Action OnGameStarted;

    private GameStatus _currentStatus;

    private void Start()
    {
        _currentStatus = GameStatus.NotStarted;
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
        gameBoard.SetActive(true);
    }
}
