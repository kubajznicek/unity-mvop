using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Reference")]
    public Transform playerObject;
    public Transform camera;
    public CharacterController CharacterController;

    [Header("Rychlosti")]
    public float lookSpeed = 5f;
    public float moveSpeed = 10f;

    [Header("Gravitace")]
    public float Gravity;
    public float JumpIntensity = 5;


    float yVelocity = 0f;



    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        // mouse move
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        camera.eulerAngles += new Vector3(-mouseY, 0, 0) * lookSpeed;
        playerObject.eulerAngles += new Vector3(0, mouseX, 0) * lookSpeed;
        
        // keyboard triggers

        Vector3 movementDirection = Vector3.zero;


        #region wsad
            if (Input.GetKey(KeyCode.W))
            {
                movementDirection += playerObject.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementDirection -= playerObject.right;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementDirection -= playerObject.forward;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movementDirection += playerObject.right;
            }
        #endregion

        if (CharacterController.isGrounded)
        {
            yVelocity = 0;
        }

        if (Input.GetKey(KeyCode.Space) && CharacterController.isGrounded)
        {
            yVelocity = JumpIntensity;
        }

        yVelocity -= Gravity * Time.deltaTime;


        movementDirection.y = yVelocity;

        movementDirection.Normalize();

        CharacterController.Move(movementDirection* Time.deltaTime * moveSpeed);
    }
}
