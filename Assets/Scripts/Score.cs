using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using	UnityEngine.EventSystems;

public class Score : UIBehaviour {
	private Text scoreText;

	public Text ScoreText {
		get {
			return scoreText == null ? GetComponent<Text>() : scoreText;
		}
	}

	void UpdateScoreText(int score) {
		ScoreText.text = score.ToString();
	}

	protected override void OnDestroy() {
		if(GameManager.Instance != null) {
			GameManager.Score.RemoveListener(UpdateScoreText);
		}
	}

	protected override void Start () {
		UpdateScoreText(GameManager.Score.Value);
		GameManager.Score.AddListener(UpdateScoreText);
	}

	protected override void Awake() {
		scoreText = GetComponent<Text>();
	}
}
