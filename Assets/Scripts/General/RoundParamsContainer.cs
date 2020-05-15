using UnityEngine;
public class RoundParamsContainer
{
    public float currentBlockScale;
    public float lastDelta = 0;
    private StayingBlock previousStayingBlock;
    private StayingBlock currentStayingBlock;
    public Vector3 PreviousBlockPosition =>previousStayingBlock == null ? Vector3.zero : previousStayingBlock.transform.position;
    public float PreviousBlockScaleX => previousStayingBlock == null ? 0 : previousStayingBlock.transform.localScale.x;
    public Vector3 CurrentStayingBlockPosition => currentStayingBlock.transform.position;

    public RoundParamsContainer(MovingBlock movingBlock)
    {
        currentBlockScale = movingBlock.transform.localScale.x;
    }

    public void UpdateCurrentBlock(StayingBlock newOne)
    {
        previousStayingBlock = currentStayingBlock;
        currentStayingBlock = newOne;
    }
}
