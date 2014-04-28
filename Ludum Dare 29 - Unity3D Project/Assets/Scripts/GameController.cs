using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {


	public MusicPlayer mPlayer;
	public GameObject player;

	public int currentState = 0;

	string[] songs =  { //Songs???? :D
		"C-1,C,C2",
		"C,D,E,F",
		"G,A,F,F-1,C",
		"A,A,B,B,C,D",
		"C,F,C,G,C,G2,C",
		"C-1,D,E-1,F,G-1,A,B-1,C",
		"C,C,G,G,A,A,G,F,F,E,E,D,D,C",
		"C2,G,A,E,F,C,F,G,E,D,C,B,A,G,A,B" 
	};

	string[] songNotes;
	static int currentSong = 0;
	int noteIndex;

	public bool levelFinished = false;
	float elapsedTime;
	public List<GameObject> tilesList;
	static int seed = (int)System.DateTime.Now.Ticks;

	public GUIText continueText, startText, levelClearedText;
	// Use this for initialization
	void Start () {

		Random.seed = seed;
		noteIndex = 0;
		elapsedTime = 0;
		songNotes = songs[currentSong].Split(',');
		populateMap();

		if(startText != null)
			startText.enabled = true;
		if(continueText != null)
			continueText.enabled = false;
		if(levelClearedText != null)
			levelClearedText.enabled = false;
	}

	public void populateMap()
	{
		List<Vector3> usedPositions = new List<Vector3>();
		usedPositions.Add(tilesList[0].transform.position);
		int numOfNotes = songNotes.Length;

		for(int i = 0; i < numOfNotes;i++)
		{
			Vector3 pos;

			do{
				int direction = Random.Range(0,4);
				pos = tilesList[tilesList.Count-1].transform.position;
				switch(direction) {
				case 0:
					pos += Vector3.right;
					break;
				case 1:
					pos += Vector3.left;
					break;
				case 3:
					pos += Vector3.forward;
					break;
				}
				
			}while(usedPositions.Contains(pos) || Mathf.Abs(pos.x) > 5 );
			

			GameObject ob = (GameObject) Instantiate(tilesList[tilesList.Count-1],pos,tilesList[tilesList.Count-1].transform.localRotation);
			ob.name = "FloorUnit_" + i;
			ob.GetComponent<FloorUnit>().id = i;
			ob.GetComponent<FloorUnit>().controller = this;
			ob.GetComponent<FloorUnit>().activeColor = new Color ( Random.Range (0f, 0.7f), Random.Range (0f, 0.7f), Random.Range (0f, 0.7f));

			usedPositions.Add(pos);
			tilesList.Add(ob);
		}
	}

	public void playNextTileNote(int id,AudioSource source)
	{
		if(id != noteIndex) // If is the wrong order
		{
			tilesList[id+1].GetComponent<FloorUnit>().flash();
			tilesList[id+1].GetComponent<FloorUnit>().animateRoll();
			mPlayer.playNote("WRONG",source);
		}else
		{
			tilesList[id+1].GetComponent<FloorUnit>().flash();
			mPlayer.playNote(songNotes[noteIndex],source);
			noteIndex ++;

			if(noteIndex == songNotes.Length) // Level Completed
			{
				levelFinished = true;
				noteIndex = 0;
				currentState = 0;
				elapsedTime = -0.2f;

				continueText.enabled = true;
				levelClearedText.enabled = true;
				player.transform.position += Vector3.up;
			}
		}
	}

	public void playbackSong()
	{
		// Set first tile color
		tilesList[0].GetComponent<FloorUnit>().setColor(Color.grey);

		if(songNotes.Length > noteIndex && elapsedTime > 0.4f) // Play a note each 0.4 seconds
		{
			// Reset previous tile
			if(noteIndex > 0 && !levelFinished)
				tilesList[noteIndex].GetComponent<FloorUnit>().reset();
			// Illuminate current tile
			tilesList[noteIndex+1].GetComponent<FloorUnit>().flash();
			// Animate roll if its the end of level repetition
			if(levelFinished && noteIndex+1 < tilesList.Count)
				tilesList[noteIndex+1].GetComponent<FloorUnit>().animateRoll();
			// Play sound note
			mPlayer.playNote(songNotes[noteIndex],tilesList[noteIndex+1].GetComponent<AudioSource>());

			noteIndex ++;
			elapsedTime = 0;
		}
	}

	public void changeNextLevel()
	{
		// move to next song
		currentSong ++;
		// initialize new seed
		seed = (int)System.DateTime.Now.Ticks;
		
		if(currentSong >= songs.Length){ // Last song
			PlayerPrefs.SetInt("gameFinished",1);

			Application.LoadLevel("MainMenu");
		}
		else
			Application.LoadLevel(Application.loadedLevel);
	}

	// Update is called once per frame
	void Update () {

		if(currentState == 0) // Playback song
		{
			elapsedTime += Time.deltaTime;
			playbackSong();
			// Space pressed
			if(Input.GetKeyUp(KeyCode.Space))
			{
				if(levelFinished)
					changeNextLevel();
				else
				{
					startText.enabled = false;
					// Reset all (except last) floor tiles
					for(int i = 1; i<tilesList.Count-1;i++)
						tilesList[i].GetComponent<FloorUnit>().reset();
					// Move to play state
					currentState = 1;
					noteIndex = 0;
					// Illuminate last tile
					tilesList[tilesList.Count-1].GetComponent<FloorUnit>().flash();
				}
			}
		}else if(levelFinished && Input.GetKeyUp(KeyCode.Space))
		{
			changeNextLevel();
		}

		// Restart level if R is pressed
		if(Input.GetKeyUp(KeyCode.R))
			Application.LoadLevel(Application.loadedLevel);

	}
}
