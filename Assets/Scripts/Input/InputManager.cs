using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class InputManager : MonoBehaviour
{
    [Header("Inputs")] private PlayerInput _playerInput;
    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;

    [Header("Input Settings")] [SerializeField]
    float m_InputSensitivity = 1.5f;

    public bool hasInput;
    Vector3 m_InputPosition;
    Vector3 m_PreviousInputPosition;
    private float normalizedDeltaPosition;

    [Header("Other Components")] private GameManager _gameManager;
    private PickerMovement _pickerMover;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _pickerMover = GameObject.FindGameObjectWithTag(Strings.PlayerTag).GetComponent<PickerMovement>();
        _touchPositionAction = _playerInput.actions[Strings.PlayerInputsTouchPosition];
        _touchPressAction = _playerInput.actions[Strings.PlayerInputsTouchPress];
    }

    private void Start()
    {
        _gameManager = Singleton.Main.gameManager;
    }


    void Update()
    {
        if (_pickerMover == null)
        {
            return;
        }


#if UNITY_EDITOR
        m_InputPosition = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.isPressed)
        {
            if (_gameManager.isGameStarted == false)
                _gameManager.IsGameStartedListener();

            if (!hasInput)
            {
                m_PreviousInputPosition = m_InputPosition;
            }

            hasInput = true;
        }
        else
        {
            hasInput = false;
        }
#else
            if(_touchPressAction.triggered)
            {
                if (_gameManager.isGameStarted == false)
                    _gameManager.GameStarterListener();
                m_InputPosition = _touchPositionAction.ReadValue<Vector2>();
                if (!m_HasInput)
                {
                    m_PreviousInputPosition = m_InputPosition;
                }

                m_HasInput = true;
            }
            else
            {
                m_HasInput = false;
            }
#endif

        if (hasInput)
        {
            normalizedDeltaPosition =
                (m_InputPosition.x - m_PreviousInputPosition.x) / Screen.width * m_InputSensitivity;
            _pickerMover.SetDirection(normalizedDeltaPosition);
        }

        m_PreviousInputPosition = m_InputPosition;
    }
}