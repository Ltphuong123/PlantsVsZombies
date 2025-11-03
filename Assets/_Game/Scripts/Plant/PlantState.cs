using UnityEngine;

public interface PlantState
{
    void OnEnter(PlantBase plant);
    void OnExecute(PlantBase plant);
    void OnExit(PlantBase plant);
}