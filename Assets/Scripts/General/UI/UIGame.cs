using System;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : GameUIParent
{
    private readonly string format = "Score: {0} Best: {1}";
    [SerializeField] private Text myText;
    public override void TapAction()
    {

    }

    public override void UpdateText()
    {
        myText.text = String.Format(format, CanvasManager.CurrentScore, CanvasManager.BestScore);
    }
}
