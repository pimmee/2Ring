using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CircleScript : NetworkBehaviour {
    [SyncVar]
    Vector3 currentScale;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (FindObjectOfType<PlayerController>() != null) {
            currentScale = transform.localScale;
            currentScale.x -= 0.01f;
            currentScale.z -= 0.01f;
            transform.localScale = currentScale;
        }
    }
}