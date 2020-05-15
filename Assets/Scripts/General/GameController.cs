using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : ITickable, IInitializable
{
    [Inject] private readonly StayingBlock.Factory _blockFactory;
    [Inject] private readonly CameraController cameraController;
    [Inject] private readonly CanvasManager canvasManager;

     private readonly MovingBlock movingBlock;
     private BlockStateFactory BlockStateFactory;
     private RoundParamsContainer roundParams;

    private List<StayingBlock> stayingBlocks = new List<StayingBlock>();

    public GameController(StayingBlock.Factory blockFactory, MovingBlock block,
        BlockStateFactory blockStateFactory,
        RoundParamsContainer roundParams)
    {
        movingBlock = block;
        BlockStateFactory = blockStateFactory;
        this.roundParams = roundParams;
        movingBlock.Init(BlockStateFactory, roundParams);
    }
    public void Initialize()
    {
        AddBlock(Vector3.zero);

        movingBlock.Loose += Loose;
        movingBlock.Win += Win;
    }

    public void Tick()
    {
        if (Input.anyKeyDown)
            movingBlock.TapAction();
    }
    private void Win(Vector3 position)
    {
        canvasManager.AddPoint();
        AddBlock(position);
    }

    private void Loose()
    {
        Debug.Log("Looser!");
        movingBlock.gameObject.SetActive(false);
        int midle = stayingBlocks.Count % 2f > 0 ? stayingBlocks.Count / 2 : stayingBlocks.Count / 2 - 1;
        cameraController.GameEnd(stayingBlocks[midle].transform.position, stayingBlocks.Count);
        canvasManager.Loose();
    }
    private void AddBlock(Vector3 position)
    {
        var newBlock = _blockFactory.Create();
        roundParams.UpdateCurrentBlock(newBlock);

        newBlock.RoundParams(Color.cyan);
        cameraController.ChangeTarget(roundParams.CurrentStayingBlockPosition);
        stayingBlocks.Add(newBlock);
        movingBlock.UpdateTargetPos(roundParams.CurrentStayingBlockPosition);
    }

}
