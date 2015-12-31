using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PagingScrollViewController : ViewController, IBeginDragHandler, IEndDragHandler {
  private ScrollRect thisScrollRect;
  private bool isAnimating = false;
  private Vector2 destPosition;
  private Vector2 initialPosition;
  private AnimationCurve animationCurve;
  private int prevPageIndex = 0;
  private Rect currentViewRect;

  public ScrollRect scrollRect {
    get {
      return thisScrollRect == null ? GetComponent<ScrollRect>() : thisScrollRect;
    }
  }

  public void OnBeginDrag(PointerEventData eventData) {
    isAnimating = true;
  }

  public void OnEndDrag(PointerEventData eventData) {
    GridLayoutGroup grid = scrollRect.content.GetComponent<GridLayoutGroup>();

    scrollRect.StopMovement();

    float pageWidth = (grid.cellSize.x + grid.spacing.x);

    int pageIndex = Mathf.RoundToInt((scrollRect.content.anchoredPosition.x) / pageWidth);

    if(pageIndex == prevPageIndex && 4 <= Mathf.Abs(eventData.delta.x)) {
      scrollRect.content.anchoredPosition += new Vector2(eventData.delta.x, 0.0f);
      pageIndex += (int)Mathf.Sign(-eventData.delta.x);
    }

    if(0 < pageIndex) {
      pageIndex = 0;
    }
    else if(grid.transform.childCount - 1 < pageIndex) {
      pageIndex = grid.transform.childCount - 1;
    }

    prevPageIndex = pageIndex;

    float destX = pageIndex * pageWidth;
    destPosition = new Vector2(destX, scrollRect.content.anchoredPosition.y);

    initialPosition = scrollRect.content.anchoredPosition;

    Keyframe keyFrame1 = new Keyframe(Time.time, 0.0f, 0.0f, 1.0f);
    Keyframe keyFrame2 = new Keyframe(Time.time + 0.3f, 1.0f, 0.0f, 0.0f);
    animationCurve = new AnimationCurve(keyFrame1, keyFrame2);

    isAnimating = true;
  }

  void LateUpdate() {
    if(isAnimating) {
      if(animationCurve.keys[animationCurve.length-1].time <= Time.time) {
          scrollRect.content.anchoredPosition = destPosition;
          isAnimating = false;
          return;
      }

      Vector2 newPosition = initialPosition + (destPosition - initialPosition) * animationCurve.Evaluate(Time.time);
      scrollRect.content.anchoredPosition = newPosition;
    }
  }

  private void UpdateView() {
    currentViewRect = rectTransform.rect;

    GridLayoutGroup grid = scrollRect.content.GetComponent<GridLayoutGroup>();

    int paddingH = Mathf.RoundToInt((currentViewRect.width - grid.cellSize.x) / 2.0f);
    int paddingV = Mathf.RoundToInt((currentViewRect.height - grid.cellSize.y) / 2.0f);

    grid.padding = new RectOffset(paddingH, paddingH, paddingV, paddingV);
  }

  void Awake() {
    thisScrollRect = GetComponent<ScrollRect>();

    Keyframe keyFrame1 = new Keyframe(Time.time, 0.0f, 0.0f, 1.0f);
    Keyframe keyFrame2 = new Keyframe(Time.time + 0.3f, 1.0f, 0.0f, 0.0f);
    animationCurve = new AnimationCurve(keyFrame1, keyFrame2);
  }
}
