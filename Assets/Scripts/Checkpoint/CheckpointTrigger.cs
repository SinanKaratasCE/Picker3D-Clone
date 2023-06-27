using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private Checkpoint _checkpoint;
    
    private void Awake()
    {
        _checkpoint = GetComponentInParent<Checkpoint>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Strings.PlayerTag)) return;
        if (!other.GetComponent<Picker>()) return;
        
        _checkpoint.CheckpointTimerStarter();
        other.GetComponent<Picker>().WhenPickerArrivesAtCheckpoint();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(Strings.PlayerTag)) return;
        if (!other.GetComponent<Picker>()) return;
    }
    
   

   
}
