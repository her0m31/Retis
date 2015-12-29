using UnityEngine;
using System.Collections;

public class ViewController : MonoBehaviour {
	private RectTransform thisRectTransform;

	public RectTransform rectTransform {
		get {
			return thisRectTransform == null ? GetComponent<RectTransform>() : thisRectTransform;
		}
	}

	public virtual string Title {
		get {
			return "";
		}
		set {
		}
	}

	void Awake() {
		thisRectTransform = GetComponent<RectTransform>();
	}
}
