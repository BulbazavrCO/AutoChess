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

    private IEnumerator updateButtle;

    private Unit enemyUnit;

    public void SetPosition(Vector3 pos)
    {
        tr.position = pos;
    }

    public void PositionOnMap(float x, float y)
    {
        tr.position = Pos(unit.MoveTo(x, y));
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

    public void Create(Transform parrent, Unit unit)
    {
        updateButtle = UpdateButtle();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();        
        tr.SetParent(parrent);
        this.unit = unit;        

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
    }

    public void StartButtle()
    {        
        StartCoroutine(updateButtle);
    }    

    public void UpdateHealth(float value)
    {
        Debug.Log(unit.info.name + " hp:" + value);
    }

    public void Dead()
    {
        
    }   

    private void OnMouseDown()
    {
        
    }
    
    private IEnumerator UpdateButtle()
    {
        enemyUnit = unit.GetEnemy();
        while(true)
        {
            IEnumerator function = CreateFunction();
            yield return StartCoroutine(function);
        }
    }

    private IEnumerator CreateFunction()
    {
        IEnumerator move = MoveUnit();
        return move;
    }

    protected abstract IEnumerator AttakUnit();

    private IEnumerator MoveUnit()
    {
        yield return null;
        
    }

  
}
