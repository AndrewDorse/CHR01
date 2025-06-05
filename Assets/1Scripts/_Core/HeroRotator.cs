using UnityEngine;
using UnityEngine.EventSystems;

public class HeroRotator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _limit;



    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private bool _isRotating;

    private GameObject _character;



    private void Start()
    {
        _character = LevelController.ChickenController.gameObject; 
    }

    private void Update()
    {
        if (_isRotating && _character != null)
        {
            _mouseOffset = (Input.mousePosition - _mouseReference);
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            _character.transform.position = (_character.transform.position  + Vector3.right * ((_mouseOffset.x ) * _sensitivity/100));
            _mouseReference = Input.mousePosition;


            _character. transform.position = new Vector3(Mathf.Clamp(_character.transform.position.x, -_limit, _limit), 0, 0); 
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