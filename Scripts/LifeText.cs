using TMPro;
using UnityEngine;

public class LifeText : MonoBehaviour
{
    private TMP_Text _text;
    private string _expectedText;
    private string _textInput = "";
    private InputManager _inputManager;
    private bool _completed;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        _inputManager = ServiceLocator.Get<InputManager>();
        _inputManager.OnInput += InputManager_OnInput;
    }

    public void Init(string term)
    {
        _expectedText = term.ToLower();
        _text.text = _expectedText;
    }

    private void OnDestroy()
    {
        _inputManager.OnInput -= InputManager_OnInput;
    }

    private void InputManager_OnInput(char c)
    {
        if (_completed) return;
        
        _textInput += c;
        _textInput = _textInput.ToLower();
        var highlightedText = "";
        if (!_expectedText.StartsWith(_textInput))
        {
            _textInput = "";
            highlightedText = _expectedText;
        }
        else
        {
            var textLeft = _expectedText.Substring(
                _textInput.Length, 
                _expectedText.Length - _textInput.Length);
            
            highlightedText = 
                $"<color=#FFA400>{_textInput}</color>{textLeft}";
        }
        _text.SetText(highlightedText.Replace(" ", "\n"));

        if (_expectedText == _textInput)
        {
            GetComponent<IAction>().Act();
            _completed = true;
        }
    }
}
