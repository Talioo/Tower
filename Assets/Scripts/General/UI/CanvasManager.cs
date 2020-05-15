using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CanvasManager : MonoBehaviour, IPointerDownHandler
{
    public enum UIState { BeforeGame, InGame, AfterGame }

    public static int CurrentScore { get; private set; }
    public static int BestScore { get; private set; }

    [Inject] private UIBeforeGame beforeGame;
    [Inject] private UIGame inGame;
    [Inject] private UIAfterGame afterGame;
    private GameUIParent currentUI;
    private const string SAVE_KEY = "bestScore";

    private void Start()
    {
        currentUI = beforeGame;
        CurrentScore = 0;
        BestScore = PlayerPrefs.HasKey(SAVE_KEY) ? PlayerPrefs.GetInt(SAVE_KEY) : 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentUI.TapAction();
    }

    public void AddPoint()
    {
        CurrentScore++;
        currentUI.UpdateText();
    }

    public void Loose()
    {
        SaveScore();
        ChangeUI(UIState.AfterGame);
    }

    public void ChangeUI(UIState state)
    {
        switch (state)
        {
            case UIState.BeforeGame:
                ChangeUI(beforeGame);
                break;
            case UIState.InGame:
                ChangeUI(inGame);
                break;
            case UIState.AfterGame:
                ChangeUI(afterGame);
                break;
        }
    }
    private void ChangeUI(GameUIParent newUI)
    {
        currentUI.Close();
        currentUI = newUI;
        currentUI.Show();
    }

    private void SaveScore()
    {
        if(CurrentScore > BestScore)
            PlayerPrefs.SetInt(SAVE_KEY, CurrentScore);
    }
    private void OnDestroy()
    {
        SaveScore();
    }
}
