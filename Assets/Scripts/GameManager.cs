using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
	private Nortification<int> score;
	private Nortification<GameState> state;

	public enum GameState {
		Title,
		playing,
		GameOver,
		Restart
	}

	public static Nortification<int> Score {
		get {
			return Instance.score;
		}
	}

	public static Nortification<GameState> State {
		get {
			return Instance.state;
		}
	}

	IEnumerator Restart() {
		yield return new WaitForSeconds(0.3f);
		Application.LoadLevel(Application.loadedLevel);
	}

	void Awake() {
		score = new Nortification<int>(0);
		state = new Nortification<GameState>(GameState.Title);
	}

	// Use this for initialization
	void Start () {
		State.AddListener((state) => {
			if(state == GameState.Restart) {
				StartCoroutine(Restart());
			}
		});
	}

	void OnDestroy() {
		Score.DisposeOf();
		State.DisposeOf();
	}
}
