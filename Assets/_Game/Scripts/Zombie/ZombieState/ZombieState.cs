using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ZombieState
{
    void OnEnter(Zombie zombie);
    void OnExecute(Zombie zombie);
    void OnExit(Zombie zombie);
}