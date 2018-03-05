using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float inputDelay = 0.1f;
    public float forwardVel = 12;
    public float rotateVel = 100;
    public float moveSpeed;

    Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput, turnInput;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    void Start()
    {
        targetRotation = transform.rotation;
        rBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        forwardInput = turnInput = 0;
    }

    void Update()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveVelocity = moveInput * moveSpeed;
        //Turn();

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);// up to "face up"
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            transform.LookAt(pointToLook);
        }


    }

    void FixedUpdate()
    {
        Debug.Log(moveVelocity);
        rBody.velocity = moveVelocity;
    }
    /*
    void Run()
    {
        if (Mathf.Abs(forwardInput) > inputDelay)
        {
            // Move!
            rBody.velocity = transform.forward * forwardInput * forwardVel; // Forwardinput can be (-1,1) and determines if we go back or forward
        }
        else
        {
            rBody.velocity = Vector3.zero;
        }
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputDelay) {
            targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }
    */
}
