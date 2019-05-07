using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoChess;

public class MapController : MonoBehaviour
{
    private Map map;

    public static MapController instance { get; private set; }

    private UnitControl selectUnit;

    private RandomEnemies AllParametrs;

    private int wave;

    [SerializeField]
    private int MapTime = 30;

    [SerializeField]
    private int ButtleTime = 30;

    private void Start()
    {
        instance = this;
        map = new Map(MapTime, ButtleTime);
        AllParametrs = GetComponent<RandomEnemies>();
        StartCoroutine(MapTimeUpdate());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectUnit == null)
            {
                selectUnit = Selected();
                if (selectUnit != null)
                    selectUnit.Selected();
            }
        }

        if (!map.CheckButtle())
        {
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
        if (map.stock.CheckStock(new Unit(param.stats)))
        {
            CreateUnit(param, -1, -1, TypeCell.union, 1);
        }
    }

    private Unit CreateUnit(UnitParametrs param, int x, int y, TypeCell type, int level)
    {
        Unit unit = new Unit(param.stats, map, x, y, type, param.ID, level);
        CreateUnit(unit, param.model, param);
        return unit;
    }

    private void CreateUnit(Unit unit, GameObject prefab, UnitParametrs param)
    {
        GameObject go = Instantiate(prefab);
        var unitControl = go.AddComponent<MeleeUnit>();
        unitControl.Create(transform, unit, param, unit.Level);
    }

    private List<Unit> EnemiesUnits()
    {
        List<UnitParametrs> param = AllParametrs.EnemiesUnits();

        List<Unit> units = new List<Unit>();


        units.Add(CreateUnit(param[0], 7, 7, TypeCell.enemy,1));
        units.Add(CreateUnit(param[1], 7, 6, TypeCell.enemy,1));
        units.Add(CreateUnit(param[2], 6, 7, TypeCell.enemy,1));

        return units;
    }

    public void StartButtle()
    {
        map.CreateButtle(EnemiesUnits());
        StartCoroutine(CheckEndButtle());
    }

    private IEnumerator MapTimeUpdate()
    {
        while (map.CheckTime())
        {
            yield return new WaitForSeconds(1);
            map.UpdateTime();
            UIController.instance.UpdateTime("Map: " + map.time);
        }

        StartButtle();
    }

    private IEnumerator CheckEndButtle()
    {
        while (map.CheckButtle())
        {
            yield return new WaitForSeconds(1);
            map.UpdateTime();
            UIController.instance.UpdateTime("Buttle: " + map.time);
        }
        EndButtle();
    }

    public void EndButtle()
    {
        map.EndButtle();
        UnitParametrs param;
        foreach (Unit unit in map.CloneUnits)
        {
            param = AllParametrs.GetParametrs(unit.ID);
            CreateUnit(unit, param.model, param);
        }
        StartCoroutine(MapTimeUpdate());
    }
}
