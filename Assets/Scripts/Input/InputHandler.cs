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
    /// 按下手电按钮
    /// </summary>
    public static bool LightPressed { get; private set; }
    
    /// <summary>
    /// 按下换电池按钮
    /// </summary>
    public static bool ReloadPressed { get; private set; }

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
    }

    protected override void Start()
    {
        base.Start();
        
        // Player
        _playerMap.actionTriggered += OnMoveInput;
        _playerMap.actionTriggered += OnInteractInput;
        _playerMap.actionTriggered += OnSprintInput;
        _playerMap.actionTriggered += OnLightInput;
        _playerMap.actionTriggered += OnReloadInput;
    }

    public static void UseLightInput() => LightPressed = false;

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

    private void OnLightInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Light") return;
        LightPressed = context.performed;
    }
    
    private void OnReloadInput(InputAction.CallbackContext context)
    {
        if (context.action.name != "Reload") return;
        
        ReloadPressed = context.performed;
    }
}
