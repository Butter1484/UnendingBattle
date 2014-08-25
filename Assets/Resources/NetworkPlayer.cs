using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {
	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	Animator anim;
	bool gotFirstUpdate = false;
	float realAimAngle = 0;

	// Use this for initialization
	void Start () {
		InitAnim ();
	}
	void InitAnim() {
		if(anim == null){


			anim = GetComponent<Animator>();
		}
	}
	// Update is called once per frame
	void Update () {
	if(photonView.isMine) {
		}
		else {
			transform.position = Vector3.Lerp (transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRotation, 0.1f);
			anim.SetFloat ("AimAngle", Mathf.Lerp(anim.GetFloat ("AimAngle"), realAimAngle, 0.1f));
		}

	}
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		InitAnim ();
		if(stream.isWriting) {
			//Sending Player Position
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
			stream.SendNext (anim.GetFloat("Speed"));
			stream.SendNext (anim.GetBool("Jumping"));
			stream.SendNext (anim.GetFloat ("AimAngle"));
		}
		else  {
			//Recieve other player positions and update view

			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();
			anim.SetFloat("Speed", (float)stream.ReceiveNext());
			anim.SetBool("Jumping", (bool)stream.ReceiveNext());
			realAimAngle = (float)stream.ReceiveNext ();

		
		if(gotFirstUpdate == false) {
				transform.position = realPosition;
				transform.rotation = realRotation;
				anim.SetFloat ("AimAngle", realAimAngle);
				gotFirstUpdate = true;
			}
		}

	}
}
