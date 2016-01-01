using UnityEngine;

public class DestroyObject : MonoBehaviour {
	private float time = 2.0f;

	void Start() {
		Destroy(base.gameObject, time);
	}
}
