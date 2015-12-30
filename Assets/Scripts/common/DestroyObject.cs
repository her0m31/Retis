using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {
	private float time = 2.0f;

	void Start() {
		Destroy(gameObject, time);
	}
}
