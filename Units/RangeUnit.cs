using System.Collections;
using System.Collections.Generic;
using AutoChess;
using UnityEngine;

public class RangeUnit : UnitControl
{    
    [SerializeField] private Transform targetSpawn;

    protected override IEnumerator AttakUnit(Unit enemyUnit, float attakSpeed, float damage)
    {
        yield return new WaitForSeconds(attakSpeed);
        Bullet bul = new GameObject().AddComponent<Bullet>();
        bul.MoveInUnit(enemyUnit, damage, param.bullet);
    }
}
