using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

    public PlayerController player;
    public Rigidbody firebolt;
    public Transform firePoint;

    public float fireRate = 1f;
    float cooldown = 0;
    public float damage = 1;
    
    public float speed = 10f;
   

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        firePoint.transform.rotation = this.transform.rotation;

        cooldown -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Teleport();
        }
    }

    void Fire()
    {
       Rigidbody newFirebolt = (Rigidbody)Instantiate(firebolt, firePoint.transform.position, firePoint.transform.rotation);
        newFirebolt.velocity = firePoint.transform.forward * speed;
        if (cooldown > 0)
            return;

        //Ray ray = new Ray(firePoint.transform.position, firePoint.transform.forward);
        //RaycastHit hitInfo;
        //if(Physics.Raycast(ray, out hitInfo))
        //{
        //    // Do effect on hitInfo.point
        //    Debug.Log("We hit: " + hitInfo.collider.name);
        //    Transform hitTransform = hitInfo.transform;
        //    Health h = hitInfo.transform.GetComponent<Health>();

        //    while(h == null && hitTransform.parent) { // Check to see if hit object has health, if not, check if parent has health(Crate/graphics)
        //        hitTransform = hitTransform.parent;
        //        h = hitTransform.GetComponent<Health>();
        //    }

        //    if(h != null) {
        //        h.TakeDamage(damage);
        //    }
        //}
        


        cooldown = fireRate;
    }


    void Teleport()
    {
        Debug.Log(player.transform.position);
        Vector3 teleportTo = player.transform.position + player.transform.forward * 3;
        player.transform.position = teleportTo;
        Debug.Log(player.transform.position);
    }
}
