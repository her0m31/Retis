using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class BestScore : UIBehaviour {
	const string prefsKey = "BEST_SCORE";
	private RectTransform thisRectTransform;
	private Text thisText;
	private int bestScore;

	public RectTransform rectTransform {
		get {
			return thisRectTransform == null ? thisRectTransform = base.GetComponent<RectTransform>() : thisRectTransform;
		}
	}

	public Text text {
		get {
			return thisText == null ? thisText = base.GetComponent<Text>() : thisText;
		}
	}

	private void UpdateBestScoreText() {
		if (bestScore < GameManager.Score.Value) {
			bestScore = GameManager.Score.Value;
			PlayerPrefs.SetInt(prefsKey, bestScore);
		}
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.GameOver:
				UpdateBestScoreText();
				text.text = text.text + bestScore.ToString();
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

	protected override void Awake() {
		base.Awake();
		bestScore = PlayerPrefs.GetInt(prefsKey, 0);
		text.text = text.text + bestScore.ToString();
	}
}
