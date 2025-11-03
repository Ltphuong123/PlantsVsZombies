using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantCartControl : MonoBehaviour
{
    [SerializeField] private List<PlantCartData> allAvailablePlants;
    [SerializeField] private GameObject plantCardPrefab;
    [SerializeField] private int maxSelectedPlants = 8;
    [SerializeField] private PlayController gameplayManager;
    private Transform availablePlantsContainer;
    private Transform selectedPlantsContainer;
    private CanvasPlantSelection canvasPlantSelection;
    private Dictionary<PlantCartData, PlantCardUI> availableCardsMap = new Dictionary<PlantCartData, PlantCardUI>();
    private Dictionary<PlantCartData, PlantCardUI> selectedCardsMap = new Dictionary<PlantCartData, PlantCardUI>();
    private int currentSun;

    public void OnInit()
    {
        availableCardsMap.Clear();
        selectedCardsMap.Clear();

        canvasPlantSelection = UIManager.Instance.OpenUI<CanvasPlantSelection>();
        canvasPlantSelection.ShowPlantSelectionPanel();
        availablePlantsContainer = canvasPlantSelection.GetAvailablePlantsContainer();
        selectedPlantsContainer = canvasPlantSelection.GetSelectedPlantsContainer();
        PopulateAvailablePlants();
        canvasPlantSelection.Close(0f);
    }

    public void OnInitSun(int sun)
    {
        currentSun = sun;
        canvasPlantSelection.UpdateSunUI(currentSun);
    }

    public void AddSun(int s)
    {
        currentSun += s;
        UpdateSunUI(currentSun);
    }

    void PopulateAvailablePlants()
    {
        foreach (PlantCartData plant in allAvailablePlants)
        {
            GameObject cardGO = Instantiate(plantCardPrefab, availablePlantsContainer);
            PlantCardUI cardUI = cardGO.GetComponent<PlantCardUI>();
            cardUI.Setup(plant, this);
            availableCardsMap.Add(plant, cardUI);
        }
    }

    public void UpdateSunUI(int sun)
    {
        canvasPlantSelection.UpdateSunUI(sun);
        UpdateCardAvailability();
    }

    public void OnPlantCardClicked(PlantCartData cardData)
    {
        SoundManager.Instance.PlaySFX(FX.selectClip);
        if (selectedCardsMap.ContainsKey(cardData)) OnclickselectedCardsMap(cardData);
        else OnclickavailableCardsMap(cardData);
    }

    private void OnclickavailableCardsMap(PlantCartData cardData)
    {
        if (selectedCardsMap.Count >= maxSelectedPlants) return;
        GameObject cardGO = Instantiate(plantCardPrefab, selectedPlantsContainer);
        PlantCardUI cardUI = cardGO.GetComponent<PlantCardUI>();
        cardUI.Setup(cardData, this);
        availableCardsMap[cardData].HideCard();
        selectedCardsMap.Add(cardData, cardUI);
    }

    private void OnclickselectedCardsMap(PlantCartData cardData)
    {
        if (GameManager.Instance.IsState(GameState.Pause))
        {
            PlantCardUI cardUI = selectedCardsMap[cardData];
            Destroy(cardUI.gameObject);
            selectedCardsMap.Remove(cardData);
            availableCardsMap[cardData].ShowCard();
        }
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            gameplayManager.OnCardClick(cardData);
        }
    }

    public void Startcooldown(PlantCartData cardData)
    {
        selectedCardsMap[cardData].StartCooldown();
    }

    public void HideAllPlantCards()
    {
        foreach (KeyValuePair<PlantCartData, PlantCardUI> pair in selectedCardsMap)
        {
            pair.Value.HideCard();
        }
    }

    public void UpdateCardAvailability()
    {
        foreach (KeyValuePair<PlantCartData, PlantCardUI> pair in selectedCardsMap)
        {
            PlantCartData plantData = pair.Key;
            PlantCardUI cardUI = pair.Value;
            if (plantData.sunCost > currentSun) cardUI.HideCard();
            else cardUI.ShowCard();
        }
    }

    public void OnDespawn()
    {
        if (selectedCardsMap.Count != 0)
        {
            foreach (KeyValuePair<PlantCartData, PlantCardUI> pair in selectedCardsMap)
            {
                Destroy(pair.Value.gameObject);
            }
        }
        if (availableCardsMap.Count != 0)
        {
            foreach (KeyValuePair<PlantCartData, PlantCardUI> pair in availableCardsMap)
            {
                Destroy(pair.Value.gameObject);
            }
        }

        availableCardsMap.Clear();
        selectedCardsMap.Clear();
        canvasPlantSelection = null;
    }
}