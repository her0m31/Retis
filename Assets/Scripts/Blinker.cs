using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blinker : MonoBehaviour {
	private GameObject thisGameObject;
	private float _Step = 0.015f;

	public new GameObject gameObject {
		get {
			return thisGameObject == null ? thisGameObject = base.gameObject : thisGameObject;
		}
	}

	void Update() {
		// 現在のAlpha値を取得
		float toColor = gameObject.GetComponent<Text>().color.a;
		// Alphaが0 または 1になったら増減値を反転
		if (toColor < 0 || toColor > 1) {
			_Step = _Step * -1;
		}
		// Alpha値を増減させてセット
		gameObject.GetComponent<Text>().color = new Color(243.0f/255.0f, 156.0f/255.0f, 18.0f/255.0f, (toColor + _Step));
	}

	void Awake() {
		gameObject.GetComponent<Text>().color = new Color(243.0f/255.0f, 156.0f/255.0f, 18.0f/255.0f, 125.0f/255.0f);
	}
}
