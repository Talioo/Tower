using System.Collections;
using UnityEngine;

public abstract class GameUIParent : MonoBehaviour
{
    [SerializeField, HideInInspector] protected CanvasGroup CanvasGroup;

    protected readonly float targetAlpha = 0.8f;
    private const float SPEED = 1f;
    private void OnValidate()
    {
        if (CanvasGroup == null)
            CanvasGroup = GetComponent<CanvasGroup>();
    }

    public abstract void TapAction();

    public void Close()
    {
        StartCoroutine(HideCorutine());
    }
    public void Show()
    {
        StartCoroutine(ShowCorutine());
        UpdateText();
    }
    IEnumerator ShowCorutine()
    {
        while (CanvasGroup.alpha < targetAlpha)
        {
            CanvasGroup.alpha += SPEED * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator HideCorutine()
    {
        while (CanvasGroup.alpha > 0)
        {
            CanvasGroup.alpha -= SPEED * Time.deltaTime;
            yield return null;
        }
    }

    public virtual void UpdateText()
    {

    }
}
