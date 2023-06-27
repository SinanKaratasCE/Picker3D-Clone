using System;
using TMPro;
using UnityEngine;

public class CollectableCounter : MonoBehaviour
{
    public int _collectablesCount;
    [SerializeField] private TextMeshPro _collectablesCountText;
    private Checkpoint _checkpoint;
    private GameManager _gameManager;

    private void Awake()
    {
        _checkpoint = GetComponentInParent<Checkpoint>();
    }

    private void Start()
    {
        _gameManager = Singleton.Main.gameManager;
        SetLevelDifficulty();
    }

    private void SetLevelDifficulty()
    {
        switch (_gameManager.currentLevel.levelDifficulty)
        {
            case LevelDifficulty.Easy:
                _checkpoint.targetCollectableCount = _checkpoint.maxCollectableCount - _checkpoint.maxCollectableCount/2;
                break;
            case LevelDifficulty.Medium:
                _checkpoint.targetCollectableCount = _checkpoint.maxCollectableCount - _checkpoint.maxCollectableCount/3;
                break;
            case LevelDifficulty.Hard:
                _checkpoint.targetCollectableCount = _checkpoint.maxCollectableCount - _checkpoint.maxCollectableCount/5;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        

        _collectablesCountText.text = $"{_collectablesCount.ToString()} / {_checkpoint.targetCollectableCount}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Strings.CollectableTag))
            return;

        CollectableCollected();
    }

    private void CollectableCollected()
    {
        _collectablesCount++;
        _gameManager.IncreasePlayerGold(1);
        _collectablesCountText.text = $"{_collectablesCount.ToString()} / {_checkpoint.targetCollectableCount}";
    }
}