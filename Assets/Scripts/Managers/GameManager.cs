using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum LevelDifficulty
{
    Easy ,
    Medium ,
    Hard 
}

public class GameManager : MonoBehaviour
{
    [Header("Game State Variables")] public bool isGameStarted = false;

    [Header("Save Variables")] public int levelIndex;
    public float gold;

    [Header("Other Components")] private UIManager _uiManager;
    private Picker _picker;
    public LevelContainer levelContainer;
    private SaveManager _saveManager;

    [Header("Level Variables")] public LevelData currentLevel;
    private int _checkpointCount;
    private int _randomLevelIndex;

    private void Awake()
    {
        _picker = GameObject.FindGameObjectWithTag(Strings.PlayerTag).GetComponent<Picker>();
    }

    void Start()
    {
        _uiManager = Singleton.Main.uiManager;
        _saveManager = Singleton.Main.saveManager;
        _saveManager.LoadPlayerData();
        LoadLevel();
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        _uiManager.UpdateLevelText(levelIndex);
        _uiManager.CheckpointUIInitializer(currentLevel.checkPointCount);
        _uiManager.UpdateGoldText(gold);
    }

    public void LoadPlayerData(PlayerData _playerData)
    {
        levelIndex = _playerData.levelIndex;
        gold = _playerData.gold;
        
        CheckLevelIndex();
    }
    
    private void CheckLevelIndex()
    {
        _randomLevelIndex = levelIndex > 10 ? Random.Range(0, levelContainer.levels.Count) : levelIndex;
    }

    public void IsGameStartedListener()
    {
        isGameStarted = true;
        _uiManager.gameStartPanel.SetActive(false);
    }

    public void ResetLevel()
    {
        isGameStarted = false;
        _uiManager.UpdateLevelText(levelIndex);
        _uiManager.CheckpointUIInitializer(currentLevel.checkPointCount);
        _uiManager.ResetCheckpointIndicators();
        _picker.GetComponent<PickerMovement>().isCheckpointReached = false;
        _uiManager.gameStartPanel.SetActive(true);
        _checkpointCount = 0;
        _picker.ResetPickerPosition();
        _picker.ResetPickerScale();
        _saveManager.SavePlayerData();
        LoadLevel();
    }

    public void IncreaseCheckpointCount()
    {
        CheckpointUIColorChanger(_checkpointCount);
        _checkpointCount++;

        if (_checkpointCount == currentLevel.checkPointCount)
        {
            _uiManager.ShowLevelCompletePanel();
        }
    }

    public void IncreasePickerScale()
    {
        _picker.IncreaseScale();
    }

    public void IncreasePlayerGold(float amount)
    {
        gold += amount;
        _uiManager.UpdateGoldText(gold);
    }

    //Add Level Pool
    private void LoadLevel()
    {
        if (currentLevel != null)
            Destroy(currentLevel.gameObject);
        currentLevel = Instantiate(levelContainer.levels[_randomLevelIndex], Vector3.zero, Quaternion.identity);
    }

    public void NextLevel()
    {
        levelIndex++;
        CheckLevelIndex();
        ResetLevel();
    }

    private void CheckpointUIColorChanger(int checkpointCount)
    {
        _uiManager.UpdateCheckpointIndicator(checkpointCount);
    }
}