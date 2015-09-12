using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;


//TODO: Change "pushed" for "pressed"
//TODO: Refactor repeated switch sentences

public class StrumbarScript : MonoBehaviour
{
	private enum Result
	{
		GOOD,
		MEDIOCRE,
		BAD
	}

	public SpriteRenderer arrowCoverUp;
	public SpriteRenderer arrowCoverDown;
	public SpriteRenderer arrowCoverLeft;
	public SpriteRenderer arrowCoverRight;
	public Color pushColor;
	public Transform pointsAnim;

	//------------ Particle system for good note ------------------
	public Transform goodNoteParticleSystem;

	//Animation controller for points animations
	public Animator pointsAnimController;

	//------------ Arrow covers positions -----------------------
	private Vector3 positionArrowCoverUp;
	private Vector3 positionArrowCoverDown;
	private Vector3 positionArrowCoverLeft;
	private Vector3 positionArrowCoverRight;

	void Start ()
	{
		positionArrowCoverUp = transform.Find ("arrowCoverUp").position;
		positionArrowCoverDown = transform.Find ("arrowCoverDown").position;
		positionArrowCoverLeft = transform.Find ("arrowCoverLeft").position;
		positionArrowCoverRight = transform.Find ("arrowCoverRight").position;

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
			checkNotes (KeyCode.UpArrow);
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			arrowCoverDown.color = pushColor;		
			checkNotes (KeyCode.DownArrow);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			arrowCoverLeft.color = pushColor;		
			checkNotes (KeyCode.LeftArrow);
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			arrowCoverRight.color = pushColor;		
			checkNotes (KeyCode.RightArrow);
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

	private void checkNotes (KeyCode arrowPushed)
	{

		Note currentNote = null;
		Queue currentQueue = null;

		switch (arrowPushed) {
		case KeyCode.UpArrow:
			currentQueue = MusicManager.instance.upQueue;
			break;
		case KeyCode.DownArrow:
			currentQueue = MusicManager.instance.downQueue;
			break;
		case KeyCode.LeftArrow:
			currentQueue = MusicManager.instance.leftQueue;
			break;
		case KeyCode.RightArrow:
			currentQueue = MusicManager.instance.rightQueue;
			break;
		}


		if (currentQueue.Count > 0) {
			currentNote = (Note)currentQueue.Peek ();

			float differenceTime = Math.Abs (MusicManager.instance.time - currentNote.strumTime);


			if (differenceTime >= 0.15f) {

				print("Difference of time: " + differenceTime);
				print ("Song time: " + MusicManager.instance.time + " Strum time: " + currentNote.strumTime);

				GameManager.instance.points -= 20;
				//currentQueue.Dequeue ();
				InstantiatePointsAnim (arrowPushed, Result.BAD);
			} else if (differenceTime < 0.15f && differenceTime >= 0.1f) {
				GameManager.instance.points += 50;
				//currentQueue.Dequeue ();
				InstantiatePointsAnim (arrowPushed, Result.MEDIOCRE);

			} else {
				GameManager.instance.points += 100;
				//currentQueue.Dequeue ();
				InstantiatePointsAnim (arrowPushed, Result.GOOD);
				InstantiateGoodNoteParticleSystem(arrowPushed);
			}
		}else{
			GameManager.instance.points -= 20;
			InstantiatePointsAnim (arrowPushed, Result.BAD);
		}
	}

	private void InstantiateGoodNoteParticleSystem(KeyCode arrowPushed){
		switch(arrowPushed){
		case KeyCode.UpArrow:
			Instantiate(goodNoteParticleSystem, new Vector3(positionArrowCoverUp.x, positionArrowCoverUp.y, 0), Quaternion.Euler (-90, 0, 0));
			break;
		case KeyCode.DownArrow:
			Instantiate(goodNoteParticleSystem, new Vector3(positionArrowCoverDown.x, positionArrowCoverDown.y, 0), Quaternion.Euler (-90, 0, 0));
			break;
		case KeyCode.LeftArrow:
			Instantiate(goodNoteParticleSystem, new Vector3(positionArrowCoverLeft.x, positionArrowCoverLeft.y, 0), Quaternion.Euler (-90, 0, 0));
			break;
		case KeyCode.RightArrow:
			Instantiate(goodNoteParticleSystem, new Vector3(positionArrowCoverRight.x, positionArrowCoverRight.y, 0), Quaternion.Euler (-90, 0, 0));
			break;
		}
	}


	//TODO: Create variable for literal points
	private void InstantiatePointsAnim (KeyCode arrowPushed, Result result)
	{

		string message = "";
		Color colorMessage = Color.black;

		switch (result) {
		case Result.GOOD:
			message =  "+" + 100 * GameManager.instance.multiplier;
			colorMessage = Color.green;
			GameManager.instance.increaseGoodCounter();
			break;
		case Result.MEDIOCRE:
			message = "+40";
			colorMessage = Color.blue;
			break;
		case Result.BAD:
			message = "-20";
			colorMessage = Color.red;
			GameManager.instance.resetGoodCounter();
			break;
		}

		Transform animPointsTransform = null; //Transform of the points canvas animation

		switch (arrowPushed) {

		case KeyCode.UpArrow:
			animPointsTransform = (Transform)Instantiate (pointsAnim, new Vector3 (positionArrowCoverUp.x + 1.3f, positionArrowCoverUp.y + 3f, 0), Quaternion.identity);
			break;
		case KeyCode.DownArrow:
			animPointsTransform = (Transform)Instantiate (pointsAnim, new Vector3 (positionArrowCoverDown.x + 1.3f, positionArrowCoverDown.y + 3f, 0), Quaternion.identity);
			break;
		case KeyCode.LeftArrow:
			animPointsTransform = (Transform)Instantiate (pointsAnim, new Vector3 (positionArrowCoverLeft.x + 1.3f, positionArrowCoverLeft.y + 3f, 0), Quaternion.identity);
			break;
		case KeyCode.RightArrow:
			animPointsTransform = (Transform)Instantiate (pointsAnim, new Vector3 (positionArrowCoverRight.x + 1.3f, positionArrowCoverRight.y + 3f, 0), Quaternion.identity);
			break;
		}

		Text textUi = animPointsTransform.FindChild ("Text").GetComponent<Text> ();
		textUi.color = colorMessage;
		textUi.text = message;
	}
}