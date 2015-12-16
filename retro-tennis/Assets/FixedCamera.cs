using UnityEngine;
using System.Collections;

public class FixedCamera : MonoBehaviour {
	private Camera fixedCamera;
	// 画面サイズ
	private float width;
	private float height;
	// 画像のpixle per unit(通常100.0f)
	private float pixelPerUnit;
	// orthographicSizeはカメラの縦幅の半分
	private float oneHalf;
	// 縦幅の倍率
	private float backgroundScale;
	// Viewport Rect の座標
	private float rectX;
	private float rectY;
	// カメラのViewport Rectの幅
	private float viewportRectWidth;
	private float viewportRectHeight;
	// アスペクト比
	private float aspect;
	private float backgroundAspect;

	// Instead of constructor
	void Awake() {
		width   = 640.0f;
		height  = 960.0f;
		pixelPerUnit = 100.0f;
		oneHalf = 2.0f;

		aspect  = (float)Screen.height / (float)Screen.width;
		backgroundAspect = height / width;

		fixedCamera = GetComponent<Camera>();
		// カメラのサイズを縦に合わせる
		fixedCamera.orthographicSize = height / oneHalf / pixelPerUnit;

		// 画像の縦幅の倍率
		backgroundScale   = height / (float)Screen.height;
		viewportRectWidth = width / ((float)Screen.width * backgroundScale);
		viewportRectHeight = 1.0f;
		// viewport Rectの座標の設定
		rectX = (1.0f - viewportRectWidth) / 2.0f;
		rectY = 0.0f;

		if(backgroundAspect < aspect) {
			// 画像の縦幅の倍率
			backgroundScale   = width / (float)Screen.width;
			viewportRectWidth = 1.0f;
			viewportRectHeight = height / ((float)Screen.height * backgroundScale);
			// viewport Rectの座標の設定
			rectX = 0.0f;
			rectY = (1.0f - viewportRectHeight) / 2.0f;
		}

		// viewport rectの設定
		fixedCamera.rect = new Rect(rectX, rectY, viewportRectWidth, viewportRectHeight);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
