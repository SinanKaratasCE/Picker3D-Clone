using System;
using UnityEngine;
using DG.Tweening;


public class Picker : MonoBehaviour
{
    private CollectablesPusher _collectablesPusher;
    private PickerMovement _pickerMovement;
    public event Action onCheckpointReached;
    public GameObject _pickerUpgrade;
    private Vector3 _pickerDefaultScale;


    private void Awake()
    {
        GetClassReferences();
    }
    
    private void OnEnable()
    {
        onCheckpointReached += PickerUpgradeCloser;
    }
    
    private void OnDisable()
    {
        onCheckpointReached -= PickerUpgradeCloser;
    }

    private void GetClassReferences()
    {
        _collectablesPusher = GetComponent<CollectablesPusher>();
        _pickerMovement = GetComponent<PickerMovement>();
        _pickerDefaultScale = transform.localScale;
    }

    public void WhenPickerArrivesAtCheckpoint()
    {
        onCheckpointReached?.Invoke();
    }

    public void ResetPickerPosition()
    {
        transform.position = new Vector3(0,0.5f,0);
    }

    private void PickerUpgradeOpener()
    {
        _pickerUpgrade.SetActive(true);
    }

    private void PickerUpgradeCloser()
    {
        _pickerUpgrade.SetActive(false);
    }
    
    public void IncreaseScale()
    {
        transform.DOScale(new Vector3(transform.localScale.x + 0.15f, transform.localScale.y, transform.localScale.z + 0.15f), 0.5f);
    }
    
    public void ResetPickerScale()
    {
        transform.DOScale(_pickerDefaultScale, 0.5f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Strings.PickerUpgrade))
        {
            PickerUpgradeOpener();
            other.gameObject.SetActive(false);
        }
    }
}