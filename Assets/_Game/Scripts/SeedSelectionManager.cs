// SeedSelectionManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeedSelectionManager : MonoBehaviour
{
    [Header("Dữ liệu & Prefab")]
    public List<PlantCartData> allAvailablePlants;
    public GameObject plantCardPrefab;

    [Header("Containers & Layers")]
    public Transform availablePlantsContainer;
    public Transform selectedPlantsContainer;
    public Transform animationLayer;

    [Header("Cài đặt")]
    public int maxSelectedPlants = 8;
    public float animationDuration = 0.3f;

    private List<PlantCartData> selectedPlants = new List<PlantCartData>();
    private Dictionary<PlantCartData, PlantCardUI> availableCardsMap = new Dictionary<PlantCartData, PlantCardUI>();
    private Dictionary<PlantCartData, PlantCardUI> selectedCardsMap = new Dictionary<PlantCartData, PlantCardUI>();

    void Start()
    {
        PopulateAvailablePlants();
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

    public void OnPlantCardClicked(PlantCardUI card)
    {
        if (availableCardsMap.ContainsKey(card.assignedPlantData)) SelectPlant(card);
        else if (selectedCardsMap.ContainsKey(card.assignedPlantData)) DeselectPlant(card);
    }

    private void SelectPlant(PlantCardUI originalCard)
    {
        if (selectedPlants.Count >= maxSelectedPlants) return;
        PlantCartData data = originalCard.assignedPlantData;
        selectedPlants.Add(data);
        availableCardsMap.Remove(data);
        StartCoroutine(AnimateCardSelection(originalCard));
    }

    private void DeselectPlant(PlantCardUI cardToRemove)
    {
        PlantCartData data = cardToRemove.assignedPlantData;
        selectedPlants.Remove(data);
        if (availableCardsMap.TryGetValue(data, out PlantCardUI originalCard))
        {
            originalCard.gameObject.SetActive(true);
        }
        selectedCardsMap.Remove(data);
        Destroy(cardToRemove.gameObject);
    }

    private IEnumerator AnimateCardSelection(PlantCardUI originalCard)
    {
        originalCard.gameObject.SetActive(false);
        GameObject dummyCardGO = Instantiate(plantCardPrefab, animationLayer);
        dummyCardGO.transform.position = originalCard.transform.position;
        dummyCardGO.GetComponent<PlantCardUI>().Setup(originalCard.assignedPlantData, this);
        GameObject finalCardGO = Instantiate(plantCardPrefab, selectedPlantsContainer);
        PlantCardUI finalCardUI = finalCardGO.GetComponent<PlantCardUI>();
        finalCardUI.Setup(originalCard.assignedPlantData, this);
        finalCardUI.GetComponent<CanvasGroup>().alpha = 0;
        selectedCardsMap.Add(originalCard.assignedPlantData, finalCardUI);
        yield return null;
        Vector3 startPosition = dummyCardGO.transform.position;
        Vector3 endPosition = finalCardGO.transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            dummyCardGO.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(dummyCardGO);
        finalCardUI.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void StartGame()
    {
        if (selectedPlants.Count == 0) return;
        // GameManager.instance.playerSelectedPlants = new List<PlantData>(selectedPlants);
        Debug.Log("Bắt đầu game với " + selectedPlants.Count + " cây!");
        // SceneManager.LoadScene("GameplayScene");
    }
}