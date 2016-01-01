using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blinker : MonoBehaviour {
	private float addAlpha;
	private float addFontSize;
	private float prevFontSize;

	private Text thisText;
	public Text text {
		get {return thisText == null ? thisText = base.gameObject.GetComponent<Text>() : thisText;}
	}

	void Update() {
		// テキストの点滅処理
		Color color = text.color;
		float currentAlpha = color.a;
		// Alphaが0 または 1になったら増減値を反転
		if(1.0f < currentAlpha || currentAlpha < 0.1f) {
			addAlpha *= -1;
		}
		// Alpha値を増減させてセット
		text.color = new Color(color.r, color.g, color.b, (currentAlpha + addAlpha));

		// テキストの拡大縮小処理
		int currentFontSize = text.fontSize;
		switch(text.text) {
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

		text.fontSize = (int)prevFontSize;
	}

	void Awake() {
		prevFontSize = 150.0f;
		addFontSize  = 0.5f;
		addAlpha     = 0.014f;
	}
}
