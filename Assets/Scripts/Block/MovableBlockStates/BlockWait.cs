using Zenject;

public class BlockWait : BlockState
{
    public override void Start()
    {
        BlockBehindCamera();
    }

    public override void TapAction()
    {
        block.ChangeState(MovableStates.BeforeRound);
    }
    public class Factory : PlaceholderFactory<BlockWait>
    {
    }
}
