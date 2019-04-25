using System.Collections;
using System.Collections.Generic;
using AutoChess;
using UnityEngine;

public class MeleeUnit : UnitControl
{
    protected override IEnumerator AttakUnit(Unit unit, float attakSpeed, float damage, float rangeAttak)
    {
        yield return null;
    }
}
