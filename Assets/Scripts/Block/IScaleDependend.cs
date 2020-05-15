using UnityEngine;

public interface IScaleDependend
{
    Vector3 CalculatePosition();
    void UpdateScale(float value = -1);
}
