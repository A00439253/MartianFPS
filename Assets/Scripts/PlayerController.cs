using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float verticalTopClamp = -70f;
    [SerializeField] float verticalBottomClamp = 80f;
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float mouseSensitivity = 45f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] Camera playerCamera;
    [SerializeField] CharacterController mCharacterController;


    private float camX_rot = 0f;
    private Vector3 playerGravityVelocity;

    bool bIsGrounded = false;
    public GameObject gunObject;
    public Transform groundCheck;
    public LayerMask groundMask;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(playerCamera == null)    playerCamera = Camera.main;
        mCharacterController = GetComponent<CharacterController>();
        if(LevelManager.Instance)
        LevelManager.Instance.DisableExtraGameObjects();
        if(UI_Manager.Instance)
        UI_Manager.Instance.UpdateUI_State(UI_Manager.UI_States.Gameplay);

        gunObject.SetActive(false);
        PlayerProperties.Instance.PlayerControllerInstance = this;


        PlayerProperties.Instance.bHasGameWon = false;

        ScoreManager.Instance.GameHasStarted();

   //     PlayerProperties.Instance.health = 100;
   //     PlayerProperties.Instance.bullets = 99;
   //     PlayerProperties.Instance.goodies = 0;
   //     ScoreManager.Instance.UpdateBullets(PlayerProperties.Instance.bullets);
   //     ScoreManager.Instance.UpdateHealth(PlayerProperties.Instance.health);
   //     ScoreManager.Instance.UpdateGoodies(PlayerProperties.Instance.goodies);
    }

    void Update()
    {
        PlayerMovement();

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        //Horizontal Cam Calculations
        camX_rot -= mouseY;
        //Vertical Cam calculations with clamping
        camX_rot = Mathf.Clamp(camX_rot, verticalTopClamp, verticalBottomClamp);

        playerCamera.transform.localRotation = Quaternion.Euler(camX_rot, 0, 0);
        //Player rotation along with Cam.
        transform.Rotate(Vector3.up * mouseX * 3);

        bIsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Input.GetButtonDown("Jump") && bIsGrounded)
            playerGravityVelocity.y = Mathf.Sqrt(jumpHeight * -2f * -9.18f);

    }

    private void FixedUpdate()
    {
        if (mCharacterController.isGrounded)
        {
            playerGravityVelocity.y = 0f;
        }
        else
        {
            playerGravityVelocity.y += -9.18f * Time.deltaTime;
            mCharacterController.Move(playerGravityVelocity * Time.deltaTime);
        }
    }

    public void ActivateGun()
    {
        gunObject.SetActive(true);
    }

    private void PlayerMovement()
    {
        //WASD input handling
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;
        mCharacterController.Move(movement * Time.deltaTime * playerSpeed);
    }


}
