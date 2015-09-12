using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeText : MonoBehaviour {

	private Text textUI;

	// Use this for initialization
	void Start () {
		textUI = transform.FindChild("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeText(string newText){
		textUI.text = newText;
		print ("Hola hola");
	}
}
