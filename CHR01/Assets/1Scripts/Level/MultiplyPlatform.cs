using UnityEngine;

public class MultiplyPlatform : MonoBehaviour
{
    [SerializeField] private float _multiplyValue;



    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();

        if(ball)
        {
            ball.MultiplyAmount(_multiplyValue, gameObject.GetInstanceID());

        }
    }
}
