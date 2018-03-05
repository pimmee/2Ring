using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 15f;
    public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Vector3 dir = target.position - this.transform.localPosition;
        /*
                float distThisFrame = speed * Time.deltaTime;

                if (dir.magnitude <= distThisFrame) {
                    // Reached target
                    DoBulletHit();
                }
                else {
                    //Move towards target

                    transform.Translate(dir.normalized * distThisFrame, Space.World);
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime);
                }


                */
    }
    void DoBulletHit()
    {
        // InjureEnemy
    }
}
