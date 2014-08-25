using UnityEngine;
using System.Collections;

public class TeamMember : MonoBehaviour {

	int _teamID = 0;
	public int teamID {
		get{return _teamID;}
	}

	[RPC]
	void SetTeamID (int id){
		_teamID = id;
		SkinnedMeshRenderer mySkin = this.transform.GetComponentInChildren<SkinnedMeshRenderer>();
		if(mySkin == null){
			Debug.LogError ("Couldn't find SkinnedMeshRenderer");
		}
		if(teamID == 1){
			mySkin.material.color = Color.red;
		}
		if(teamID == 2){
			mySkin.material.color = Color.blue;
		}
	}
}
