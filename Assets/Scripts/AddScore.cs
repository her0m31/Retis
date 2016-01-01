using UnityEngine;

public class AddScore : MonoBehaviour {

  void OnCollisionEnter2D(Collision2D coll) {
    // 衝突したオブジェクトのタグが"AddScore"かつゲームプレイ中の時
    if(coll.gameObject.CompareTag("AddScore") && GameManager.IsPlay()) {
      GameManager.Score.Value += 1;
    }
  }
}
