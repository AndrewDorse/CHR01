using System.Collections;
using UnityEngine;

public class ObjectOnRoadTrash : ObjectOnRoad
{ 
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private BoxCollider _boxCollider2;

    private bool _afterHit = false;

      

    private void Start()
    { 
        _rigidbody.isKinematic = true;
    }

    

    public override void Hit()
    {
        if (_rigidbody != null && _type == Enums.ObjectType.Trash && _afterHit == false)
        {
            _rigidbody.isKinematic = false;

            if (ChickenController.instance.transform.position.x < transform.position.x)
            {
                _rigidbody.AddForce(Vector3.forward * 2 + Vector3.up * 5 + Vector3.right * 4, ForceMode.Impulse);
                _rigidbody.AddTorque(Vector3.forward * 2 + Vector3.up * 5 + Vector3.right * 4, ForceMode.Impulse);
            }
            else
            {
                _rigidbody.AddForce(Vector3.forward * 2 + Vector3.up * 5 + Vector3.right * -4, ForceMode.Impulse);
                _rigidbody.AddTorque(Vector3.forward * 2 + Vector3.up * 5 + Vector3.right * -4, ForceMode.Impulse);
            }

            _afterHit = true;

            _boxCollider.enabled = false;
            _boxCollider2.enabled = false;

            StartCoroutine(DeathCoroutine()); 
        }
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(3);

        OblectsOnRoadController.Instance.RemoveObject(gameObject);
    }
}
