using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIAfterGame : GameUIParent
{
    private readonly string format = "Your score: {0}\n Best:{1}\n\n Tap to restart";
    [SerializeField] private Text myText;
    public override void TapAction()
    {
        if(CanvasGroup.alpha >= targetAlpha)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void UpdateText()
    {
        myText.text = String.Format(format, CanvasManager.CurrentScore, CanvasManager.BestScore);
    }
}
