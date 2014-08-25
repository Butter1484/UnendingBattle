using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	SpawnSpot[] spawnSpots;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (transform.position.y <-7) {
			spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
			RespawnPlayer();	
		}

	}
	void RespawnPlayer(){
		SpawnSpot mySpawnSpot = spawnSpots[Random.Range (0, spawnSpots.Length)];
		transform.position = mySpawnSpot.transform.position;
		Debug.Log ("Respawn");
	}

}
