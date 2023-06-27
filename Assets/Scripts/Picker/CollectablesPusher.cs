using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesPusher : MonoBehaviour
{
    private List<Collectable> _collectables = new List<Collectable>();
    private PickerMovement _pickerMovement;
    private Picker _picker;


    private void Awake()
    {
        GetClassReferences();
    }

    private void GetClassReferences()
    {
        _picker = GetComponent<Picker>();
        _pickerMovement = GetComponent<PickerMovement>();
    }

    private void OnEnable()
    {
        _picker.onCheckpointReached += PushCollectables;
    }

    private void OnDisable()
    {
        _picker.onCheckpointReached -= PushCollectables;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Strings.CollectableTag)) return;

        Collectable collectable = other.GetComponent<Collectable>();
        if (collectable != null)
        {
            _collectables.Add(collectable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(Strings.CollectableTag)) return;

        Collectable collectable = other.GetComponent<Collectable>();
        if (collectable != null)
        {
            _collectables.Remove(collectable);
        }
    }


    public void PushCollectables()
    {
        foreach (var collectable in _collectables)
        {
            collectable.PushCollectable();
        }
    }
}