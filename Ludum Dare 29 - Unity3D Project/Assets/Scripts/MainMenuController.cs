using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public MainMenuTextElement ldtext,authorText, playButtonText, thanksText, titleText;
	float elapsedTime = 0;
	bool animate = false;

	// Use this for initialization
	void Start () {

		// Hide all texts
		ldtext.hide(); authorText.hide(); playButtonText.hide(); 
		thanksText.hide();
		titleText.hide();

		if(PlayerPrefs.GetInt("gameFinished") == 1)
		{
			animate = true;
			thanksText.show();
			print ("thanks show");

		}else 
		{
			ldtext.show();
			authorText.show();
			playButtonText.show();
			titleText.show();
		}
	}


	void Update () {
		
		if(animate)
		{
			elapsedTime += Time.deltaTime;
			
			if(elapsedTime > 3f) // Show thanks text during 3 seconds;
			{
				animate = false;
				thanksText.gameObject.SetActive(false);
				ldtext.show();
				authorText.show();
				playButtonText.show();
				titleText.show();
			}
			PlayerPrefs.SetInt("gameFinished",0);
		}

		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

}
