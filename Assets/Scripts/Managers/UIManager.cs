using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject gameStartPanel;
    [SerializeField] private GameObject _levelFailPanel;
    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private TMP_Text _currentLevelText, _nextLevelText;
    [SerializeField] private List<Image>_checkpointIndicators = new List<Image>();
    
    
    public void CheckpointUIInitializer(int targetCheckpointCount)
    {
        for(int i=0; i<targetCheckpointCount; i++)
        {
            _checkpointIndicators[i].gameObject.SetActive(true);
        }
    }
    
    public void ShowLevelFailPanel()
    {
        _levelFailPanel.SetActive(true);
    }
    public void ShowLevelCompletePanel()
    {
        _levelCompletePanel.SetActive(true);
    }
    
    public void UpdateLevelText(int currentLevel)
    {
        _currentLevelText.text = currentLevel.ToString();
        _nextLevelText.text = (currentLevel+1).ToString();
    }
    
    public void UpdateCheckpointIndicator(int checkpointCount)
    { 
        _checkpointIndicators[checkpointCount].color = Color.yellow;
    }
    
    public void ResetCheckpointIndicators()
    {
        foreach (var indicator in _checkpointIndicators)
        {
            indicator.color = Color.white;
        }
    }

    public void UpdateGoldText(float amount)
    {
        _goldText.text = amount.ToString();
    }
}
