using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ZombieState
{
    void OnEnter(ZombieBase zombie);
    void OnExecute(ZombieBase zombie);
    void OnExit(ZombieBase zombie);
}