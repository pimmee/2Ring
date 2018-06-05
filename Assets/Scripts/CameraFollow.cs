using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : NetworkBehaviour {

    public Transform playerTransform;

    public float smoothSpeed = 0.3f;

    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate() {
        if(playerTransform != null) {
            Vector3 desiredPosition = playerTransform.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;
        }

    }

    public void SetTarget(Transform target) {
        playerTransform = target;
        //transform.LookAt(target);
    }
}
