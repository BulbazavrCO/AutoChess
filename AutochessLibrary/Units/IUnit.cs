using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AutoChess
{
    public interface IUnit
    {
        void LevelUp(int level);

        void Dead();

        void DestroyUnit();

        void UpdateHealth(float value);

        void StartButtle();

        void Attak(Unit enemyUnit, float attakSpeed, float damage, float attakRange);

        void Move(int x, int y);
    }
}
