﻿using System.Collections;
using UnityEngine;
 
public class Slingshot : MonoBehaviour {
static private Slingshot S;
// fields set in the Unity Inspector pane
[Header("Set in Inspector")]                                //a
public GameObject prefabProjectile;
public float velocityMult = 8f;
 
// fields set dynamically
[Header("Set Dynamically")]                                 //a
public GameObject launchPoint;                             
public Vector3 launchPos;                                   //b
public GameObject projectile;                               //b
public bool aimingMode;                                     //b
private Rigidbody projectileRigidbody;

static public Vector3 LAUNCH_POS{
   get {
      if(S == null) return Vector3.zero;
      return S.launchPos;
   }
}
 
void Awake() {
   S = this;
Transform launchPointTrans = transform.Find("LaunchPoint"); 
launchPoint = launchPointTrans.gameObject;
launchPoint.SetActive( false );                             
launchPos = launchPointTrans.position;                       //c
}
 
void OnMouseEnter() {
//print("Slingshot:OnMouseEnter()");
launchPoint.SetActive( true );                               //b
}
 
void OnMouseExit() {
//print("Slingshot:OnMouseExit()");
launchPoint.SetActive (false);
}

void OnMouseDown(){                                          //d
   //the player has pressed the mouse button while over slingshot
   aimingMode = true;
   //Instantiate a Projectile
   projectile = Instantiate(prefabProjectile) as GameObject;
   // Start it at the launchPoint
   projectile.transform.position = launchPos;
   projectile.GetComponent<Rigidbody>().isKinematic = true;
   // Set it to isKinematic for now
   projectileRigidbody = projectile.GetComponent<Rigidbody>();
   projectileRigidbody.isKinematic = true;
  
   }
void Update() {
// If Slingshot is not in aimingMode, don't run this code
if (!aimingMode) return;
// Get the current mouse position in 2D screen coordinates
Vector3 mousePos2D = Input.mousePosition;
// Convert the mouse position to 3D world coordinates
mousePos2D.z = -Camera.main.transform.position.z;
Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );
// Find the delta from the launchPos to the mousePos3D
Vector3 mouseDelta = mousePos3D-launchPos;
// Limit mouseDelta to the radius of the Slingshot SphereCollider
float maxMagnitude = this.GetComponent<SphereCollider>().radius;
if (mouseDelta.magnitude > maxMagnitude) {
mouseDelta.Normalize();
mouseDelta *= maxMagnitude;
}
// Move the projectile to this new position
Vector3 projPos = launchPos + mouseDelta;
projectile.transform.position = projPos;
 
if ( Input.GetMouseButtonUp(0) ) {
// The mouse has been released
aimingMode = false;
projectile.GetComponent<Rigidbody>().isKinematic = false;
projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
FollowCam.POI = projectile;
projectile = null;
}
}
}