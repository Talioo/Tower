using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BlockState : IDisposable
{
    private readonly Vector3[] startDirections = new[] {Vector3.right, Vector3.left, Vector3.up};
    [Inject]  private readonly CameraController mainCamera;
    [Inject] protected MovingBlock block;
    private const float DISTANCE_TO_CAMERA = 3f;

    public virtual void Start()
    {

    }
    public virtual void Move()
    {

    }

    protected void BlockBehindCamera()
    {
        var direction = startDirections[Random.Range(0, startDirections.Length - 1)];
        block.SetPosition(mainCamera.transform.position + (direction * DISTANCE_TO_CAMERA));
    }
    public virtual void TapAction()
    {

    }
    public override string ToString()
    {
        return GetType().ToString();
    }


    public void Dispose()
    {

    }
}
