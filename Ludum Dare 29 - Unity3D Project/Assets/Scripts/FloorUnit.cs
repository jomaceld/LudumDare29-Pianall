using UnityEngine;
using System.Collections;

public class FloorUnit : MonoBehaviour {

	Animator anim;
	AudioSource audioSource;
	public Color activeColor;

	public int id = -1;
	bool soundPlayed = false;
	public GameController controller;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();

		reset();
	}

	void OnCollisionEnter(Collision collision) 
	{
		if(!soundPlayed)
		{
			if(controller != null)
				controller.playNextTileNote(id,audioSource);

			soundPlayed = true;
		}
	}

	public void setColor(Color c)
	{
		this.renderer.material.color = c;
	}

	public void animateRoll()
	{
		if(anim != null)
			anim.SetTrigger("Click");
	}

	public void flash()
	{
		this.renderer.material.color = activeColor;
	}

	public void reset()
	{
		if(controller != null && !controller.levelFinished)
			this.renderer.material.color = Color.black;
		soundPlayed = false;
	}
}
