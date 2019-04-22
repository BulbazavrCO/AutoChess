using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChess
{
    public enum TypeCell
    {
        union,
        enemy,
        netral
    }

    public interface ICell
    {
        int X { get; }

        int Y { get; }

        TypeCell typeCell { get; }
    }
}
