using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown() {
		Application.LoadLevel("LevelPlayer");
	}

	void OnMouseEnter() {
		guiText.color = Color.white;
	}

	void OnMouseExit() {
		guiText.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
