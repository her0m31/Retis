using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
  protected static T instance;

  public static T Instance {
    get {
      if(instance == null) {
        instance = (T)FindObjectOfType(typeof(T));

        if(instance == null) {
          // ログがでる時とでない時がある。おそらくインスタンスの廃棄タイミングの兼ね合いでログが表示されている。
          // 実行には問題なく見えるので、今の所(2015/12/30時点)は消しておく
          // Debug.LogWarning("An instance of "+ typeof(T) +" is needed in the scene.");
        }
      }

      return instance;
    }
  }
}
