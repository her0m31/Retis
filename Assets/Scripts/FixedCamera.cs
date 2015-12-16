using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FixedCamera : MonoBehaviour {
	public Vector2 aspect = new Vector2(9.0f, 16.0f);
	private Camera fixedCamera;
	private float aspectRate;

	void UpdateScreenRate() {
		float nowAspect = (float)aspect.y / (float)aspect.x;
		float newAspect = (float)Screen.height / (float)Screen.width;

		float changeAspect = nowAspect / newAspect;
		fixedCamera.rect = new Rect(0.0f, (1.0f - changeAspect)*0.5f, 1.0f, changeAspect);

		if(newAspect < nowAspect) {
			changeAspect = newAspect / nowAspect;
			fixedCamera.rect = new Rect((1.0f - changeAspect)*0.5f, 0.0f, changeAspect, 1.0f);
		}
	}

	bool IsChangeAspect() {
		if(fixedCamera.aspect != aspectRate) {
			return true;
		}
		return false;
	}

	// Use this for initialization
	void Start () {
		aspectRate  = (float)aspect.x / (float)aspect.y;
		fixedCamera = GetComponent<Camera>();

		UpdateScreenRate();
	}

	// Update is called once per frame
	void Update () {
		if(IsChangeAspect()) {
			UpdateScreenRate();
			fixedCamera.ResetAspect();
		}
	}

}
