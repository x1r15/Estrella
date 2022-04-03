using TMPro;
using UnityEngine;

public class UI_AchievedScore : MonoBehaviour
{
    public void SetScore()
    {
        var pointsController = ServiceLocator.Get<PointsController>();
        GetComponent<TMP_Text>().SetText(pointsController.Year + " AD");
    }
}
