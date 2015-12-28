using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {
  void OnCollisionEnter2D(Collision2D coll) {
    if(coll.gameObject.CompareTag("AddScore") && GameManager.IsPlaying()) {
      GameManager.CurrentScore += 1;
    }
  }
}
