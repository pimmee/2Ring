using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

    public GameObject PlayerPrefab;
	// Use this for initialization
	void Start () {

        if (isLocalPlayer == false) {
            return;
        }
        Instantiate(PlayerPrefab);
        Debug.Log("Instantiating my own Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
