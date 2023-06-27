using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Spin : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 200f;
    
    

    private void Start()
    {
        if(transform.position.x>0)
            rotateSpeed = -rotateSpeed;
    }
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }
}
