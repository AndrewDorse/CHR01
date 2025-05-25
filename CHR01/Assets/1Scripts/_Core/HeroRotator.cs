using UnityEngine;
using UnityEngine.EventSystems;

public class HeroRotator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    

    [SerializeField]  private float _sensitivity;

    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private bool _isRotating;

    [SerializeField]  private GameObject _hero;

    [SerializeField] private float _limit;

    private void Update()
    {
        if (_isRotating && _hero != null)
        {
            _mouseOffset = (Input.mousePosition - _mouseReference);
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            _hero.transform.position = (_hero.transform.position  + Vector3.right * ((_mouseOffset.x ) * _sensitivity/100));
            _mouseReference = Input.mousePosition;


            _hero. transform.position = new Vector3(Mathf.Clamp(_hero.transform.position.x, -_limit, _limit), 0, 0); 
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isRotating = true;
        _mouseReference = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isRotating = false;
    }
}