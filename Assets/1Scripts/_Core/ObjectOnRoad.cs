using UnityEngine;

public class ObjectOnRoad : MonoBehaviour
{
    public Enums.ObjectType Type => _type;

    [SerializeField] private protected float _speed;
    [SerializeField] private protected Enums.ObjectType _type = Enums.ObjectType.Obstacle; 

     
    public virtual void Update()
    {
        transform.Translate(-Vector3.forward * _speed/3 * Time.deltaTime);

        if(transform.position.z < -5)
        {
            OblectsOnRoadController.Instance.RemoveObject(gameObject);
        }
    }

    public virtual void Setup(float speed)
    {
        gameObject.SetActive(true);
        _speed = speed;
    }

    public bool IsObstacle()
    {
        return _type == Enums.ObjectType.Obstacle;
    }

    public virtual void Hit()
    {
        if(_type == Enums.ObjectType.Heart)
        {
            LevelController.Instance.Lives++;
            OblectsOnRoadController.Instance.RemoveObject(gameObject);
        }
        else if (_type == Enums.ObjectType.Bonus)
        {
            LevelController.Instance.Score += 5;
            OblectsOnRoadController.Instance.RemoveObject(gameObject);
        }
    }
}
