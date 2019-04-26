using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoChess;

public class MapController : MonoBehaviour
{
    private Map map;

    public static MapController instance { get; private set; }    

    private UnitControl selectUnit;

    private RandomEnemies enemies;

    private int wave;

    private void Start()
    {
        instance = this;
        map = new Map();
        enemies = GetComponent<RandomEnemies>();        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectUnit == null)
            {
                selectUnit = Selected();
                if(selectUnit != null)
                selectUnit.Selected();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (selectUnit != null)
            {
                selectUnit.SetPosition(GetPosition());
            }
        }

        if (Input.GetMouseButtonUp(0))
        {            
            if (selectUnit != null)
            {
                selectUnit.PositionOnMap(GetPosition().x, GetPosition().z);
                selectUnit = null;
            }
        }
    }    

    private Vector3 GetPosition()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag == "Plane")               
                return new Vector3(hits[i].point.x, 1.0f, hits[i].point.z);            
        }

        return Vector3.zero;
    }

    private UnitControl Selected()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            return hit.collider.GetComponent<UnitControl>();
        return null;

    }

    public void BuyUnit(UnitParametrs param)
    {
        if (map.stock.CheckStock(new Unit(param)))
        {
            CreateUnit(param, -1, -1, TypeCell.union);
        }
    }

    private Unit CreateUnit(UnitParametrs param, int x, int y, TypeCell type)
    {       
        Unit unit = new Unit(param, map, x, y, type);
        CreateUnit(unit, param.model);
        return unit;
    }

    private void CreateUnit(Unit unit, GameObject prefab)
    {
        GameObject go = Instantiate(prefab);
        var unitControl = go.AddComponent<MeleeUnit>();
        unitControl.Create(transform, unit);
    }

    private List<Unit> EnemiesUnits()
    {
        List<UnitParametrs> param = enemies.EnemiesUnits();

        List<Unit> units = new List<Unit>();

        foreach(var p in param)
        {
           units.Add(CreateUnit(p, 2, 2, TypeCell.enemy));
        }
        return units;
    }

    public void StartButtle()
    {
       map.CreateButtle(EnemiesUnits());
    }

    private IEnumerator CheckEndButtle()
    {
        while(map.CheckButtle())
        {
            yield return new WaitForSeconds(1);
        }
        EndButtle();
    }

    public void EndButtle()
    {
        map.EndButtle();
        List<Unit> units = map.UnionUnits;
        foreach(var unit in units)
        {
            CreateUnit(unit, unit.info.model);
        }
    }
}
