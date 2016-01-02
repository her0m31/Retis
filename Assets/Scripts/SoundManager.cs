using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	private AudioSource titleBgm;
	private AudioSource playBgm;
	private AudioSource overBgm;
	private AudioSource outBgm;

	void PlayTitleBGM() {
		if(!(titleBgm.isPlaying)) {
			titleBgm.Play();
		}
		if(playBgm.isPlaying) {
			playBgm.Stop();
		}
		if(overBgm.isPlaying) {
			overBgm.Stop();
		}
	}

	void PlayPlayingBGM() {
		if(!(playBgm.isPlaying)) {
			playBgm.Play();
		}
		if(titleBgm.isPlaying) {
			titleBgm.Stop();
		}
		if(overBgm.isPlaying) {
			overBgm.Stop();
		}
	}

	void PlayGameOverBGM() {
		if(!(overBgm.isPlaying)) {
			overBgm.Play();
		}
		if(titleBgm.isPlaying) {
			titleBgm.Stop();
		}
		if(playBgm.isPlaying) {
			playBgm.Stop();
		}
	}

	void StopAllBGM() {
		if(titleBgm.isPlaying) {
			titleBgm.Stop();
		}
		if(playBgm.isPlaying) {
			playBgm.Stop();
		}
		if(overBgm.isPlaying) {
			overBgm.Stop();
		}
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
				PlayTitleBGM();
				break;
			case GameManager.GameState.Playing:
				PlayPlayingBGM();
				break;
			case GameManager.GameState.GameOver:
				outBgm.PlayOneShot(outBgm.clip);
				PlayGameOverBGM();
				break;
			default:
				StopAllBGM();
				break;
		}
	}

	void Start() {
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}

	void Awake() {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		titleBgm = audioSources[0];
		playBgm = audioSources[1];
		overBgm = audioSources[2];
		outBgm = audioSources[3];
	}
}
