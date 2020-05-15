using Zenject;

public class UIBeforeGame : GameUIParent
{
    [Inject] private CanvasManager canvasManager;
    public override void TapAction()
    {
        Close();
        canvasManager.ChangeUI(CanvasManager.UIState.InGame);
    }
}
