using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDManager : MonoBehaviour {

	public Text pointText;

	private static HUDManager _instance;

	public static HUDManager instance{
		get{
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<HUDManager>();
			}
			return _instance;
		}
	}




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updatePointText(){
		pointText.text = GameManager.instance.points.ToString();
	}
}
