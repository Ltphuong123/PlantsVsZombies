using UnityEngine;

public interface PlantState
{
    void OnEnter(Plant plant);
    void OnExecute(Plant plant);
    void OnExit(Plant plant);
}