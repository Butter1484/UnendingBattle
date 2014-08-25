using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {
	public GameObject sniperVisual;

[RPC]
	void SniperBulletFX( Vector3 startPos, Vector3 endPos) {
		Debug.Log ("SniperBulletFX");
		GameObject sniperFX = (GameObject)Instantiate (sniperVisual, startPos, Quaternion.LookRotation (endPos - startPos));
		LineRenderer lr = sniperFX.transform.Find ("Line").GetComponent<LineRenderer>();
		lr.SetPosition (0, startPos);
		lr.SetPosition (1, endPos);
	}
}
