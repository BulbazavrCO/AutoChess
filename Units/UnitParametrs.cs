using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoChess.Parametrs;


[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit")]
public class UnitParametrs : ScriptableObject
{
    public int ID;

    public UnitStats stats;        

    public GameObject model;    

    public Skill skill;

    public BulletParametrs bullet;

    public int Level { get => stats.LevelUnit; }
}

