using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Checkpoint : MonoBehaviour
{
    private CollectableCounter _collectableCounter;
    [SerializeField] private GameObject _leftBarrierRoot, _rightBarrierRoot, _bottomPlatform;
    public event Action onSuccess;
    public event Action onFail;
    private PickerMovement _pickerMovement;

    [Header("Checkpoint Settings")] public int maxCollectableCount;
    public int targetCollectableCount;
    private IEnumerator _checkpointTimer;

    private void Awake()
    {
        GetClassReferences();
    }

    private void GetClassReferences()
    {
        _collectableCounter = GetComponentInChildren<CollectableCounter>();
        _pickerMovement = GameObject.FindGameObjectWithTag(Strings.PlayerTag).GetComponent<PickerMovement>();
        _checkpointTimer = CheckpointTimerCO();
    }

    #region Checkpoint Subscribers

    private void Start()
    {
        OnSuccessEventSubscribe();
        OnFailSubscribe();
    }

    private void OnDisable()
    {
        OnSuccessUnsubscribe();
        OnFailUnsubscribe();
    }

    private void OnSuccessEventSubscribe()
    {
        //Add Level Progress 
        onSuccess += BarrierOpener;
        onSuccess += _pickerMovement.ChangePickerMoveCondition;
        onSuccess += BottomPlatformBoxColliderEnabler;
        onSuccess += Singleton.Main.gameManager.IncreaseCheckpointCount;
        onSuccess += Singleton.Main.gameManager.IncreasePickerScale;
    }

    private void OnSuccessUnsubscribe()
    {
        //Add Level Progress 
        onSuccess -= BarrierOpener;
        onSuccess -= _pickerMovement.ChangePickerMoveCondition;
        onSuccess -= BottomPlatformBoxColliderEnabler;
        onSuccess -= Singleton.Main.gameManager.IncreaseCheckpointCount;
        onSuccess += Singleton.Main.gameManager.IncreasePickerScale;
    }

    private void OnFailSubscribe()
    {
        onFail += CheckpointFail;
    }

    private void OnFailUnsubscribe()
    {
        onFail -= CheckpointFail;
    }

    #endregion


    public void CheckpointTimerStarter()
    {
        if (_checkpointTimer != null)
            StartCoroutine(_checkpointTimer);
    }

    private IEnumerator CheckpointTimerCO()
    {
        yield return new WaitForSeconds(2f);
        CheckCollectablesCount();
    }


    private void CheckCollectablesCount()
    {
        if (_collectableCounter._collectablesCount >= targetCollectableCount)
        {
            OnSuccess();
        }
        else
        {
            onFail?.Invoke();
        }
    }

    private void OnSuccess()
    {
        _bottomPlatform.transform.DOMoveY(-0.218f, 1f).OnComplete(RunSuccessEvent);
    }

    private void RunSuccessEvent()
    {
        onSuccess?.Invoke();
    }


    private void BarrierOpener()
    {
        _leftBarrierRoot.transform.DORotate(new Vector3(-70f, 90, 90), 1f);
        _rightBarrierRoot.transform.DORotate(new Vector3(70f, 90, 90), 1f);
    }

    private void BottomPlatformBoxColliderEnabler()
    {
        _bottomPlatform.GetComponent<BoxCollider>().enabled = true;
    }


    private void CheckpointFail()
    {
        Singleton.Main.uiManager.ShowLevelFailPanel();
    }
}