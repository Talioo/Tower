using UnityEngine;
using Zenject;

public class BlockBeforeRound : BlockState
{
    private readonly BlockMoving.Settings settings;
    [Inject] private RoundParamsContainer roundParams;

    public BlockBeforeRound(BlockMoving.Settings settings)
    {
        this.settings = settings;
    }

    public override void Start()
    {
        block.ResetBlockValues();
        BlockBehindCamera();
        block.UpdateScale();
        block.transform.rotation = Quaternion.identity;

    }

    public override void Move()
    {
        block.MoveToTarget(settings.moveSpeed * 2);
        if(block.transform.position == block.targetPos)
            block.ChangeState(MovableStates.Moving);
    }

    public class Factory : PlaceholderFactory<BlockBeforeRound>
    {
    }
}
