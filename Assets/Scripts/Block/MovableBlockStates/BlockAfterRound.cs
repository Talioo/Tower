using Zenject;

public class BlockAfterRound : BlockState
{
    private const float RESTART_DELTA = 1.5f;
    public override void Start()
    {
        block.CheckPosition();
        block.PushBlock();
    }

    public override void Move()
    {
        if((block.targetPos.y - block.transform.position.y) > block.transform.position.y * RESTART_DELTA)
            block.ChangeState(MovableStates.BeforeRound);
    }

    public class Factory : PlaceholderFactory<BlockAfterRound>
    {
    }
}
