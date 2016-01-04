using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	private Nortification<int> score;
	private Nortification<GameState> state;
	private GameObject thisGameObject;

	public static Nortification<int> Score {
		get {return Instance.score;}
	}

	public static Nortification<GameState> State {
		get {return Instance.state;}
	}

	public new GameObject gameObject {
		get {return thisGameObject == null ? thisGameObject = base.gameObject : thisGameObject;}
	}

	public enum GameState {
		Title, Ready, Playing, GameOver, Restart
	}

	public static bool IsPlay() {
		return State.Value == GameState.Playing ? true : false;
	}

	IEnumerator Restart() {
		yield return new WaitForSeconds(0.10f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:

			break;
			case GameManager.GameState.Ready:

			break;
			case GameManager.GameState.Playing:
			if(Application.platform == RuntimePlatform.Android) {
				Everyplay.StartRecording();
			}
			break;
			case GameManager.GameState.GameOver:
			if(Application.platform == RuntimePlatform.Android) {
				Everyplay.SetMetadata("score", Score.Value);
				Everyplay.StopRecording();
				Everyplay.ShowSharingModal();
			}
			break;
			case GameManager.GameState.Restart:
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			break;
		}
	}

	public void OnReadyForRecording(bool enabled) {
		if(enabled) {
			// The recording is supported
			// gameObject.SetUpRecording();
		}
	}

	void OnDestroy() {
		Score.DisposeOf();
		State.DisposeOf();
	}

	void Start () {
		State.AddListener(OnChangeGameState);

		// Register for the Everyplay ReadyForRecording event
		if(Application.platform == RuntimePlatform.Android) {
			Everyplay.ReadyForRecording += OnReadyForRecording;
		}
	}

	void Awake() {
		if(this != Instance) {
			Destroy(this);
			return;
		}
		score = new Nortification<int>(0);
		state = new Nortification<GameState>(GameState.Title);
	}
}
