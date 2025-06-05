using UnityEngine;

public class ChickenController : MonoBehaviour
{
    public static ChickenController instance;

    [SerializeField] private GameObject _effectHit;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectOnRoad obj = other.GetComponent<ObjectOnRoad>();

        if (obj != null)
        {
            if(obj.IsObstacle())
            {
                TakeDamage();
            }
            else
            {
                obj.Hit();
            }

        }
    }

    private void TakeDamage()
    {
        Debug.Log("### GOT DA<AGE");

        GameObject effect = ObjectsPool.Spawn<GameObject>(_effectHit, transform.position, transform.rotation);

        LevelController.Instance.Lives--;
        LevelController.Instance.GotDamage();
    }

}
