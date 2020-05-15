using System;
using UnityEngine;
using Zenject;

public class BlockMoving : BlockState
{
    readonly Settings settings;
    private Vector3 direction = Vector3.right;
    private Vector3 startPosition;
    private float deltaX => block.transform.position.x - startPosition.x;
    private void ChangeDirection() => direction = deltaX > 0 ? Vector3.left : Vector3.right;


    public BlockMoving(Settings settings)
    {
        this.settings = settings;
    }

    public override void Start()
    {
        startPosition = block.transform.position;
    }

    public override void Move()
    {
        block.Move(direction * (settings.moveSpeed * Time.deltaTime));
        if(Mathf.Abs(deltaX) > settings.moveDelta)
            ChangeDirection();
    }

    public override void TapAction()
    {
        block.ChangeState(MovableStates.AfterRound);
    }

    [Serializable]
    public class Settings
    {
        [Range(0.1f, 15f)] public float moveSpeed;
        [Range(1f, 5f)] public float moveDelta;
    }
    public class Factory : PlaceholderFactory<BlockMoving>
    {
    }
}
