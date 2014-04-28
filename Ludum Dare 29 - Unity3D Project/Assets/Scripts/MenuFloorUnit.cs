using UnityEngine;
using System.Collections;

public class MenuFloorUnit : MonoBehaviour {

	Animator anim;
	AudioSource audioSource;
	public MusicPlayer mPlayer;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		newColor();
	}

	void OnMouseEnter() 
	{
		if(anim != null)
			anim.SetTrigger("MenuRoll_Anim_Trigger");
		if(mPlayer != null)
			mPlayer.playRandomNote(audioSource);

		newColor();
	}

	public void newColor()
	{
		this.renderer.material.color = new Color ( Random.Range (0f, 0.7f), Random.Range (0f, 0.7f), Random.Range (0f, 0.7f));
	}
}
