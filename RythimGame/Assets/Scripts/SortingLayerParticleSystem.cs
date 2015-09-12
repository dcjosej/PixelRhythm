using UnityEngine;
using System.Collections;

public class SortingLayerParticleSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ParticleSystemRenderer psr = GetComponent<ParticleSystemRenderer>();
		psr.sortingLayerName = "GoodNoteEmitter";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
