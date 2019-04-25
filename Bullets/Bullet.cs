using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoChess;

public class Bullet : MonoBehaviour
{
    private Transform tr;    

    public void MoveInUnit(Unit unit, float damage, BulletParametrs param)
    {
        tr = GetComponent<Transform>();
        StartCoroutine(MoveBullet(unit, damage, param.Speed));
    }

    private IEnumerator MoveBullet(Unit unit, float damage, float speed)
    {
        Vector3 target = new Vector3(unit.X + 0.5f, 1, unit.Y + 0.5f);
        float dis = Vector3.Distance(tr.position, target);

        while (dis <= 0.2f)
        {
            yield return null;
            tr.position = Vector3.MoveTowards(tr.position, target, speed * Time.deltaTime);
        }

        unit.Damage(damage);
        Destroy(gameObject);
    }
}
