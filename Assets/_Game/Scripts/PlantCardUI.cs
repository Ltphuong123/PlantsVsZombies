using UnityEngine;
using UnityEngine.UI;

public class PlantCardUI : MonoBehaviour
{
    public PlantCartData assignedPlantData;
    public Image cardImage; // Dùng để hiển thị ảnh gói hạt
    private SeedSelectionManager selectionManager;

    public void Setup(PlantCartData data, SeedSelectionManager manager)
    {
        assignedPlantData = data;
        selectionManager = manager;
        if (cardImage != null) cardImage.sprite = data.seedPacketSprite;
    }

    public void OnCardClicked(){
         selectionManager.OnPlantCardClicked(this);
    }
  
}