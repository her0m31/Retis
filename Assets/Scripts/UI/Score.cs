using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using	UnityEngine.EventSystems;

public class Score : UIBehaviour {
	private Text thisText;

	public Text text {
		get {
			return text == null ? thisText = base.GetComponent<Text>() : thisText;
		}
	}

	void UpdateScoreText(int score) {
		text.text = score.ToString();
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
	}

	protected override void Awake() {
		base.Awake();
		thisText = base.GetComponent<Text>();
	}
}
