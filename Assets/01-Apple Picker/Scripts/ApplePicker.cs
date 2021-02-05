using UnityEngine;
using System.Collections;

public class ApplePicker : MonoBehaviour {

public GameObject basketPrefab;
public int numBaskets = 3;
public float basketBottomY = -14f;
public float basketSpacingY = 2f;

void Start () {
for (int i=0; i<numBaskets; i++) {
GameObject tBasketGO = Instantiate( basketPrefab ) as GameObject;
Vector3 pos = Vector3.zero;
pos.y = basketBottomY + ( basketSpacingY * i );
tBasketGO.transform.position = pos;
}
}
}