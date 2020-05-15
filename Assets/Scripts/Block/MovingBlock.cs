using System;
using UnityEngine;

public class MovingBlock: MonoBehaviour, IScaleDependend
{
    [SerializeField, HideInInspector] private Rigidbody rigidbody;
    private BlockState currentState;

    public Vector3 targetPos { get; private set; }
    public Action Loose;
    public Action<Vector3> Win;

    private BlockStateFactory BlockStateFactory;
    private RoundParamsContainer RoundParamsContainer;
    public  void Init(BlockStateFactory blockStateFactory, RoundParamsContainer roundParams)
    {
        BlockStateFactory = blockStateFactory;
        RoundParamsContainer = roundParams;
        ChangeState(MovableStates.Waiting);
    }
    private void OnValidate()
    {
        if (rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();
    }
    public void UpdateTargetPos(Vector3 newPos)
    {
        targetPos = newPos + Vector3.up * (transform.localScale.y);
    }

    public void StartMoving(Color color, float scale, float lastY)
    {
       transform.position = Vector3.up * (lastY + transform.localScale.y);
    }

    private void FixedUpdate()
    {
        currentState.Move();
    }

    public void Move(Vector3 moveVector)
    {
        transform.position += moveVector;
    }
    public void MoveToTarget(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }
    public void TapAction() => currentState.TapAction();

    public void PushBlock()
    {
        rigidbody.useGravity = true;
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.AddForce(RoundParamsContainer.lastDelta > 0 ? Vector3.right : Vector3.left, ForceMode.Impulse);
    }

    public void ResetBlockValues()
    {
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void CheckPosition()
    {
        RoundParamsContainer.lastDelta = transform.position.x - targetPos.x;
        RoundParamsContainer.currentBlockScale = transform.localScale.x - Math.Abs(RoundParamsContainer.lastDelta);
        if (Math.Abs(RoundParamsContainer.lastDelta) > transform.localScale.x)
        {
            Loose?.Invoke();
            return;
        }

        var newBlockPos = CalculatePosition();

        Win.Invoke(newBlockPos);

        SetPosition(CalculatePosition());

        UpdateScale(Math.Abs(RoundParamsContainer.lastDelta));
    }

    public Vector3 CalculatePosition()
    {
        Vector3 newPos = RoundParamsContainer.PreviousBlockPosition + Vector3.up * transform.localScale.y;
        if (RoundParamsContainer.lastDelta > 0)
        {
            newPos.x += (RoundParamsContainer.PreviousBlockScaleX / 2) + (RoundParamsContainer.lastDelta / 2);
            return newPos;
        }
        newPos.x -= (RoundParamsContainer.PreviousBlockScaleX / 2) - (RoundParamsContainer.lastDelta / 2);
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
    public void ChangeState(MovableStates state)
    {
        if (currentState != null)
        {
            currentState.Dispose();
            currentState = null;
        }
        currentState = BlockStateFactory.CreateState(state);
        currentState.Start();
    }
}
