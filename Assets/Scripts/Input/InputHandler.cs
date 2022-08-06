using UnityEngine;
using UnityEngine.InputSystem;
using Base;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : SingletonMono<InputHandler>
{
    private static PlayerInput _playerInput;

    private InputActionMap _playerMap;
    private InputActionMap _cameraMap;
    private InputActionMap _lockPickMap;
    
    #region Value Getter
    
    /// <summary>
    /// 二位输入值
    /// </summary>
    public static Vector2 RawMoveInput { get; private set; }
    
    /// <summary>
    /// x轴标准化的输入值
    /// </summary>
    public static int NormInputX { get; private set; }
    
    /// <summary>
    /// y轴标准化的输入值
    /// </summary>
    public static int NormInputY { get; private set; }
    
    /// <summary>
    /// 按下冲刺键
    /// </summary>
    public static bool SprintPressed { get; private set; }
    
    /// <summary>
    /// 按下交互键
    /// </summary>
    public static bool InteractPressed { get; private set; }
    
    /// <summary>
    /// 按下退出键
    /// </summary>
    public static bool ExitPressed { get; private set; }
    
    /// <summary>
    /// 按下顶锁键
    /// </summary>
    public static bool PryInput { get; private set; }
    
    /// <summary>
    /// 按下开锁键
    /// </summary>
    public static bool PickPressed { get; private set; }

    /// <summary>
    /// 按下任意键
    /// </summary>
    public static bool AnyKeyPressed => Keyboard.current.anyKey.wasPressedThisFrame;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        
        _playerInput = GetComponent<PlayerInput>();
        
        _playerMap = _playerInput.actions.FindActionMap("Player", true);
        _cameraMap = _playerInput.actions.FindActionMap("SurveillanceCam", true);
        _lockPickMap = _playerInput.actions.FindActionMap("Lock Pick", true);
    }

    protected override void Start()
    {
        base.Start();
        
        // Player
        _playerMap.actionTriggered += OnMoveInput;
        _playerMap.actionTriggered += OnInteractInput;
        _playerMap.actionTriggered += OnSprintInput;
        
        // Camera
        _cameraMap.actionTriggered += OnCamRotateInput;
        _cameraMap.actionTriggered += OnExitInput;

        // Lock Pick
        _lockPickMap.actionTriggered += OnPryInput;
        _lockPickMap.actionTriggered += OnPickInput;
    }

    #region Action Trigger Functions
    
    // Player
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Move") return;
        RawMoveInput = context.ReadValue<Vector2>();
        NormInputX = Mathf.RoundToInt(RawMoveInput.x);
        NormInputY = Mathf.RoundToInt(RawMoveInput.y);
    }

    private void OnSprintInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Sprint") return;

        SprintPressed = context.performed;
    }
    
    private void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Interact") return;
        
        InteractPressed = context.performed;
    }
    
    // Surveillance Camera
    private void OnCamRotateInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Rotate") return;
        NormInputX = Mathf.RoundToInt(context.ReadValue<float>());
        Debug.Log(NormInputX);
    }

    private void OnExitInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Exit") return;

        ExitPressed = context.performed;
    }

    // Lock Pick
    private void OnPryInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Pry") return;

        PryInput = context.performed;
    }

    private void OnPickInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Pick") return;

        PickPressed = context.performed;
    }
    
    #endregion
    
    # region Map Switcher
    
    /// <summary>
    /// 切换到玩家控制状态的输入
    /// </summary>
    public static void SwitchToPlayer() => _playerInput.SwitchCurrentActionMap("Player");
    
    /// <summary>
    /// 切换到摄像头控制状态的输入
    /// </summary>
    public static void SwitchToCamera() => _playerInput.SwitchCurrentActionMap("SurveillanceCam");
    
    /// <summary>
    /// 切换到开锁状态的输入
    /// </summary>
    public static void SwitchToLockPick() => _playerInput.SwitchCurrentActionMap("Lock Pick");
    
    #endregion
}
