using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	NetworkManager network;
	public float hitPoints = 100f;
	float currentHitPoints;
	public bool isDead = false;

	
	// Use this for initialization
	void Start () {

		currentHitPoints = hitPoints;
		network = GameObject.FindObjectOfType<NetworkManager>();
	}
	
	[RPC]
	public void TakeDamage(float amt) {
		currentHitPoints -= amt;
		
		if(currentHitPoints <= 0) {
			Die();
		}
	}
	
	void OnGUI() {
		if(GetComponent<PhotonView>().isMine && gameObject.tag == "Player"){
			if(GUI.Button (new Rect (Screen.width-100, 0, 100, 40), "Suicide")){
				if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition)){
					network.hover = true;
				}
				Die ();
			}

		}
	}


public void Die () {
        if(GetComponent<PhotonView>().instantiationId == 0) {

		Destroy(gameObject);
		Debug.Log (name + " was destroyed");
		}

		else{
			if(GetComponent<PhotonView>().isMine){
				if(gameObject.tag == "Player"){
					GetComponent<NetworkManager>();
					network.standbyCamera.enabled = true;
					network.respawnTimer = 5f;
					isDead = true;
					network.isDead = true;
					network.AddChatMessage(PhotonNetwork.player.name + " has died");
				}
		PhotonNetwork.Destroy (gameObject);


				//network.OnGUI();

			}
		}
		network.hover = false;
	}

	public void Respawn(){
		Debug.Log ("Respawn has occured");
		network.AddChatMessage (PhotonNetwork.player.name + " has respawned");
		//network.SpawnMyPlayer();
		isDead = false;
	}
	}


