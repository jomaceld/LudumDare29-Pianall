using UnityEngine;
using System.Collections;

public class MainMenuTextElement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public bool animate = false;
	float elapsedTime = 0;
	// Update is called once per frame
	void Update () {

		if(animate)
		{
			elapsedTime += Time.deltaTime;

			if(elapsedTime > 0.1f)
				guiText.color += new Color(0,0,0,0.01f);
		}
	
	}

	public void hide()
	{
		guiText.color -= new Color(0,0,0,1);
	}

	public void show()
	{
		animate = true;
		//guiText.enabled = true;
	}

}
