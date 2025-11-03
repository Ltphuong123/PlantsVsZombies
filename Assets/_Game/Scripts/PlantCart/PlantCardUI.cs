using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlantCardUI : MonoBehaviour
{
    [SerializeField] private PlantCartData assignedPlantData;
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardImage1;
    [SerializeField] private Image cardImage2;

    private bool isOnCooldown;
    private bool isshow;
    private PlantCartControl selectionManager;
    private Sprite sprite;

    public void Setup(PlantCartData data, PlantCartControl manager)
    {
        assignedPlantData = data;
        selectionManager = manager;
        sprite = data.seedPacketSprite;
        if (cardImage != null) cardImage.sprite = data.seedPacketSprite;
        isOnCooldown = false;
        isshow = true;
    }

    public void OnCardClicked()
    {
        if (!isOnCooldown && isshow) selectionManager.OnPlantCardClicked(assignedPlantData);
    }

    public void HideCard()
    {
        cardImage1.fillAmount = 1;
        isshow = false;
    }
    public void ShowCard()
    {
        cardImage1.fillAmount = 0;
        isshow = true;
    }


    public void StartCooldown()
    {
        isOnCooldown = true;
        cardImage2.fillAmount = 1;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        float duration = assignedPlantData.rechargeTime;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cardImage2.fillAmount = 1f - (elapsedTime / duration);
            yield return null;
        }

        cardImage2.fillAmount = 0;
        isOnCooldown = false;
    }
}
