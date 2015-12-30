using UnityEngine;
using System.Collections;

public class InActive : MonoBehaviour {

	void Awake() {
		gameObject.SetActive(false);
	}
}
