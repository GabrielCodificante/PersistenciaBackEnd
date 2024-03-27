using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Atributes
    [Header("References")]
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CameraController cameraController;

    [Header("Inputs")]
    [SerializeField] private Vector2 currentMove;
    [SerializeField] private Vector2 cameraLook;
   
    private PlayerInputActions inputs;
    private InputAction move, jump, look, load, save; 

    public static PlayerController instance;

     #endregion

    void OnEnable()
    {
        inputs.Enable();

        jump.performed += characterMovement.Jump;
        load.performed += PlayerManager.instance.LoadPrefs;
        save.performed += PlayerManager.instance.SavePrefs;
    }

    void OnDisable()
    {
        inputs.Disable();
    }

    void Awake()
    {
        inputs = new PlayerInputActions();

        move = inputs.Player.Move;
        jump = inputs.Player.Jump;
        look = inputs.Player.Look;
        load = inputs.Player.Load;
        save = inputs.Player.Save;
    }

    void Start(){
        if(instance != null){
            Destroy(this.gameObject);
        }
        else instance = this;
    }

    void FixedUpdate()
    {
        currentMove = move.ReadValue<Vector2>();

        characterMovement.SetInput(new CharacterMovementInput()
        {
            MoveInput = currentMove
        });
    }

    void Update()
    {
        cameraLook = look.ReadValue<Vector2>();
        cameraController.CameraRotation(new Vector2(cameraLook.y, cameraLook.x));
    }

}
