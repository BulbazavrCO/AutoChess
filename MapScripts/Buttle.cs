using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttle 
{
    public List<Unit> EnemyUnits { get; private set; }
    public List<Unit> UnionUnits { get; private set; }


    public Buttle(List<Unit> union, List<Unit> enemies)
    {
        EnemyUnits = enemies;
        UnionUnits = union;
    }
}
