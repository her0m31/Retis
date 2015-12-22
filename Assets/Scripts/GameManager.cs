using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	private Nortification<int> score;
	private Nortification<GameState> state;

	public enum GameState {
		Title,
		Playing,
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
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void Awake() {
		if(this != Instance) {
			Destroy(this);
			return;
		}

		score = new Nortification<int>(0);
		state = new Nortification<GameState>(GameState.Title);
	}

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
