using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Reference")]
    public Transform playerObject;
    public Transform Camera;
    public CharacterController CharacterController;
    public GameObject Turret;

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

    void PlayerMove(){
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


    void PlaceTurret(){
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.forward, out hit, Mathf.Infinity) && hit.collider.tag == "floor"){
            Vector3 up = hit.normal.normalized;
            Vector3 forward = Vector3.Cross(Camera.right, up).normalized;           

            Instantiate(Turret, hit.point, Quaternion.LookRotation(forward, up));
        } 

    }

    void turretPreview(){
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.forward, out hit, Mathf.Infinity) && hit.collider.tag == "floor"){
            Vector3 up = hit.normal.normalized;
            Vector3 forward = Vector3.Cross(Camera.right, up).normalized;

            GameObject turretPreview = Instantiate(Turret, hit.point, Quaternion.LookRotation(forward, up));
            Color tempcolor = turretPreview.GetComponent<Renderer>().sharedMaterial.color;
            tempcolor.a = 0.1f;
        } 
    }


    // Update is called once per frame
    void Update()
    {
        // keyboard triggers
        PlayerMove();

        if (Input.GetMouseButtonDown(0))
        {
            PlaceTurret();
        }
        if (Input.GetKey(KeyCode.E))
        {
            turretPreview();
        }

        // mouse move
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        Camera.eulerAngles += new Vector3(-mouseY, 0, 0) * lookSpeed;
        playerObject.eulerAngles += new Vector3(0, mouseX, 0) * lookSpeed;
        
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Ray(Camera.transform.position, Camera.forward));

        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.forward, out hit, Mathf.Infinity))
        {
            // draw hit target
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(hit.point, 0.1f);

            // draw bullet landing normal
            Gizmos.color = Color.green;
            Vector3 up = hit.normal.normalized;
            Gizmos.DrawRay(hit.point, up);

            Gizmos.color = Color.cyan;
            Vector3 forward = Vector3.Cross(Camera.right, up).normalized;
            Gizmos.DrawRay(hit.point, forward);

            Gizmos.color = Color.red;
            Vector3 right = Vector3.Cross(up, forward).normalized;
            Gizmos.DrawRay(hit.point, right);


            // draw cube
            Gizmos.color = Color.white;
            Gizmos.matrix = Matrix4x4.TRS(hit.point, Quaternion.LookRotation(forward, up), Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}
