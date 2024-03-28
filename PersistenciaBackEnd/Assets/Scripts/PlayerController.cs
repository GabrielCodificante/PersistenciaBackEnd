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
    private InputAction move, jump, look, load, save, level1, level2; 

    public static PlayerController instance;

     #endregion

     void Save(InputAction.CallbackContext call){
        SaveLoadController.instance.Save();
        PlayerManager.instance.SavePrefs();
        
     }

     void Load(InputAction.CallbackContext call){
        SaveLoadController.instance.Load();
        PlayerManager.instance.LoadPrefs();
     }

     void OnlineLoad(InputAction.CallbackContext call)
     {
        if(call.action == level1){
            StartCoroutine(SaveLoadController.instance.OnlineLoad(1));
            Debug.Log(call);
        }
        else if(call.action == level2){
            StartCoroutine(SaveLoadController.instance.OnlineLoad(2));
            Debug.Log(call);
        }
     }

    void OnEnable()
    {
        inputs.Enable();

        jump.performed += characterMovement.Jump;
        load.performed += Load;
        save.performed += Save;
        level1.performed += OnlineLoad;
        level2.performed += OnlineLoad;
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
        level1 = inputs.Player.LoadLevel1;
        level2 = inputs.Player.LoadLevel2;
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
