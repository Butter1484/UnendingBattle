using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {


	
	private float gldepth = -0.5f;
	private float startTime = 0.1f;
	

	
	private int tris = 0;
	private int verts = 0;
	private float savedTimeScale;
	//private  pauseFilter;
	public bool isPaused = false;
	private bool showfps;
	private bool showtris;
	private bool showvtx;
	private bool showfpsgraph;
	

	
	int lowFPS = 30;
	int highFPS = 50;
	
	//var start:GameObject;
	
	string url = "unity.html";
	

	

	
	enum Page {
		None,Main,Options,Quit
	}
	

	

	
	void Start() {
		//fpsarray = new int[Screen.width];
		Time.timeScale = 1.0f;
		//pauseFilter = Camera.main.GetComponent(SepiaToneEffect);
		//PauseGame();
	}
	
	/*void OnPostRender() {
		if (showfpsgraph && mat != null) {
			GL.PushMatrix ();
			GL.LoadPixelMatrix();
			for (var i = 0; i < mat.passCount; ++i)
			{
				mat.SetPass(i);
				GL.Begin( GL.LINES );
				for (var x=0; x<fpsarray.length; ++x) {
					GL.Vertex3(x,fpsarray[x],gldepth);
				}
				GL.End();
			}
			GL.PopMatrix();
			ScrollFPS();
		}
	}
	
	void ScrollFPS() {
		for (var x=1; x<fpsarray.length; ++x) {
			fpsarray[x-1]=fpsarray[x];
		}
		if (fps < 1000) {
			fpsarray[fpsarray.length-1]=fps;
		}
	}
	*/
	/*static void IsDashboard() {
		return Application.platform == RuntimePlatform.OSXDashboardPlayer;
	}
	
	static void IsBrowser() {
		return (Application.platform == RuntimePlatform.WindowsWebPlayer ||
		        Application.platform == RuntimePlatform.OSXWebPlayer);
	}
	*/
	/*void LateUpdate () {
		if (showfps || showfpsgraph) {
			FPSUpdate();
		}

		if (Input.GetKeyDown("escape")) {
			switch (currentPage) {
			case Page.None: PauseGame(); break;
			case Page.Main: if (!IsBeginning()) UnPauseGame(); break;
			default: currentPage = Page.Main;
			}
		}
	}
	*/
	/*void OnGUI () {
		if (skin != null) {
			GUI.skin = skin;
		}
		ShowStatNums();
		ShowLegal();
		if (IsGamePaused()) {
			GUI.color = statColor;
			switch (currentPage) {
			case Page.Main: PauseMenu(); break;
			case Page.Options: ShowToolbar(); break;
			case Page.Credits: ShowCredits(); break;
				
			}
		}	
	}

	void ShowLegal() {
		if (!IsLegal()) {
			GUI.Label(Rect(Screen.width-100,Screen.height-20,90,20),
			          "fugugames.com");
		}
	}
	
	void IsLegal() {
		return !IsBrowser() || 
			Application.absoluteURL.StartsWith("http://www.fugugames.com/") ||
				Application.absoluteURL.StartsWith("http://fugugames.com/");
	}
	*/
	//private var toolbarInt:int=0;
	//private var toolbarStrings: String[]= ["Audio","Graphics","Stats","System"];
	
	/*void ShowToolbar() {
		BeginPage(300,300);
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarStrings);
		switch (toolbarInt) {
		case 0: VolumeControl(); break;
		case 3: ShowDevice(); break;
		case 1: Qualities(); QualityControl(); break;
		case 2: StatControl(); break;
		}
		EndPage();
	}
	*/
	/*void ShowCredits() {
		BeginPage(300,300);
		for (var credit in credits) {
			GUILayout.Label(credit);
		}
		for (var credit in crediticons) {
			GUILayout.Label(credit);
		}
		EndPage();
	}
	*/
	/*void ShowBackButton() {
		if (GUI.Button(Rect(20,Screen.height-50,50,20),"Back")) {
			currentPage = Page.Main;
		}
	}
	*/
	
	/*void ShowDevice() {
		GUILayout.Label ("Unity player version "+Application.unityVersion);
		GUILayout.Label("Graphics: "+SystemInfo.graphicsDeviceName+" "+
		                SystemInfo.graphicsMemorySize+"MB\n"+
		                SystemInfo.graphicsDeviceVersion+"\n"+
		                SystemInfo.graphicsDeviceVendor);
		GUILayout.Label("Shadows: "+SystemInfo.supportsShadows);
		GUILayout.Label("Image Effects: "+SystemInfo.supportsImageEffects);
		GUILayout.Label("Render Textures: "+SystemInfo.supportsRenderTextures);
	}
*/
/*	void Qualities() {
		GUILayout.Label(QualitySettings.names[QualitySettings.GetQualityLevel()]);
	}
	
	void QualityControl() {
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Decrease")) {
			QualitySettings.DecreaseLevel();
		}
		if (GUILayout.Button("Increase")) {
			QualitySettings.IncreaseLevel();
		}
		GUILayout.EndHorizontal();
	}
	
	void VolumeControl() {
		GUILayout.Label("Volume");
		AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume,0.0,1.0);
	}

	void StatControl() {
		GUILayout.BeginHorizontal();
		showfps = GUILayout.Toggle(showfps,"FPS");
		showtris = GUILayout.Toggle(showtris,"Triangles");
		showvtx = GUILayout.Toggle(showvtx,"Vertices");
		showfpsgraph = GUILayout.Toggle(showfpsgraph,"FPS Graph");
		GUILayout.EndHorizontal();
	}
	
	void FPSUpdate() {
		var delta = Time.smoothDeltaTime;
		if (!IsGamePaused() && delta !=0.0) {
			fps = 1 / delta;
		}
	}
	
	void ShowStatNums() {
		GUILayout.BeginArea(Rect(Screen.width-100,10,100,200));
		if (showfps) {
			var fpsString= fps.ToString ("#,##0 fps");
			GUI.color = Color.Lerp(lowFPSColor, highFPSColor,(fps-lowFPS)/(highFPS-lowFPS));
			GUILayout.Label (fpsString);
		}
		if (showtris || showvtx) {
			GetObjectStats();
			GUI.color = statColor;
		}
		if (showtris) {
			GUILayout.Label (tris+"tri");
		}
		if (showvtx) {
			GUILayout.Label (verts+"vtx");
		}
		GUILayout.EndArea();
	}
	*/
	void BeginPage() {
		GUILayout.BeginArea( new Rect(0,0,Screen.width,Screen.height));
	}
	
	void EndPage() {
		GUILayout.EndArea();
		/*if (currentPage != Page.Main) {
			ShowBackButton();
		}*/
	}
	
	/*void IsBeginning() {
		return Time.time < startTime;
	}*/
	void QuitGame() {
		Application.Quit();
	} 
	
	void PauseMenu() {
		//BeginPage(200,200);
		isPaused = true;
		if (GUILayout.Button ("Play" , "Continue")) {
			UnPauseGame();
			
		}
		/*if (GUILayout.Button ("Options")) {
			currentPage = Page.Options;
		}
		if (GUILayout.Button ("Credits")) {
			currentPage = Page.Credits;
		}*/
		if (GUILayout.Button ("Quit")) {
			Debug.Log("Should be quitting");
			
			PhotonNetwork.Disconnect();
			Application.Quit();
		}
		/*if (IsBrowser() && !IsBeginning() && GUILayout.Button ("Restart")) {
			Application.OpenURL(url);
		}*/
		EndPage();
	}
	
	/*void GetObjectStats() {
		verts = 0;
		tris = 0;
		var ob = FindObjectsOfType(GameObject);
		for (var obj in ob) {
			GetObjectStats(obj);
		}
	}

	void GetObjectStats(object) {
		var filters : Component[];
		filters = object.GetComponentsInChildren(MeshFilter);
		for( var f : MeshFilter in filters )
		{
			tris += f.sharedMesh.triangles.Length/3;
			verts += f.sharedMesh.vertexCount;
		}
	}
	*/
	void PauseGame() {
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;
		
		//pauseFilter.enabled = true;

		
	}
	
	void UnPauseGame() {
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
		isPaused = false;
		
		
		//if (pauseFilter) pauseFilter.enabled = false;
		//currentPage = Page.None;
		//if (IsBeginning() && start != null) {
		//	start.active = true;
		}

	
	void IsGamePaused() {
		//return Time.timeScale==0;
	}
	
	void OnApplicationPause() {
		//if (IsGamePaused()) {
			AudioListener.pause = true;
		//}
	}
}
