using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class BestScore : UIBehaviour {
	const string prefsKey = "BEST_SCORE";
	private Text thisText;
	private string baseText;
	private int bestScore;

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
			case GameManager.GameState.Title:
				gameObject.SetActive(true);
				UpdateBestScoreText();
				text.text = baseText + bestScore.ToString();
				break;
			default:
				gameObject.SetActive(false);
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
		baseText  = text.text;
		text.text = baseText + bestScore.ToString();
	}
}
