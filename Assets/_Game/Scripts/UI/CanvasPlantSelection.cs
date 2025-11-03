using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPlantSelection : UICanvas
{
    [SerializeField]private TextMeshProUGUI sun;
    [SerializeField]private Transform availablePlantsContainer;
    [SerializeField]private Transform selectedPlantsContainer;
    [SerializeField] private Transform select;
    

    public Transform GetAvailablePlantsContainer()
    {
        return availablePlantsContainer;
    }
    public Transform GetSelectedPlantsContainer()
    {
        return selectedPlantsContainer;
    }

    public void StartButton()
    {
        GameManager.Instance.ConfirmPlantSelection();
        select.gameObject.SetActive(false);
    }

    public void ShowPlantSelectionPanel()
    {
        select.gameObject.SetActive(true);
    }

    public void UpdateSunUI(int t)
    {
        sun.text = t.ToString();
    }


}
