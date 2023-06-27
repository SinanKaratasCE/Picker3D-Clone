using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PickerCamera : MonoBehaviour
    {
       private Picker _picker;
       private Vector3 _offset;      

       private void Start()
       {
           Initialize();
       }

       private void Initialize()
       {
           _picker = GameObject.FindGameObjectWithTag(Strings.PlayerTag).GetComponent<Picker>();

           if (_picker != null)
           {
               _offset = transform.position - _picker.transform.position;

           }
       }

       private void LateUpdate()
       {
           if(_picker == null)
               return;
            
           transform.position = new Vector3(transform.position.x,transform.position.y,
               _picker.transform.position.z + _offset.z);
       }
    }

