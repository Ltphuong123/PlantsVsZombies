using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        ZombieBase zombie = other.GetComponent<ZombieBase>();
        if (zombie != null)
        {
            GameManager.Instance.OnLose();
        }
    }
}
