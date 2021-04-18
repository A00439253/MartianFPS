using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float verticleTopClamp = -70f;
    [SerializeField] float verticleBottomClamp = 80f;
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float mouseSensitivity = 45f;
    [SerializeField] Camera playerCamera;
    [SerializeField] CharacterController mCharacterController;


    private float camX_rot = 0f;
    private Vector3 playerGravityVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(playerCamera == null)    playerCamera = Camera.main;
        mCharacterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        PlayerMovement();

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        //Horizontal Cam Calculations
        camX_rot -= mouseY;
        //Vertical Cam calculations with clamping
        camX_rot = Mathf.Clamp(camX_rot, verticleTopClamp, verticleBottomClamp);

        playerCamera.transform.localRotation = Quaternion.Euler(camX_rot, 0, 0);
        //Player rotation along with Cam.
        transform.Rotate(Vector3.up * mouseX * 3);
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

    private void PlayerMovement()
    {
        //WASD input handling
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;
        mCharacterController.Move(movement * Time.deltaTime * playerSpeed);
    }


}
