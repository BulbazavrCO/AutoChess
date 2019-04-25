using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBullet", menuName = "Bullet")]
public class BulletParametrs : ScriptableObject
{
    public GameObject BulletPrefab;

    public GameObject Effect;

    public float Speed;   
}
