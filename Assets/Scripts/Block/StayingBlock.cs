using UnityEngine;
using Zenject;

public class StayingBlock : MonoBehaviour, IScaleDependend
{
    [Inject] private BlockStateFactory BlockStateFactory;
    [Inject] private RoundParamsContainer RoundParamsContainer;

    [Inject]
    public void Init(BlockStateFactory blockStateFactory, RoundParamsContainer roundParams)
    {
        BlockStateFactory = blockStateFactory;
        RoundParamsContainer = roundParams;
    }

    public void RoundParams(Color color)
    {
        UpdateScale();
        transform.position = CalculatePosition();
    }

    public Vector3 CalculatePosition()
    {
        var newScale = RoundParamsContainer.currentBlockScale;
        Vector3 newPos = RoundParamsContainer.PreviousBlockPosition + Vector3.up * transform.localScale.y;
        if (RoundParamsContainer.lastDelta > 0)
        {
            newPos.x += (RoundParamsContainer.PreviousBlockScaleX / 2) - (newScale / 2);
            return newPos;
        }
        newPos.x -= (RoundParamsContainer.PreviousBlockScaleX / 2) - (newScale / 2);
        return newPos;
    }
    public void UpdateScale(float value = -1)
    {
        if (value <= 0)
            value = RoundParamsContainer.currentBlockScale;
        var temp = transform.localScale;
        temp.x = value;
        transform.localScale = temp;
    }
    public class Factory : PlaceholderFactory< StayingBlock>
    {
    }
}
