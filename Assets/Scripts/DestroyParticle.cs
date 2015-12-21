using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {
	public float time = 2.0f;

	void Start() {
		Destroy(gameObject, time);
	}
}
