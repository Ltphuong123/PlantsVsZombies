using UnityEngine;
public class PlayController : MonoBehaviour
{
    [SerializeField] private GridController gridManager;
    [SerializeField] private PlantCartControl plantCartControl;
    [SerializeField] private GameObject shovel;
    private PlantCartData selectedPlantData;
     private PlantBase plant;
    private bool isShovel = false;

    private Vector3 mouseWorldPos;

    public void OnInit()
    {
        DeselectAndDestroyGhost();
        CancleShovel();
    }

    public void OnCardClick(PlantCartData cardData)
    {
        if (isShovel)
        {
            CancleShovel();
        }

        if (selectedPlantData == null)
        {
            selectedPlantData = cardData;
            plant = CharacterManager.Instance.SpawnPlant(selectedPlantData.poolTypePlant, transform.position, Quaternion.identity);
        }

        else
        {
            if (cardData == selectedPlantData) DeselectAndDestroyGhost();
            else
            {
                DeselectAndDestroyGhost();
                selectedPlantData = cardData;
                plant = CharacterManager.Instance.SpawnPlant(selectedPlantData.poolTypePlant, transform.position, Quaternion.identity);
            }
        }

    }

    public void Shovel()
    {
        if (selectedPlantData != null) return;
        if (isShovel)
        {
            CancleShovel();
            return;
        }
        isShovel = true;
        shovel.SetActive(true);
    }
    public void CancleShovel()
    {
        isShovel = false;
        shovel.SetActive(false);
    }
    
    public void DeselectAndDestroyGhost()
    {
        if (selectedPlantData != null)
        {
            plant.OnDespawn();
            plant = null;
            selectedPlantData = null;
        }
    }

    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = -6f;
        if (plant != null)
        {
            plant.transform.position = mouseWorldPos;

            if (Input.GetMouseButtonDown(1))
            {
                DeselectAndDestroyGhost();
            }
        }
        if (isShovel) shovel.transform.position = mouseWorldPos;

        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    private void HandleMouseClick()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero);
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                Sun sun = hit.collider.GetComponent<Sun>();
                if (sun != null)
                {
                    sun.Collect();
                    return;
                }
            }
        }
    }

    public void AttemptToPlantAt(Cell cell)
    {
        if (!isShovel)
        {
            if (selectedPlantData == null)
            {
                return;
            }
            if (gridManager.IsCellOccupied(cell))
            {
                return;
            }
            gridManager.PlantObjectAt(cell, plant);
            plantCartControl.Startcooldown(selectedPlantData);
            LevelManager.Instance.AddSun(-selectedPlantData.sunCost);
            plant = null;
            selectedPlantData = null;
        }
        else
        {
           if (gridManager.IsCellOccupied(cell))
            {
                gridManager.RevomePlant(cell.x_coord,cell.y_coord);
                SoundManager.Instance.PlaySFX(FX.PlantingClip);
                CancleShovel();
            }
        }

    }
    public void OnDespawn()
    {
        DeselectAndDestroyGhost();
        CancleShovel();
        plantCartControl.OnDespawn();
    }
}