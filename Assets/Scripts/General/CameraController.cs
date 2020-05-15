using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 targetPosition;
    private const float CAMERA_SPEED = 2f;
    [SerializeField] private Vector3 distanceToTarget = new Vector3(0,0, -7f);


    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, CAMERA_SPEED * Time.deltaTime);
    }
    public void ChangeTarget(Vector3 newTarget)
    {
        targetPosition = newTarget + distanceToTarget;
    }

    public void GameEnd(Vector3 midlePosition, int count)
    {
        targetPosition = midlePosition + (distanceToTarget + Vector3.back * count);
    }
}
