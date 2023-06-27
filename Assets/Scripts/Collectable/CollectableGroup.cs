using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroupType
{
    SquareGroup = 1,
    RectangleGroup = 2,
    XGroup = 3,
    ArrowGroup = 4,
    PickerUpgrade = 5
}
public class CollectableGroup : MonoBehaviour
{
    public GroupType groupType;
    public float minGroupLenght;
    public float maxGroupLenght;
    public int ballCount;
}
