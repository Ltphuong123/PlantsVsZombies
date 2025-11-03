using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform flagsContainer;
    
    public void SettingButton()
    {
        GameManager.Instance.OnSettings();
        UIManager.Instance.OpenUI<CanvasSetting>().SetState(this);
    }

    public void ShovelButton()
    {
        BoosterManager.Instance.SelectShovel();
    }

    public void UpdatePregess(float t)
    {
        slider.value = t;
    }

    public Transform GetFlagsContainer()
    {
        return flagsContainer;
    }


}
