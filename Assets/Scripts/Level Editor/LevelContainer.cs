using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelContainer", menuName = "Level/LevelContainer", order = 0)]
public class LevelContainer : ScriptableObject
{
    public List<LevelData> levels;
}