using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void PushCollectable()
    {
        _rigidbody.DOMoveZ(transform.position.z + 12f, 0.5f);
    }
}
