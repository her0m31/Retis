using UnityEngine;
using System.Collections;

public class BackKey : MonoBehaviour {

	void Update() {
		// エスケープキー(Android->バックキー)が押された時
		if(Input.GetKey(KeyCode.Escape)) {
			// タイトルでバックキーが押されたらアプリ終了
			if(GameManager.State.Value == GameManager.GameState.Title) {
				Application.Quit();
				return;
			}
			// ゲームステートをリスタートにして初期化、タイトル画面へ
			GameManager.State.Value = GameManager.GameState.Restart;
		}

		// Androidのホームキーが押された時 -> アプリ終了
		if(Input.GetKey(KeyCode.Home)) {
			Application.Quit();
			return;
		}
	}
}
