using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RecordSong : MonoBehaviour
{

	private static RecordSong _instance;

	public static RecordSong instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<RecordSong> ();
			}
			return _instance;
		}
	}
	
	private JSONNode root = JSON.Parse ("{}");
	private int notesCounter = 0;


	// Use this for initialization
	void Start ()
	{
		root ["bpm"].AsInt = 128;
	}
	
	void Update ()
	{
	}

	public void recordNote (KeyCode arrow, float time)
	{

		int beat = (int)(time / (60.0f / 128));

		


		string arrowString = "";

		switch (arrow) {
		case KeyCode.UpArrow:
			arrowString = "up";
			break;
		case KeyCode.DownArrow:
			arrowString = "down";
			break;
		case KeyCode.LeftArrow:
			arrowString = "left";
			break;
		case KeyCode.RightArrow:
			arrowString = "right";
			break;
		}

		root ["notes"] [notesCounter] ["beat"].AsInt = beat;
		root ["notes"] [notesCounter] ["arrow"] = arrowString;

		notesCounter++;

	}


	public void saveSongToFile(){
		System.IO.File.WriteAllText("C:\\Users\\dcjav1\\archivoPrueba2.json", root.ToString());
	}
}