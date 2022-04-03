using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState State { get; private set; } = GameState.NotStarted;

    private List<IControlledGameService> _controlledServices = new List<IControlledGameService>();
    private Earth _earth;
    private SoundManager _soundManager;

    [SerializeField]
    private CanvasGroup _startPanel;

    [SerializeField]
    private CanvasGroup _endGamePanel;
    
    [SerializeField]
    private CanvasGroup _gameElementsPanel;

    [SerializeField]
    private CanvasGroup _readyStartPanel;

    private static readonly int StartTrigger = Animator.StringToHash("Start");

    private void Awake()
    {
        _soundManager = ServiceLocator.Get<SoundManager>();
    }
    private void Start()
    {
        _earth = ServiceLocator.Get<Earth>();
        _earth.OnDestroyed += Earth_OnDestroyed;
        _controlledServices.Add(ServiceLocator.Get<DifficultyController>());
        _controlledServices.Add(ServiceLocator.Get<MeteorSpawner>());
        _controlledServices.Add(ServiceLocator.Get<PointsController>());
        StartCoroutine(_startPanel.FadeIn());
    }

    private void Update()
    {
        if (State == GameState.NotStarted && _startPanel.alpha.Equals(1f) && Input.anyKeyDown)
        {
            ResetGame();
        }
    }

    private void Earth_OnDestroyed()
    {
        State = GameState.Finished;
        foreach (var controlledGameService in _controlledServices)
        {
            controlledGameService.Disable();
        }

        foreach (var meteor in Meteor.All)
        {
            meteor.Destroy();
        }

        if (Ufo.Instance != null)
        {
            Destroy(Ufo.Instance.gameObject);   
        }
        
        _endGamePanel.GetComponentInChildren<UI_LearnMoreButton>().SetTerm();
        _endGamePanel.GetComponentInChildren<UI_AchievedScore>().SetScore();
        StartCoroutine(_endGamePanel.FadeIn());
        StartCoroutine(_gameElementsPanel.FadeOut());
    }

    public void ResetGame()
    {
        _soundManager.Play(SoundManager.Sounds.ButtonClicked);
        if (State == GameState.Finished)
        {
            _earth.Reset();
        }
        State = GameState.InProgress;

        foreach (var controlledGameService in _controlledServices)
        {
            controlledGameService.Reset();
        }

        
        StartCoroutine(_startPanel.FadeOut());
        StartCoroutine(_gameElementsPanel.FadeIn());
        StartCoroutine(_endGamePanel.FadeOut());
        _readyStartPanel.GetComponent<Animator>().SetTrigger(StartTrigger);
    }
}

public enum GameState
{
    NotStarted,
    InProgress,
    Paused,
    Finished
}
