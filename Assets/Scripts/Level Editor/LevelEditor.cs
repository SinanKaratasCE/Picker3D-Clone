using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    [Header("Create Level Vars")] public GameObject emptyLevelPrefab;
    [SerializeField] private LevelContainer levelContainer;
    private GameObject _currentLevel;
    private int _currentLevelIndex;
    private LevelData _levelData = null;

    [Header("Add Object Vars")] private GroupType groupType;
    private PlatformType platformType;
    public List<CollectableGroup> collectableGroupPrefabs;
    public List<Platform> platformPrefabs;

    [Header("Get Level Vars")] private int _getLevelIndex;


    public void AddCollectableObject()
    {
        if (_currentLevel == null)
            return;

        InstantiateCollectables();

        SaveLevel();
    }

    public void AddPlatformObject()
    {
        if (_currentLevel == null)
            return;

        InstantiatePlatform();

        SaveLevel();
    }

    private void InstantiateCollectables()
    {
        if (!GetRightCollectablesPrefab())
        {
            Debug.LogWarning($"Index is not found in collectable types");
            return;
        }


        //Temp values
        var collectableReference = GetRightCollectablesPrefab().GetComponent<CollectableGroup>();
        var randomXPosition = Random.Range(-7.8f, 3.8f);
        var randomZPosition = Random.Range(collectableReference.minGroupLenght,
            collectableReference.maxGroupLenght);


        //Instantiate
        var tempObject = Instantiate(GetRightCollectablesPrefab(), new Vector3(randomXPosition, 0,
                _levelData.lastZPositionOfCollectables +
                randomZPosition),
            Quaternion.identity);
        tempObject.transform.SetParent(_currentLevel.transform);


        CastRayToCheckpoint(tempObject);

        _levelData.lastZPositionOfCollectables +=
            randomZPosition;

        if (tempObject == null)
            return;

        AddCollectableUpToCheckpoint(tempObject);
    }

    private void CheckpointTargetCollectableCountSet(GameObject go)
    {
        _levelData.CheckpointTargetCollectableCountSet(go.GetComponent<Checkpoint>());
    }

    private void AddCollectableUpToCheckpoint(GameObject go)
    {
        _levelData.collectableCountUntilCheckpoint += go.GetComponent<CollectableGroup>().ballCount;
    }

    private void CastRayToCheckpoint(GameObject go)
    {
        var ray = new Ray(go.transform.position, Vector3.up);
        var hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 50))
        {
            if (hit.collider.CompareTag(Strings.CheckpointTag))
            {
                Debug.Log($"Hit Checkpoint and Destroyed");
                CheckpointTargetCollectableCountSet(hit.collider.gameObject);
                DestroyImmediate(go);
            }
        }
    }

    private void InstantiatePlatform()
    {
        if (!GetRightPlatformPrefab())
        {
            Debug.LogWarning($"Index is not found in platform types");
            return;
        }


        var tempObject = Instantiate(GetRightPlatformPrefab(),
            new Vector3(0, -0.5f,
                _levelData.lastZPositionOfPlatform +
                GetRightPlatformPrefab().GetComponent<Platform>().platformLenght / 2), Quaternion.Euler(0, 90, 0));
        tempObject.transform.SetParent(_currentLevel.transform);
        _levelData.lastZPositionOfPlatform += GetRightPlatformPrefab().GetComponent<Platform>().platformLenght;
    }


    private GameObject GetRightCollectablesPrefab()
    {
        foreach (var collectables in collectableGroupPrefabs)
        {
            if (groupType == collectables.groupType)
                return collectables.gameObject;
        }

        return null;
    }


    private GameObject GetRightPlatformPrefab()
    {
        foreach (var platformPrefab in platformPrefabs)
        {
            if (platformType == platformPrefab.platformType)
                return platformPrefab.gameObject;
        }

        return null;
    }

    public void CreateNewLevel(int createLevelIndex)
    {
        _currentLevelIndex = createLevelIndex;
        GetLevelContainer();

        if (levelContainer.levels[createLevelIndex] != null)
        {
            Debug.LogWarning($"This level is exist! Use get level: {createLevelIndex}");
            return;
        }

        IsSceneEmptyCheck();

        InstantiateAndInitializeLevel();
    }

    private void InstantiateAndInitializeLevel()
    {
        _currentLevel = Instantiate(emptyLevelPrefab, Vector3.zero, Quaternion.identity);
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        _currentLevel.name = $"Level {_currentLevelIndex}";
        _levelData = _currentLevel.GetComponent<LevelData>();
        _levelData.lastZPositionOfPlatform = 75f;
        _levelData.lastZPositionOfCollectables = 0f;
        _levelData.levelIndex = _currentLevelIndex;
        _levelData.levelName = $"Level {_currentLevelIndex}";
        _levelData.checkPointCount = 0;
        SaveLevel();
    }

    private void IsSceneEmptyCheck()
    {
        if (_currentLevel != null)
        {
            DestroyImmediate(_currentLevel);
            _levelData = null;
        }
    }

    public void GetLevel()
    {
        GetLevelContainer();
        if (levelContainer.levels[_getLevelIndex] == null)
        {
            Debug.LogWarning($"This level is not exist: {_getLevelIndex}");
            return;
        }

        IsSceneEmptyCheck();

        _currentLevel = Instantiate(GetLevelPrefab(), Vector3.zero, Quaternion.identity);
        _currentLevelIndex = _getLevelIndex;
        _levelData = _currentLevel.GetComponent<LevelData>();
    }


    public void ChangeWantedLevelIndex(int index)
    {
        _getLevelIndex = index;
    }

    public void ChangePlatformToAdd(int enumIndex)
    {
        switch (enumIndex)
        {
            case (int)PlatformType.Empty:
                platformType = PlatformType.Empty;
                break;
            case (int)PlatformType.Checkpoint:
                platformType = PlatformType.Checkpoint;
                break;
            case (int)PlatformType.Finish:
                platformType = PlatformType.Finish;
                break;
            default:
                Debug.LogWarning($"This enum index is not defined: {enumIndex}");
                break;
        }
    }

    public void ChangeCollectableGroupToAdd(int enumIndex)
    {
        switch (enumIndex)
        {
            case (int)GroupType.SquareGroup:
                groupType = GroupType.SquareGroup;
                break;
            case (int)GroupType.RectangleGroup:
                groupType = GroupType.RectangleGroup;
                break;
            case (int)GroupType.XGroup:
                groupType = GroupType.XGroup;
                break;
            case (int)GroupType.ArrowGroup:
                groupType = GroupType.ArrowGroup;
                break;
            case (int)GroupType.PickerUpgrade:
                groupType = GroupType.PickerUpgrade;
                break;
            default:
                Debug.LogWarning($"This enum index is not defined: {enumIndex}");
                break;
        }
    }


    public void SaveLevel()
    {
        if (_currentLevel == null || _levelData == null)
        {
            Debug.LogWarning($"current level is null or level data  cannot be null");
            return;
        }

        string prefabPath =
            $"Assets/Prefabs/Levels/Level{_currentLevel.GetComponent<LevelData>().levelIndex}.prefab";

        // Delete the assets if they already exist.
        AssetDatabase.DeleteAsset(prefabPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();


        PrefabUtility.CreatePrefab(prefabPath, _currentLevel);

        SaveLevelContainer();
    }


    private void GetLevelContainer()
    {
        string[] result = AssetDatabase.FindAssets("LevelContainerSO");
        string path = AssetDatabase.GUIDToAssetPath(result[0]);
        levelContainer = (LevelContainer)AssetDatabase.LoadAssetAtPath(path, typeof(LevelContainer));
    }

    private void SaveLevelContainer()
    {
        if (!GetLevelPrefab())
            return;
        levelContainer.levels[_currentLevelIndex] = GetLevelPrefab().GetComponent<LevelData>();
        EditorUtility.SetDirty(levelContainer);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private GameObject GetLevelPrefab()
    {
        string[] result = AssetDatabase.FindAssets($"Level{_currentLevelIndex}");
        string path = AssetDatabase.GUIDToAssetPath(result[0]);
        var prefabRoot = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        return prefabRoot;
    }
}