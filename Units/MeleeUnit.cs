using System.Collections;
using System.Collections.Generic;
using AutoChess;
using UnityEngine;

public class MeleeUnit : UnitControl
{
    protected override IEnumerator AttakUnit(Unit enemyUnit, float attakSpeed, float damage)
    {
        yield return new WaitForSeconds(attakSpeed);
        if(enemyUnit != null)
        {
            enemyUnit.Damage(damage);
            Debug.Log(unit.typeCell + " Damage by: " + enemyUnit.typeCell);
        }
        action = null;
    }
}
