using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform camera;
    public Transform model;
    public Transform closedReference;
    public Transform openReference;
    // Start is called before the first frame update
    void Start()
    {
        model.position = closedReference.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = camera.forward;
        Vector3 playerToDoorDirection = camera.forward - transform.position;
        playerToDoorDirection.y = 0;
        lookDirection.y = 0;

        lookDirection.Normalize();
        playerToDoorDirection.Normalize();

        model.position = Vector3.Dot(lookDirection, playerToDoorDirection) < -0.8 ? openReference.position : closedReference.position;
    }


    private void OnDrawGizmos() {
        Gizmos.DrawRay(camera.position, camera.forward);
        Gizmos.DrawRay(camera.position, transform.position);
    }
}


