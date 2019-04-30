using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoChess;

[RequireComponent(typeof(Animator))]
public abstract class UnitControl : MonoBehaviour, IUnit
{
    protected Animator anim;
    private Transform tr;

    protected Unit unit;

    private Vector3 movePos;

    protected IEnumerator action = null;   

    private bool buttle = false;

    protected UnitParametrs param;

    public void SetPosition(Vector3 pos)
    {
        tr.position = pos;
    }

    public void PositionOnMap(float x, float y)
    {
        tr.position = Pos(unit.CreateTo(x, y));
    }

    private Vector3 Pos ((int x, int y) pos)
    {
        if (pos.y < 0)
            return new Vector3(pos.x + 0.5f, 1.5f, -1);

        return new Vector3(pos.x + 0.5f, 1, pos.y + 0.5f);
    }

    public void Selected()
    {
        unit.RemoveUnitPosition();
    }    

    public void Create(Transform parrent, Unit unit, UnitParametrs param)
    {       
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();        
        tr.SetParent(parrent);
        this.unit = unit;

        unit.AddControl(this);
        PositionOnMap(unit.X, unit.Y);
    }      

    public void DestroyUnit()
    {
        Destroy(gameObject);
    }

    public void LevelUp(int level)
    {
        Renderer r = GetComponent<MeshRenderer>();       

        r.material.color = level == 2 ? Color.red : Color.blue;
        Debug.Log(level);
    }

    public void StartButtle()
    {
        buttle = true;
        StartCoroutine(UpdateButtle());        
    }    

    public void UpdateHealth(float value)
    {
        Debug.Log(unit.stats.name + " " + unit.typeCell +   "  hp:" + value);
    }

    public void Dead()
    {
        buttle = false;
        StartCoroutine(DeadUnit());
    }   

    private void OnMouseDown()
    {
        
    }

    public void Attak(Unit enemyUnit, float attakSpeed, float damage)
    {
        action = AttakUnit(enemyUnit, attakSpeed, damage);

        Debug.Log(unit.typeCell + " " + damage);
    }

    public void Move(int x, int y)
    {
        action = MoveUnit(x, y);        
    }

    private IEnumerator DeadUnit()
    {
        yield return new WaitForSeconds(2.0f);
        DestroyUnit();
    }
    
    private IEnumerator UpdateButtle()
    {        
        while(buttle)
        {
            if (action == null)
                unit.ActionUnit();
            else
                yield return StartCoroutine(action);            
        }
    }    

    protected abstract IEnumerator AttakUnit(Unit enemyUnit,float attakSpeed, float damage);

    private IEnumerator MoveUnit(int x, int y)
    {
        yield return null;
        action = null;
    }    
}
