using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
static public GameObject POI; //The static point of inerest

[Header("Set in Inspector")]
public float easing = 0.05f;
public Vector2 minXY = Vector2.zero;
[Header("Set Dynamically")]
public float camZ; //The desired z pos of the camera

void Awake() {
camZ = this.transform.position.z;
}

void FixedUpdate () {
// if there's only one line following an if, it doesn't need braces
if (POI == null) return; // return if there is no poi

// Get the position of the poi
//Vector3 destination = POI.transform.position;

Vector3 destination;
if (POI == null) {
    destination = Vector3.zero;
 }else {
    destination = POI.transform.position;
    if (POI.tag == "Projectile") {
        if (POI.GetComponent<Rigidbody>().IsSleeping() ){
            POI = null;
            return;
        }
    }
}

// Limit the x & y to minimum values
destination.x = Mathf.Max( minXY.x, destination.x );
destination.y = Mathf.Max( minXY.y, destination.y );
// Interpolate from the current Camera position towards destination
destination = Vector3.Lerp(transform.position,destination, easing);
// Force destination.z to be camZ to keep the camera far enough away
destination.z = camZ;
// Set the camera to the destination
transform.position = destination;
// Set the orthographicSize of the Camera to keep Ground in view
Camera.main.orthographicSize = destination.y + 10;
}
}
