using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class QuitButtonManager : UIBehaviour {

	void OnClick() {
		
	}

	protected override void OnDestroy() {
		base.OnDestroy();
		GetComponent<Button>().onClick.RemoveListener(OnClick);
	}

	protected override void Awake() {
		base.Awake();
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
}
