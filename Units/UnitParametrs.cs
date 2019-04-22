using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region AllClassUnits

public enum ClassUnits
{

}

#endregion

#region AllRaceUnits

public enum RaceUnits
{

}

#endregion

#region TypeAttakUnit

public enum TypeAttakUnit
{
    range,
    melee
}

#endregion


[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit")]
public class UnitParametrs : ScriptableObject
{
    public List<Parametrs> parametrs;

    public new string name;   

    public float maxMP;

    public int LevelUnit;    

    public int armor;
    public int magicArmor;
    public int attakRange = 1;

    public GameObject model;

    public TypeAttakUnit attakUnit;

    public Skill skill;

    public RaceUnit[] AllRace;

    public ClassUnit[] AllClasses;   
}

#region UnitParametrs

[System.Serializable]
public class Parametrs
{
    public float damage;

    public float maxHP;

    public float attakSpeed = 1;
}

#endregion

#region AllRacesUnits

[System.Serializable]
public class RaceUnit
{
    public RaceUnits Race;

    public string discription;

    public float value;
}

#endregion

#region AllClassesUnits

[System.Serializable]
public class ClassUnit
{
    public ClassUnits Race;

    public string discription;

    public float value;
}

#endregion
