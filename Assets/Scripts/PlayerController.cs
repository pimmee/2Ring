using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public float inputDelay = 0.1f;
    public float forwardVel = 12;
    public float rotateVel = 100;
    public float speed = 10;

    Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput, turnInput;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera camera;
    public AttackController attackController;
    public Vector3 mousePosition;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    void Start()
    {
        targetRotation = transform.rotation;
        rBody = GetComponent<Rigidbody>();
        camera = FindObjectOfType<Camera>();
        forwardInput = turnInput = 0;
    }

    void Update()
    {
        //Player movement
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            mousePosition = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(mousePosition.x, transform.position.y, mousePosition.z));
            if (Input.GetMouseButtonDown(0))
            { // Lägg till if mouseposition < position
                moveVelocity = new Vector3(mousePosition.x - transform.position.x, 0, mousePosition.z - transform.position.z);
            }
        }
        //    moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //    moveVelocity = moveInput * moveSpeed;
        //    Turn();
        //    rBody.velocity = moveVelocity;
        //    Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        //    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);// up to "face up"
        //    float rayLength;

        //    if (groundPlane.Raycast(cameraRay, out rayLength))
        //    {
        //        Vector3 pointToLook = cameraRay.GetPoint(rayLength);

        //        transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        //    }

        //    if (Input.GetMouseButtonDown(0))
        //        attackController.isAttacking = true;
        //    if (Input.GetMouseButtonUp(0))
        //        attackController.isAttacking = false;
        //}
    }
    void FixedUpdate()
    {
        rBody.velocity = moveVelocity;
    }

}
