using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour {


	float transpose = -4;

	static Dictionary<string, int> notes = new Dictionary<string, int>() {
		{"C-1",-12},{"D-1",-10},{"E-1",-8}, {"F-1",-7},{"G-1",-5},{"A-1",-3},{"B-1",-1},
		{"C",0},{"D",2}, {"E",4}, {"F",5}, {"G",7}, {"A",9}, {"B",11}, 
		{"C2",12}, {"D2",14} ,{"E2",16}, {"F2",17} , {"G2",19}, {"WRONG",-10} 
	};

	public void playNote(string note,AudioSource source)
	{
		int noteNumber = notes[note];
		source.pitch =  Mathf.Pow(2, (noteNumber+transpose)/12.0f);
		source.Play();
	}

	public void playRandomNote(AudioSource source)
	{
		int noteNumber = Random.Range(0,notes.Count);
		source.pitch =  Mathf.Pow(2, (noteNumber+transpose)/12.0f);
		source.Play();
	}
}
