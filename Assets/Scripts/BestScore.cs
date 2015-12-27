using PlayerPrefs = PreviewLabs.PlayerPrefs;
using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class BestScore : UIBehaviour {
	const string prefsKey = "BEST_SCORE";
	private Text bestScoreText;
	private int bestScore;

	public Text BestScoreText {
		get {
			return bestScoreText == null ? GetComponent<Text>() : bestScoreText;
		}
	}

	private void UpdateBestScoreText() {
		if (bestScore < GameManager.Score.Value) {
			bestScore = GameManager.Score.Value;
			PlayerPrefs.SetInt(prefsKey, bestScore);
			bestScoreText.text = "Best Score\n<" + bestScore.ToString() + ">";
		}
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
				BestScoreText.enabled = true;
				break;
			case GameManager.GameState.GameOver:
				UpdateBestScoreText();
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

	public void OnApplicationQuit() {
		PlayerPrefs.Flush();
	}

	protected override void Start () {
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}

	protected override void Awake() {
		bestScoreText = GetComponent<Text>();

		bestScore = PlayerPrefs.GetInt(prefsKey, 0);
		bestScoreText.text = "Best Score\n<" + bestScore.ToString() + ">";
	}
}
