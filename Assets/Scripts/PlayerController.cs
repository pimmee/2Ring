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
    private bool moving = false;

    public GameObject bulletPrefab;
    public Transform firePoint;
    Plane groundPlane;
    private GameObject circle;
    public float speed = 10;
    public float bulletSpeed = 12;
    void Start()
    {
        circle = GameObject.Find("Circle");
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
        CheckOutside();
    }

       public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
        Camera.main.GetComponent<CameraFollow>().SetTarget(transform);
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
        firePoint.position = transform.position + (mousePosition - transform.position).normalized*1.5f;
        var bullet = (GameObject) Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation);

        bullet.GetComponent<Rigidbody>().velocity = mousePosition.normalized * bulletSpeed;

        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0f);
    }

    void Move() 
    {
        step = speed * Time.deltaTime;
        if (moving && transform.position == clickedPosition) {
            moving = false;
        }
        if (moving) {
            transform.position = Vector3.MoveTowards(transform.position, clickedPosition, step);
        }
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float rayLength;
        if (groundPlane.Raycast(ray, out rayLength))
        {
            mousePosition = ray.GetPoint(rayLength);
            mousePosition.y = transform.position.y;
            if (Input.GetMouseButtonDown(0))
            {
                moving = true;
                clickedPosition = ray.GetPoint(rayLength);
                transform.LookAt(new Vector3(clickedPosition.x, transform.position.y, clickedPosition.z));
            }
        }
    }

    void CheckOutside() {
        print(circle.transform.localScale);
        if (Vector3.Distance(Vector3.zero, transform.position) > Vector3.Distance(Vector3.zero, circle.transform.localScale) / 3) {
            Health health = GetComponent<Health>();
            health.TakeDamage(0.15f);
        }
    }
}
