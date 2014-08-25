#pragma strict
private var gldepth = -0.5;
private var startTime = 0.1;
var mat:Material;
private var tris = 0;
private var verts = 0;
private var savedTimeScale:float;
private var pauseFilter;
 
private var showfps:boolean;
private var showtris:boolean;
private var showvtx:boolean;
private var showfpsgraph:boolean;
 
var lowFPSColor = Color.red;
var highFPSColor = Color.green;
 
var lowFPS = 30;
var highFPS = 50;
var statColor:Color = Color.yellow;
private var fpsarray:int[];
private var fps:float;
 
function Start() {
	fpsarray = new int[Screen.width];
	Time.timeScale = 1.0;
	//pauseFilter = Camera.main.GetComponent(SepiaToneEffect);

}
 
function OnPostRender() {
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
 
function ScrollFPS() {
	for (var x=1; x<fpsarray.length; ++x) {
		fpsarray[x-1]=fpsarray[x];
	}
	if (fps < 1000) {
		fpsarray[fpsarray.length-1]=fps;
	}
}
 



function ShowDevice() {
	GUILayout.Label ("Unity player version "+Application.unityVersion);
	GUILayout.Label("Graphics: "+SystemInfo.graphicsDeviceName+" "+
	SystemInfo.graphicsMemorySize+"MB\n"+
	SystemInfo.graphicsDeviceVersion+"\n"+
	SystemInfo.graphicsDeviceVendor);
	GUILayout.Label("Shadows: "+SystemInfo.supportsShadows);
	GUILayout.Label("Image Effects: "+SystemInfo.supportsImageEffects);
	GUILayout.Label("Render Textures: "+SystemInfo.supportsRenderTextures);
}
function FPSUpdate() {
	var delta = Time.smoothDeltaTime;
		if (delta !=0.0) {
			fps = 1 / delta;
		}
}

 

function ShowStatNums() {
	GUILayout.BeginArea(Rect(Screen.width-100,10,100,200));
	if (showfps) {
		var fpsString= fps.ToString ("#,##0 fps");
		GUI.color = Color.Lerp(lowFPSColor, highFPSColor,(fps-lowFPS)/(highFPS-lowFPS));
		GUILayout.Label (fpsString);
	}

	if (showtris) {
		GUILayout.Label (tris+"tri");
	}
	if (showvtx) {
		GUILayout.Label (verts+"vtx");
	}
	GUILayout.EndArea();
}
 