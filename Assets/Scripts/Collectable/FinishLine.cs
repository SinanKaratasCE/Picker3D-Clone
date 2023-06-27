using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Strings.PlayerTag)) return;
        if (!other.GetComponent<PickerMovement>()) return;
        
       other.GetComponent<PickerMovement>().isCheckpointReached= true;
    }
}
