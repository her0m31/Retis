using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {

  void OnTriggerExit2D(Collider2D ogj) {
    Debug.Log(GameManager.Score.Value);
    GameManager.Score.Value += 1;
  }
}
