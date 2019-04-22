using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit 
{
    void LevelUp(int level);

    void Dead();

    void DestroyUnit();

    void UpdateHealth(float value);

    void StartButtle();    
}
