using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Vector3 originalScale, upScale;
    private void Awake()
    {
        originalScale = transform.localScale;
        upScale = originalScale * 1.2f;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, upScale, 0.1f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, originalScale, 0.1f).setDelay(0.1f);
    }
}
