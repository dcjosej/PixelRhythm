using UnityEngine;
using System.Collections;

public class CreateNote : MonoBehaviour {


	// Use this for initialization
	void Start () {
		InvokeRepeating("ShowNote", 0.47f, 0.47f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShowNote(){
		//print("Pulse!");
	}
}
