using UnityEngine;

public interface IHittable
{
    public void OnHit(float ratio, Vector3 directionHit);
}
