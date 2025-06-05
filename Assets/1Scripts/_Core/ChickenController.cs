using UnityEngine;

public class ChickenController : MonoBehaviour
{
    public static ChickenController instance;

    [SerializeField] private GameObject _effectHit;
    [SerializeField] private GameObject _effectGold;
    [SerializeField] private GameObject _effectBonus;
    [SerializeField] private GameObject _effectTrash;
    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectOnRoad obj = other.GetComponent<ObjectOnRoad>();

        if (obj != null)
        {
            if (obj.IsObstacle())
            {
                TakeDamage();
            }
            else
            {
                obj.Hit();
                if (obj.Type == Enums.ObjectType.Bonus)
                {
                    GameObject effect = ObjectsPool.Spawn<GameObject>(_effectGold, transform.position, transform.rotation);
                }
                else if (obj.Type == Enums.ObjectType.Heart)
                {
                    GameObject effect = ObjectsPool.Spawn<GameObject>(_effectBonus, transform.position + Vector3.up, transform.rotation);
                }
                else if (obj.Type == Enums.ObjectType.Trash)
                {
                    GameObject effect = ObjectsPool.Spawn<GameObject>(_effectTrash, transform.position  , transform.rotation);
                }
            }

        }
    }

    private void TakeDamage()
    {
        Debug.Log("### GOT DA<AGE");

        GameObject effect = ObjectsPool.Spawn<GameObject>(_effectHit, transform.position + Vector3.up * 1f, transform.rotation);

        LevelController.Instance.Lives--;
        LevelController.Instance.GotDamage();
    }

}
