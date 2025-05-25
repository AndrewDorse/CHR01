using UnityEngine;

public class ObjectOnRoad : MonoBehaviour
{

    [SerializeField] private protected float _speed;
    [SerializeField] private protected Enums.ObjectType _type = Enums.ObjectType.Obstacle; 

     
    public virtual void FixedUpdate()
    {
        transform.Translate(-Vector3.forward * _speed/100);
    }

    public virtual void Setup(float speed)
    {
        _speed = speed;
    }

    public bool IsObstacle()
    {
        return _type == Enums.ObjectType.Obstacle;
    }

    public virtual void Hit()
    {
        
    }
}
