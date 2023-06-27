using System;
using UnityEngine;

public enum PlatformType
{
    Empty = 1,
    Checkpoint = 2,
    Finish = 3
}
public class Platform : MonoBehaviour
{
    public PlatformType platformType;
    public float platformLenght;

}
