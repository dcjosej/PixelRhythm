using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class StrumbarRecordScript : MonoBehaviour
{

	public SpriteRenderer arrowCoverUp;
	public SpriteRenderer arrowCoverDown;
	public SpriteRenderer arrowCoverLeft;
	public SpriteRenderer arrowCoverRight;
	public Color pushColor;

	void Start ()
	{
	}
	
	void Update ()
	{
		CheckKeyboard ();
	}

	private void CheckKeyboard ()
	{

		//----------- KEY DOWN ------------------------
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			arrowCoverUp.color = pushColor;	
			RecordSong.instance.recordNote(KeyCode.UpArrow, MusicManager.instance.time);
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			arrowCoverDown.color = pushColor;		
			RecordSong.instance.recordNote(KeyCode.DownArrow, MusicManager.instance.time);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			arrowCoverLeft.color = pushColor;		
			RecordSong.instance.recordNote(KeyCode.LeftArrow, MusicManager.instance.time);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			arrowCoverRight.color = pushColor;
			RecordSong.instance.recordNote(KeyCode.RightArrow, MusicManager.instance.time);
		}

		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			RecordSong.instance.saveSongToFile();
		}




		//----------- KEY UP ------------------------

		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			arrowCoverUp.color = Color.white;		
		}
		
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			arrowCoverDown.color = Color.white;		
		}
		
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			arrowCoverLeft.color = Color.white;		
		}
		
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			arrowCoverRight.color = Color.white;		
		}
	}
}