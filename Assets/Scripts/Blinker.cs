using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blinker : MonoBehaviour {
	private GameObject thisGameObject;
	private float addAlpha;
	private float addFontSize;
	private float prevFontSize;

	public new GameObject gameObject {
		get {
			return thisGameObject == null ? thisGameObject = base.gameObject : thisGameObject;
		}
	}

	void Update() {
		float currentAlpha = gameObject.GetComponent<Text>().color.a;
		// Alphaが0 または 1になったら増減値を反転
		if(1.0f < currentAlpha || currentAlpha < 0.1f) {
			addAlpha *= -1;
		}
		// Alpha値を増減させてセット
		gameObject.GetComponent<Text>().color = new Color(243.0f/255.0f, 156.0f/255.0f, 18.0f/255.0f, (currentAlpha + addAlpha));

		int currentFontSize = gameObject.GetComponent<Text>().fontSize;
		switch(gameObject.GetComponent<Text>().text) {
			case "-":
				if(165 < currentFontSize || currentFontSize < 135) {
					addFontSize *= -1;
				}
				prevFontSize += addFontSize;
				break;
			case "2":
				if(135 <= currentFontSize) {
					prevFontSize = 100;
				}
				if(115 < currentFontSize || currentFontSize < 85) {
					addFontSize *= -1;
				}
				prevFontSize += addFontSize;
				break;
		}

		gameObject.GetComponent<Text>().fontSize = (int)prevFontSize;
	}

	void Awake() {
		prevFontSize = 150.0f;
		addFontSize  = 0.5f;
		addAlpha     = 0.014f;
	}
}
