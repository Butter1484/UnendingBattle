using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	public Camera standbyCamera;
	public bool offlineMode = false;
	bool connecting = false;
	List<string> chatMessages;
	int maxChatMessages = 5;
	private string message = "Chat Message Filler";
	//int chatTime = 7;
	//float timeSent = 0f;
	//float timeHide = 0f;
	public SpawnSpot[] spawnSpots;
	public team1[] Team1;
	public team2[] Team2;
	Health health;
	bool isRespawning = false;
	public bool isDead = false;
	bool userHasHitReturn = false;
	string roomName = "Room";
	string players = "0";
	bool connected = false;
	public float respawnTimer = 0;
	public bool hasPickedTeam = false;
	int teamID = 0;
	public string version = "Alpha 2.0.4";
	public int randomTeam = 1;
	string team = "0";
	bool cursorLocked = false;
	bool firstClick = false;
	public bool hover = false;
	SpawnSpot spawn;
	public string nameEnt = "";
	//Pause pause;
	//bool paused = false;


		// Use this for initialization
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		PhotonNetwork.player.name = PlayerPrefs.GetString("Username", "Noob");
		chatMessages = new List<string>();
		health = GameObject.FindObjectOfType<Health>();
		//pause = GameObject.FindObjectOfType<Pause>();

	}
	void OnDestroy (){
		PlayerPrefs.SetString ("Username", PhotonNetwork.player.name);
	}
	public void AddChatMessage (string m) {GetComponent<PhotonView>().RPC ("AddChatMessage_RPC", PhotonTargets.All, m);
		//timeSent = Time.deltaTime;
		//timeHide = Time.deltaTime + 7;
		Debug.Log ("AddChatMessage(m)");
		hover = false;
	}
	[RPC]
	 void AddChatMessage_RPC (string m) {
		while(chatMessages.Count >= maxChatMessages) {
			chatMessages.RemoveAt (0);
		}
		chatMessages.Add (m);
	}

	void Connect () {

	
		connecting = true;

		PhotonNetwork.ConnectUsingSettings ("Alpha 2.1.0");


	}
	public void OnGUI () {
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
		GUILayout.Label ("ALPHA BUILD 2.1.0");
		if(PhotonNetwork.connected == true){
		GUILayout.Label (PhotonNetwork.player.name);
		}
		GUILayout.Label (roomName);
		GUILayout.Label (players + " Players");


		if(PhotonNetwork.connected == false && connecting == false) {
			GUILayout.BeginArea ( new Rect(0,0, Screen.width, Screen.height));
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical ();
			GUILayout.FlexibleSpace ();
			GUILayout.BeginHorizontal();
		
			GUILayout.Label ("Username");
		    
		    PhotonNetwork.player.name =	GUILayout.TextField(PhotonNetwork.player.name, 20);
			GUILayout.EndHorizontal ();
			GUILayout.FlexibleSpace ();

			if(GUILayout.Button ("Single Player")) {
				PhotonNetwork.offlineMode = true;
				//Skip Connection
				OnJoinedLobby ();
				Debug.LogWarning ("Offline Mode");
			}
			if(GUILayout.Button ("Multi Player")) {
				Connect ();
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndVertical ();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea ();
		}
	 /* if(health.isDead == true) {





			
			
			
			if(GUILayout.Button ("Respawn")) {
				health.Respawn ();
				Debug.Log ("Would Respawn if code was working");
			}

			
		}*/
		if(PhotonNetwork.connected == true && connecting == false) {

			if(hasPickedTeam){
			GUILayout.BeginArea (new Rect(0,0, Screen.width, Screen.height));
			GUILayout.BeginVertical ();
			GUILayout.FlexibleSpace ();
			//while (timeSent != Time.deltaTime - 7) {

			foreach(string msg in chatMessages) {
				GUILayout.Label(msg);


			}
			GUILayout.BeginHorizontal ();
			message = GUILayout.TextField (message, 150);
			//Event e = Event.current;
			
			//if (e.keyCode == KeyCode.Return) userHasHitReturn = true;
			//The following works, but is deprecated
			Input.eatKeyPressOnTextFieldFocus = false;
			/*if (Event.current == (Event.KeyboardEvent('return'))) {
				enter = true;
			}*/
			if(GUILayout.Button ("Send") && message != "Chat Message Filler") {
				Debug.Log ("Message should be sending");
				AddChatMessage (PhotonNetwork.player.name + ": " + message);
				message = "Chat Message Filler";
			}
				if (Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition)){
					hover = true;
				}

			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}
			else {
				GUILayout.BeginArea ( new Rect(0,0, Screen.width, Screen.height));
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.BeginVertical ();
				GUILayout.FlexibleSpace ();
				GUILayout.BeginHorizontal();

				
				if(GUILayout.Button ("Red Team")) {
					//Team1 = GameObject.FindObjectsOfType<team1>();
					SpawnMyPlayer (1);
					hasPickedTeam = true;

				}
				if(GUILayout.Button ("Blue Team")) {
					//Team2 = GameObject.FindObjectsOfType<team2>();
					SpawnMyPlayer (2);
					hasPickedTeam = true;
				}
				if(GUILayout.Button ("Random")) {
					randomTeam = Random.Range (1,3);
					SpawnMyPlayer (randomTeam);
					hasPickedTeam = true;
					Debug.Log (randomTeam);
				}
				if(GUILayout.Button ("Renegade")){
					SpawnMyPlayer (0);
					hasPickedTeam = true;
				}
				GUILayout.FlexibleSpace ();
				GUILayout.EndVertical ();
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.EndArea ();
			}
		}
	}

	void OnJoinedLobby () {

		PhotonNetwork.JoinRandomRoom ();
		Debug.Log ("OnJoinedLobby");
	}
	void OnPhotonRandomJoinFailed () {
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom ("Development " + this.version);
	}
	void OnJoinedRoom () {
		connecting = false;

		roomName = PhotonNetwork.room.name;
	Debug.Log ("OnJoinedRoom");


	}

	public void SpawnMyPlayer (int teamID) {
		this.teamID = teamID;
		connected = true;
		if (teamID == 1){
		//spawnSpots = GameObject.FindObjectsOfType<spawn.teamId.1>();
		}
		if (teamID == 2){
		//	spawnSpots = GameObject.FindObjectsOfType<spawn.teamId.>();
		}
		if (teamID == 0){
		//	spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		}
		if(isRespawning == false) {

			AddChatMessage (PhotonNetwork.player.name + " has joined");
		}
		if(isRespawning == true){
			AddChatMessage (PhotonNetwork.player.name + " has respawned");
		}
		if (spawnSpots == null) {
			Debug.LogError ("No Spawns!");
			return;
		}
		//if (teamID == 0) {
		SpawnSpot mySpawnSpot = spawnSpots[Random.Range (0, spawnSpots.Length)];

		GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0 );

		/*if (teamID == 1){
			Team1 mySpawnSpot = Team1[Random.Range (0, Team1.Length)];
			
			GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0 );

		}
		if (teamID == 2){
			Team2 mySpawnSpot = Team2[Random.Range (0, Team2.Length)];
			
			GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0 );

		}*/
			standbyCamera.enabled = false;

		//((MonoBehaviour)myPlayerGO.GetComponent("FPSInputController")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("MouseLook")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("PlayerMovement")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent ("PlayerShooting")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent ("Health")).enabled = true;

		myPlayerGO.GetComponent<PhotonView>().RPC ("SetTeamID", PhotonTargets.AllBuffered, teamID);
		//((MonoBehaviour)myPlayerGO.GetComponent ("CharacterMotor")).enabled = true;
		myPlayerGO.transform.FindChild ("Main Camera").gameObject.SetActive (true);
	}
	void OnMouseDown () {
		// Lock the cursor
		Screen.lockCursor = true;
		Debug.Log("Cursor should be locked");
	}
	void Update(){
		if (Input.GetMouseButtonUp(0) && connected == true && cursorLocked == false && firstClick == false){
			firstClick = true;
		}
		if (Input.GetMouseButton(0) && connected == true && cursorLocked == false && firstClick == true){
			Screen.lockCursor = true;
			cursorLocked = true;
			Debug.Log("Cursor should be locked");
		}
		if (Input.GetKeyDown ("tab") && cursorLocked == true){
			Screen.lockCursor = false;
			cursorLocked = false;
			firstClick = false;
		}
		if(userHasHitReturn == true && message != "Chat Message Filler") {
			Debug.Log ("Message should be sending");
			AddChatMessage (PhotonNetwork.player.name + ": " + message);
			message = "Chat Message Filler";
			userHasHitReturn = false;

		}
		if(Input.GetKey("escape")){
			PhotonNetwork.Disconnect ();
			Application.Quit ();
		}
		if(connected == true) {
		players = PhotonNetwork.room.playerCount.ToString();
		if(respawnTimer > 0){
				respawnTimer -= Time.deltaTime;

				if(respawnTimer <= 0){
					isRespawning = true;
					SpawnMyPlayer (teamID);
				}
			}
		}

		/*if(pause.isPaused == true && paused == false){
			((MonoBehaviour)myPlayerGO.GetComponent("MouseLook")).enabled = false;
			((MonoBehaviour)myPlayerGO.GetComponent ("PlayerShooting")).enabled = false;
			paused = true;
		}
		if(pause.isPaused == false && paused == true){
			((MonoBehaviour)myPlayerGO.GetComponent("MouseLook")).enabled = true;
			((MonoBehaviour)myPlayerGO.GetComponent ("PlayerShooting")).enabled = true;
			paused = false;
		}*/
		/*if(health.isDead == true){
			OnGUI ();*/

		}

	}
	/*void Respawn() {
		if(isRespawning == true){
		 ((MonoBehaviour)myPlayerGo).enabled = false;
			isRespawning = false;
		}*/
	

