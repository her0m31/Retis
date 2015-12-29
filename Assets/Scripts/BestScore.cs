using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class BestScore : UIBehaviour {
	private RectTransform thisRectTransform;
	const string prefsKey = "BEST_SCORE";
	private Text bestScoreText;
	private int bestScore;

	public RectTransform rectTransform {
		get {
			return thisRectTransform == null ? GetComponent<RectTransform>() : thisRectTransform;
		}
	}

	public Text BestScoreText {
		get {
			return bestScoreText == null ? GetComponent<Text>() : bestScoreText;
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
				BestScoreText.enabled = true;
				break;
			case GameManager.GameState.GameOver:
				UpdateBestScoreText();
				bestScoreText.text = "Best\n" + bestScore.ToString();
				rectTransform.localPosition = new Vector3(rectTransform.localPosition.x -90.0f, rectTransform.localPosition.y, rectTransform.localPosition.z);
				BestScoreText.enabled = true;
				break;
			default:
				BestScoreText.enabled = false;
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
		bestScoreText = GetComponent<Text>();
		bestScore = PlayerPrefs.GetInt(prefsKey, 0);
		bestScoreText.text = "BestsScore\n" + bestScore.ToString();
	}
}
