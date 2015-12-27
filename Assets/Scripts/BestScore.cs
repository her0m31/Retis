using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class BestScore : UIBehaviour {
	private Text bestScoreText;

	public Text BestScoreText {
		get {
			return bestScoreText == null ? GetComponent<Text>() : bestScoreText;
		}
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
				BestScoreText.enabled = true;
				break;
			case GameManager.GameState.GameOver:
				BestScoreText.enabled = true;
				break;
			default:
				BestScoreText.enabled = false;
				break;
		}
	}

	protected override void OnDestroy() {
		if(GameManager.Instance != null) {
			GameManager.State.RemoveListener(OnChangeGameState);
		}
	}

	protected override void Start () {
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}
}
