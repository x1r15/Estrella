using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HpBar : MonoBehaviour
{
    private float _initialWidth;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        _initialWidth = _rectTransform.rect.width;
        ServiceLocator.Get<Earth>().OnDamage += Earth_OnDamage;
    }

    private void SetNewWidth(int newHp)
    {
        var newWidth = newHp * _initialWidth / 100;
        _rectTransform.sizeDelta = new Vector2(newWidth, _rectTransform.rect.height);
    }

    private void Earth_OnDamage(object sender, DamageEventArgs args)
    {
        SetNewWidth(args.HpLeft);
    }
}
