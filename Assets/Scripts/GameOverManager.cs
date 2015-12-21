using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Text))]
public class GameOverManager : UIBehaviour {
	private Text gameOverText;

	public Text GameOverText {
		get {
			if(gameOverText == null) {
				gameOverText = GetComponent<Text>();
			}
			return gameOverText;
		}
	}

	void GameStateChange(GameManager.GameState state) {
		if(state == GameManager.GameState.GameOver) {
			GameOverText.enabled = true;
		}
		else {
			GameOverText.enabled = false;
		}
	}

	protected override void Start () {
		base.Start();
		GameStateChange(GameManager.State.Value);
		GameManager.State.AddListener(GameStateChange);
	}

	protected override void OnDestroy() {
		base.OnDestroy();
		if(GameManager.Instance != null) {
			GameManager.State.RemoveListener(GameStateChange);
		}
	}
}
