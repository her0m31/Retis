using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class GameOver : UIBehaviour {
	private Text gameOverText;

	public Text GameOverText {
		get {
			return gameOverText == null ? GetComponent<Text>() : gameOverText;
		}
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.GameOver:
				GameOverText.enabled = true;
				break;
			default:
				GameOverText.enabled = false;
				break;
		}
	}

	protected override void OnDestroy() {
		base.OnDestroy();
		if(GameManager.Instance != null) {
			GameManager.State.RemoveListener(OnChangeGameState);
		}
	}

	protected override void Start () {
		base.Start();
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}
}
