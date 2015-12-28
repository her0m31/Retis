using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blinker : MonoBehaviour {
	private GameObject thisGameObject;
	private float addAlpha = 0.015f;

	public new GameObject gameObject {
		get {
			return thisGameObject == null ? thisGameObject = base.gameObject : thisGameObject;
		}
	}

	void Update() {
		float currentAlpha = gameObject.GetComponent<Text>().color.a;
		// Alphaが0 または 1になったら増減値を反転
		if (currentAlpha < 0 || currentAlpha > 1) {
			addAlpha *= -1;
		}
		// Alpha値を増減させてセット
		gameObject.GetComponent<Text>().color = new Color(243.0f/255.0f, 156.0f/255.0f, 18.0f/255.0f, (currentAlpha + addAlpha));
	}

	void Awake() {
		gameObject.GetComponent<Text>().color = new Color(243.0f/255.0f, 156.0f/255.0f, 18.0f/255.0f, 125.0f/255.0f);
	}
}
