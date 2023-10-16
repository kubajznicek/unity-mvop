using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform barel;
    private Transform lookAtPosition;

    // Start is called before the first frame update
    void Start()
    {
        lookAtPosition = GameObject.FindGameObjectWithTag("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        barel.transform.LookAt(lookAtPosition);
    }
}
