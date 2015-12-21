using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using	UnityEngine.EventSystems;

public class ScoreManager : UIBehaviour {
	private Text scoreText;

	public Text ScoreText {
		get {
			if(scoreText == null) {
				scoreText = GetComponent<Text>();
			}
			return scoreText;
		}
	}

	void UpdateScoreText(int score) {
		ScoreText.text = score.ToString();
	}

	// Use this for initialization
	protected override void Start () {
		UpdateScoreText(GameManager.Score.Value);
		GameManager.Score.AddListener(UpdateScoreText);
	}

	protected override void OnDestroy() {
		if(GameManager.Instance != null) {
			GameManager.Score.RemoveListener(UpdateScoreText);
		}
	}
}
