using UnityEngine;
using System.Collections;

//TODO: Rename methods to adapt them to C# nomenclature
//TODO: Change "firs" and "second" for "short" and "long"

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public static GameManager instance
	{
		get{
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<GameManager>();
			}
			return _instance;
		}
	}

	public Animator dancerAnimator;

	//----------------- Particle effects --------------------
	public Transform goodStreakParticleSystem;
	public ParticleSystem firstGoodStrask;
	public ParticleSystem secondGoodStrask;
	//-------------------------------------------------------


	private int _goodCounter {get; set;}

	public int multiplier {get; private set;}



	//-----------------------------------------
	private int _points;
	public int points {
		get{
			return _points;
		} 

		//Update points text whenever updates points
		set{
			_points = value;
			HUDManager.instance.updatePointText();
		}
	}


	//------------- Methods ----------------------------

	

	void Start () {
		points = 0;
		_goodCounter = 0;
		multiplier = 1;
	}
	
	void Update () {
	
	}	

	public void increaseGoodCounter(){
		_goodCounter++;

		if(_goodCounter == 15){
			dancerAnimator.SetTrigger("dance2");
			dancerAnimator.ResetTrigger("dance1");

			Instantiate(goodStreakParticleSystem);

			firstGoodStrask.Play();
			secondGoodStrask.Stop();

			MusicManager.instance.PlayGoodStraskSound();

			multiplier = 2;

		}else if(_goodCounter == 40){
			dancerAnimator.ResetTrigger("dance1");
			dancerAnimator.ResetTrigger("dance2");
			dancerAnimator.SetTrigger("dance3");
			Instantiate(goodStreakParticleSystem);

			firstGoodStrask.Stop();
			secondGoodStrask.Play();

			multiplier = 3;
		}
	}

	public void resetGoodCounter(){
		_goodCounter = 0;
		multiplier = 1;

		firstGoodStrask.Stop();
		secondGoodStrask.Stop();

		dancerAnimator.SetTrigger("dance1");
		dancerAnimator.ResetTrigger("dance2");
		dancerAnimator.ResetTrigger("dance3");
	}

	private void activateDance(string dance){
		dancerAnimator.SetTrigger(dance);
	}
}