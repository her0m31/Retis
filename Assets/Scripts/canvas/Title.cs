using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class Title : UIBehaviour {
	private Text thisText;
	public Text text {
		get {return thisText == null ? thisText = base.GetComponent<Text>() : thisText;}
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
				text.text = "Retis.";
				text.fontSize = 70;
				text.enabled  = true;
				break;
			case GameManager.GameState.GameOver:
				text.text = "   Game Over...";
				text.fontSize = 50;
				text.enabled  = true;
				break;
			default:
				text.enabled = false;
				break;
		}
	}

	protected override void OnDestroy() {
		base.OnDestroy();
		
		if(GameManager.Instance != null) {
			GameManager.State.RemoveListener(OnChangeGameState);
		}
	}

	protected override void Start() {
		base.Start();

		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}
}
