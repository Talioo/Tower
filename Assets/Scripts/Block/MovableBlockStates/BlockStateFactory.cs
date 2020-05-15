using ModestTree;

public enum MovableStates
{
    Waiting,
    Moving,
    BeforeRound,
    AfterRound
}


public class BlockStateFactory
{
    readonly BlockWait.Factory _waitingFactory;
    readonly BlockMoving.Factory _movingFactory;
    readonly BlockBeforeRound.Factory _beforeRoundFactory;
    readonly BlockAfterRound.Factory _afterRoundFactory;

    public BlockStateFactory(
        BlockWait.Factory waitingFactory,
        BlockMoving.Factory movingFactory,
        BlockBeforeRound.Factory beforeRoundFactory,
        BlockAfterRound.Factory  afterRoundFactory)
    {
        _waitingFactory = waitingFactory;
        _movingFactory = movingFactory;
        _beforeRoundFactory = beforeRoundFactory;
        _afterRoundFactory = afterRoundFactory;
    }

    public BlockState CreateState(MovableStates state)
    {
        switch (state)
        {
            case MovableStates.Waiting:
            {
                return _waitingFactory.Create();
            }
            case MovableStates.Moving:
            {
                return _movingFactory.Create();
            }
            case MovableStates.BeforeRound:
            {
                return _beforeRoundFactory.Create();
            }
            case MovableStates.AfterRound:
            {
                return _afterRoundFactory.Create();
            }
        }

        throw Assert.CreateException();
    }
}
