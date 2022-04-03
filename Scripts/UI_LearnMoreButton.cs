using TMPro;
using UnityEngine;

public class UI_LearnMoreButton : MonoBehaviour
{
    private Vocabulary _vocabulary;
    private TMP_Text _text;
    private string _term;
    private SoundManager _soundManager;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        _soundManager = ServiceLocator.Get<SoundManager>();
        _vocabulary = ServiceLocator.GetSafe<Vocabulary>();
    }

    [ContextMenu("SetRandomTerm")]
    public void SetTerm()
    {
        _term = _vocabulary.GetRandomTerm();
        _text.SetText(_term);
    }

    public void OpenSearch()
    {
        _soundManager.Play(SoundManager.Sounds.ButtonClicked);
        Application.OpenURL($"https://www.google.com/search?q=Astronomy+What+Is+{_term.Replace(" ", "+")}");
    }
}
