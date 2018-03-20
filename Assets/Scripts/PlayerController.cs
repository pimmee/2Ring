using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    Vector3 velocity;
    float step;
    private Camera cam;
    public Vector3 clickedPosition;
    public Vector3 mousePosition;

    public GameObject bulletPrefab;
    public Transform firePoint;
    Plane groundPlane;
    public float speed = 10;
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        groundPlane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        if (!isLocalPlayer){
            return;
        }
        Move();
        Attack();
    }

       public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            CmdShoot();
        }
    }
    //All Commands will be called by the client, but run on the server.
    [Command]
    void CmdShoot() {
        var bullet = (GameObject) Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0f);
    }

    void Move() 
    {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, clickedPosition, step);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float rayLength;
        if (groundPlane.Raycast(ray, out rayLength))
        {
            mousePosition = ray.GetPoint(rayLength);
            mousePosition.y = transform.position.y;
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(transform.position);
                clickedPosition = ray.GetPoint(rayLength);
                transform.LookAt(new Vector3(clickedPosition.x, transform.position.y, clickedPosition.z));
            }
        }
    }

    void FixedUpdate()
    {
        //rBody.velocity = moveVelocity;
    }

}
