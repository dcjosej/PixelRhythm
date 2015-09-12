using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{

	public enum ArrowPosition
	{
		LEFT,
		RIGHT,
		UP,
		DOWN
	}
	public ArrowPosition arrowPosition;
	public float beat;
	public Transform strumBarTransform;

	//----------------- Debug fields   ----------------------
	public int id;
	//-------------------------------------------------------


	public float strumTime { get; private set; }

	private float secondsBeat;
	public float timeToStrum; //TODO: Private
	public float velocity;	//TODO: Private
	public float distanceToStrumBar; //TODO: Delete this field
	private bool moving; //Check when note start to move


	public void Initialize (ArrowPosition arrowPosition, int beat, int bpm)
	{
		this.arrowPosition = arrowPosition;
		this.beat = beat;
		this.secondsBeat = 60.0f / bpm;
		this.strumTime = beat * secondsBeat;
	}

	public void Start ()
	{
		timeToStrum = 0;
		velocity = 0f; //Vertical velocity
		moving = false;

		float xPos = 0f;
		float yPos = 0f;
		float zRotation = 0f;

		switch (arrowPosition) {
		
		case ArrowPosition.LEFT:
			xPos = strumBarTransform.position.x;
			zRotation = 90f;
			break;
		
		case ArrowPosition.UP:
			xPos = strumBarTransform.position.x + 1.5f;
			zRotation = 0f;
			break;

		case ArrowPosition.DOWN:
			xPos = (2 * 1.5f) + strumBarTransform.position.x;
			zRotation = 180f;
			break;
		
		case ArrowPosition.RIGHT:
			xPos = (3 * 1.5f) + strumBarTransform.position.x;
			zRotation = 270f;
			break;
		}

		this.transform.position = new Vector2 (xPos, 15f);
		this.transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, zRotation);
	}

	public void Update ()
	{


		timeToStrum = strumTime - (MusicManager.instance.backgroundMusic.timeSamples * (1.0f / MusicManager.instance.backgroundMusic.clip.frequency));
		timeToStrum = (float)System.Math.Round(timeToStrum, 2);

		//MusicManager.instance.song.timeSamples / MusicManager.instance.song.clip.fre
		//timeToStrum = strumTime - MusicManager.instance.song.time;

		if (timeToStrum <= 5 && !moving) {
			//print ("Moviendo nota!");
			//MusicManager.instance.addNote (this);


			//float distanceToStrumBar = Vector2.Distance (this.transform.position, strumBarTransform.position);
			float distanceToStrumBar = this.transform.position.y - strumBarTransform.position.y;
			this.distanceToStrumBar = distanceToStrumBar;
			velocity = distanceToStrumBar / timeToStrum;
			//velocity = (float)System.Math.Round(distanceToStrumBar / timeToStrum, 1);	
			moving = true;

		}

		//float newX = transform.position.x;
		//float newY = strumBarPosition.y - (MusicManager.instance.song.time - strumTime);


		transform.position = new Vector2 (transform.position.x, transform.position.y - velocity * Time.deltaTime);


		checkPositionToDelete ();

	}

	private void checkPositionToDelete ()
	{
		if (transform.position.y <= -5f) {
			dequeueNote(this);
			Destroy (gameObject);
		}
	}

	private void dequeueNote (Note note)
	{
		switch (note.arrowPosition) {
		case ArrowPosition.UP:
			MusicManager.instance.upQueue.Dequeue();
			break;
		case ArrowPosition.DOWN:
			MusicManager.instance.downQueue.Dequeue();
			break;
		case ArrowPosition.LEFT:
			MusicManager.instance.leftQueue.Dequeue();
			break;
		case ArrowPosition.RIGHT:
			MusicManager.instance.rightQueue.Dequeue();
			break;
		}
	}
}