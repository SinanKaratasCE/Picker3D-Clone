using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelData : MonoBehaviour
{
    public string levelName;
    public int levelIndex;
    public int checkPointCount;
    public int collectableCountUntilCheckpoint;
    public float lastZPositionOfPlatform;
    public float lastZPositionOfCollectables;
    public LevelDifficulty levelDifficulty;
    
    public void CheckpointTargetCollectableCountSet(Checkpoint checkpoint)
    {
        if(collectableCountUntilCheckpoint <= 0)
            return;
        
        checkPointCount++;
        checkpoint.maxCollectableCount = collectableCountUntilCheckpoint;
        collectableCountUntilCheckpoint = 0;
    }
    
}
