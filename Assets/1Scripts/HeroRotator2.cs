using UnityEngine;
using UnityEngine.EventSystems;

public class HeroRotator2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _sensitivity;

    [SerializeField] private GameObject _hero;

    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private bool _isRotating;



    private void Update()
    {
        if (_isRotating && _hero != null)
        {
            _mouseOffset = (Input.mousePosition - _mouseReference);
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            _hero.transform.Rotate(_rotation);
            _mouseReference = Input.mousePosition;
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