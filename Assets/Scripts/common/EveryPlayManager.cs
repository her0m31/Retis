using UnityEngine;
using System.Collections;

public class EveryPlayManager : MonoBehaviour {

	IEnumerator Wait() {
		yield return new WaitForSeconds(1.0f);
		Everyplay.SetMetadata("score", GameManager.Score.Value);
		Everyplay.StopRecording();
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Ready:
			Everyplay.StartRecording();
			break;
			case GameManager.GameState.GameOver:
			StartCoroutine(Wait());
			break;
		}
	}

	public void OnShareButton() {
		Everyplay.ShowSharingModal();
	}

	public void OnReadyForRecording(bool enabled) {
  	if(enabled) {
    	// The recording is supported
    	// myGameObject.SetUpRecording();
  	}
	}

	// Use this for initialization
	void Start () {
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
		// Register for the Everyplay ReadyForRecording event
		Everyplay.ReadyForRecording += OnReadyForRecording;
	}
}
