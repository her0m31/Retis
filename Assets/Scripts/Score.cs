using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using	UnityEngine.EventSystems;

public class Score : UIBehaviour {
	private RectTransform thisRectTransform;
	private Text scoreText;

	public RectTransform rectTransform {
		get {
			return thisRectTransform == null ? GetComponent<RectTransform>() : thisRectTransform;
		}
	}

	public Text ScoreText {
		get {
			return scoreText == null ? GetComponent<Text>() : scoreText;
		}
	}


	void UpdateScoreText(int score) {
		ScoreText.text = score.ToString();
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Playing:
				scoreText.enabled = true;
				break;
			case GameManager.GameState.GameOver:
				rectTransform.localPosition = new Vector3(-112.0f, -385.0f, 0.0f);
				ScoreText.text = "Score\n" + ScoreText.text;
				ScoreText.fontSize = 40;
				scoreText.enabled = true;
				break;
			default:
				scoreText.enabled = false;
				break;
		}
	}

	protected override void OnDestroy() {
		base.OnDestroy();
		if(GameManager.Instance != null) {
			GameManager.Score.RemoveListener(UpdateScoreText);
		}
	}

	protected override void Start () {
		base.Start();
		UpdateScoreText(GameManager.CurrentScore);
		GameManager.Score.AddListener(UpdateScoreText);
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}

	protected override void Awake() {
		base.Awake();
		scoreText = GetComponent<Text>();
		thisRectTransform = GetComponent<RectTransform>();
	}
}
