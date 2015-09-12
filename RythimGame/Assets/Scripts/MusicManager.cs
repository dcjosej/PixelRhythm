using UnityEngine;
using System.Collections;
using System.Linq;
using SimpleJSON;

public class MusicManager : MonoBehaviour
{

	private static MusicManager _instance;
	public static MusicManager instance{
		get{
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<MusicManager>();
			}
			return _instance;
		}
	}

	public AudioSource backgroundMusic {get; private set;} 
	public AudioSource sfx {get; private set;}

	public float time{
		get{
			return backgroundMusic.timeSamples * (1.0f/backgroundMusic.clip.frequency);
		}
	}

	//--------------Sounds---------------------
	public AudioClip goodStraskSound;
	//-----------------------------------------


	public TextAsset JSONstring;

	public Transform notePrefab; //Note prefab for instantiate when we iterate through JSON file


	//Four queues for notes. One queue for line
	public Queue rightQueue { get; private set; }
	public Queue leftQueue { get; private set; }
	public Queue upQueue { get; private set; }
	public Queue downQueue { get; private set; }

	void Awake ()
	{
		AudioSource [] audioSources = GetComponents<AudioSource>();

		backgroundMusic = audioSources[0];
		sfx = audioSources[1];

		rightQueue = new Queue ();
		leftQueue = new Queue();
		upQueue = new Queue();
		downQueue = new Queue();

		var N = JSON.Parse(JSONstring.text);

		foreach(JSONNode node in N["notes"].Childs){
			//TODO: Change access to bmp for only one access before the loop
			instantiateNote(int.Parse(node["beat"]), node["arrow"], int.Parse(N["bpm"]));
		}
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void addNote (Note note)
	{
		switch (note.arrowPosition) {

		case Note.ArrowPosition.RIGHT:
			rightQueue.Enqueue(note);
			break;
		case Note.ArrowPosition.LEFT:
			leftQueue.Enqueue(note);
			break;
		case Note.ArrowPosition.UP:
			upQueue.Enqueue(note);
			break;
		case Note.ArrowPosition.DOWN:
			downQueue.Enqueue(note);
			break;
		
		}
	}

	public void instantiateNote(int beat, string arrow, int bpm){

		Note.ArrowPosition arrowPosition = Note.ArrowPosition.DOWN;
		switch(arrow){
		case "up":
			arrowPosition = Note.ArrowPosition.UP;
			break;
		case "down":
			arrowPosition = Note.ArrowPosition.DOWN;
			break;
		case "left":
			arrowPosition = Note.ArrowPosition.LEFT;
			break;
		case "right":
			arrowPosition = Note.ArrowPosition.RIGHT;
			break;
		}


		Note note = ((Transform)Instantiate(notePrefab, Vector3.zero, Quaternion.identity)).GetComponent<Note>();
		note.Initialize(arrowPosition, beat, bpm);
		addNote(note);

	}

	
	public void PlayGoodStraskSound()
	{
		print ("PLAYING GOOD STREAKS SOUND!!");

		this.sfx.clip = goodStraskSound;
		this.sfx.PlayOneShot(goodStraskSound);
	}
}
