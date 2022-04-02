using TMPro;
using UnityEngine;

public class UI_YearCounter : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        ServiceLocator.Get<PointsController>().OnNewYear += PointsController_OnNewYear;
    }

    private void PointsController_OnNewYear(int year)
    {
        _text.SetText(year + "AD");
    }
}
