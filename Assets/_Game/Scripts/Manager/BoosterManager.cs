using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoosterManager : Singleton<BoosterManager>
{
    [SerializeField] private PlayController gameplayManager;
    public void SelectShovel()
    {
        gameplayManager.Shovel();
    }
}

