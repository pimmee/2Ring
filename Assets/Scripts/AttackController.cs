using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AttackController : NetworkBehaviour {

    public GameObject PlayerUnitPrefab;
    public GameObject FireBoltPrefab;
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
            CmdFire();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Teleport();
        }
    }

    [Command]
    void CmdFire()
    {   
        if (cooldown > 0)
            return;

        var firebolt = Instantiate(FireBoltPrefab, firePoint.transform.position, firePoint.transform.rotation);
        firebolt.GetComponent<Rigidbody>().velocity = firebolt.transform.forward * speed;

        NetworkServer.Spawn(firebolt);
        Destroy(firebolt, 2.0f);
        cooldown = 2;
        //CmdShootFirebolt();
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
        Vector3 teleportTo = PlayerUnitPrefab.transform.position + PlayerUnitPrefab.transform.forward * 5;
        PlayerUnitPrefab.transform.position = teleportTo;
    }

    [Command]
    void CmdShootFirebolt() {
        //GameObject newFirebolt = Instantiate(firebolt, firePoint.transform.position, firePoint.transform.rotation);
        //newFirebolt = firePoint.transform.forward * speed;
        Debug.Log("Fire!");
        //NetworkServer.SpawnWithClientAuthority(newFirebolt, connectionToClient);
    }
}
