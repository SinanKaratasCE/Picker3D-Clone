using System;
using UnityEngine;

public class PickerMovement : MonoBehaviour
{
    [Header("Movement Settings")] [SerializeField]
    private float speed;

    [SerializeField] private float horizontalMovementSpeed;
    private float _direction;
    public bool isCheckpointReached;


    [Header("Other Components")] private GameManager _gameManager;
    private Picker _picker;

    private void Awake()
    {
        _picker = GetComponent<Picker>();
    }
    
    
    private void Start()
    {
        _gameManager = Singleton.Main.gameManager;
    }
    
    private void OnEnable()
    {
        _picker.onCheckpointReached += ChangePickerMoveCondition;
    }
    
    private void OnDisable()
    {
        _picker.onCheckpointReached -= ChangePickerMoveCondition;
    }

    private void FixedUpdate()
    {
        if (_gameManager.isGameStarted == false)
            return;

        transform.Translate(Time.deltaTime * (_direction * horizontalMovementSpeed), 0, 0);
        
        if (!isCheckpointReached)
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        
        _direction = 0;
    }

    public void SetDirection(float normalizedDeltaPosition)
    {
        _direction = normalizedDeltaPosition;
    }
    
    public void ChangePickerMoveCondition()
    {
        isCheckpointReached = !isCheckpointReached;
    }
}