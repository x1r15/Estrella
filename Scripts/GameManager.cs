using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState State { get; } = GameState.InProgress;

    private List<IControlledGameService> _controlledServices = new List<IControlledGameService>();

    private void Start()
    {
        _controlledServices.Add(ServiceLocator.Get<DifficultyController>());
        _controlledServices.Add(ServiceLocator.Get<MeteorSpawner>());
        _controlledServices.Add(ServiceLocator.Get<PointsController>());
        foreach (var controlledGameService in _controlledServices)
        {
            controlledGameService.Enable();
        }
    }
}

public enum GameState
{
    NotStarted,
    InProgress,
    Paused,
    Finished
}
