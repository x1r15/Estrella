using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState State { get; } = GameState.InProgress;

    private List<IControlledGameService> _controlledServices = new List<IControlledGameService>();
    private Earth _earth;

    [SerializeField]
    private CanvasGroup _menuPanel;

    [SerializeField]
    private CanvasGroup _endGamePanel;
    
    [SerializeField]
    private CanvasGroup _gameElementsPanel;

    [SerializeField]
    private CanvasGroup _startGamePanel;

    private void Start()
    {
        _earth = ServiceLocator.Get<Earth>();
        _earth.OnDestroyed += Earth_OnDestroyed;
        _controlledServices.Add(ServiceLocator.Get<DifficultyController>());
        _controlledServices.Add(ServiceLocator.Get<MeteorSpawner>());
        _controlledServices.Add(ServiceLocator.Get<PointsController>());
        foreach (var controlledGameService in _controlledServices)
        {
            controlledGameService.Enable();
        }
    }

    private void Earth_OnDestroyed()
    {
        foreach (var controlledGameService in _controlledServices)
        {
            controlledGameService.Disable();
        }

        foreach (var meteor in Meteor.All)
        {
            meteor.Destroy();
        }
        _endGamePanel.GetComponentInChildren<UI_LearnMoreButton>().SetTerm();
        _endGamePanel.GetComponentInChildren<UI_AchievedScore>().SetScore();
        StartCoroutine(_endGamePanel.FadeIn());
        StartCoroutine(_gameElementsPanel.FadeOut());
    }

    public void ResetGame()
    {
        foreach (var controlledGameService in _controlledServices)
        {
            controlledGameService.Reset();
        }

        _earth.Reset();
        StartCoroutine(_endGamePanel.FadeOut());
        StartCoroutine(_gameElementsPanel.FadeIn());
        _startGamePanel.GetComponent<Animator>().SetTrigger("Reset");
    }
}

public enum GameState
{
    NotStarted,
    InProgress,
    Paused,
    Finished
}
