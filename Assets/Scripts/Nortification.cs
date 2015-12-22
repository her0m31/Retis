using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

[System.Serializable]
public class Nortification<T> : UnityEvent<T> {
	private T data;

  public Nortification(T t) {
    Value = t;
  }

  public T Value {
    get {
      return data;
    }
    set {
      data = value;
      Invoke(data);
    }
  }

  public void DisposeOf() {
    RemoveAllListeners();
  }
}
