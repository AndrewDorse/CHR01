using Hellmade.Sound;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    public float Amount => _amount;


    [SerializeField] private float _amount;
    [SerializeField] private TMPro.TMP_Text _text;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private PoolDespawner _effect;
    [SerializeField] private PoolDespawner _miultiplyEffect;

    private bool _ignoreTouches = false;
    private bool _ignoreTouchesInitial = true;

    private float _stackTime;

    private int _lastMultilpyId = -1;

    public void Setup(float amount)
    {
        _amount = amount;
        _rigidbody.AddForce(Vector3.down * 3f, ForceMode.Impulse);

        UpdateViewNoSize();
    }

    public void MultiplyAmount(float value, int id)
    {
        if(id == _lastMultilpyId)
        {
            return;
        }
         
        if (_ignoreTouches)
        {
            return;
        }

        _lastMultilpyId = id;

        _ignoreTouchesInitial = false;

        _amount *= value;

        if(_amount < 1)
        {
            _amount = 1;
        }


        _text.text = _amount.ToString();
        StartCoroutine(Coroutine());

        CreateEffectMultiply();
        EventsProvider.OnBallMerged?.Invoke(_amount);
        EazySoundManager.PlaySound(AudioClipGameStorage.RandomImprove, volume: 0.6f);
    }

    public void Upgrade()
    {
        if (_ignoreTouches)
        {
            return;
        }


        _amount *= 2;

        EventsProvider.OnBallMerged?.Invoke(_amount);

        _text.text = _amount.ToString();
        _rigidbody.AddForce(Vector3.up * 200f, ForceMode.Impulse);

        CreateEffect();

        SaveManager.SaveModel.Gold += Mathf.RoundToInt(_amount / 4);

        EazySoundManager.PlaySound(AudioClipGameStorage.RandomImprove, volume: 0.6f);

        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Coroutine());
        }
    }

    public void Remove()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.StartsWith("Cylinder"))
        {
            _rigidbody.AddForce(Vector3.right * Random.Range(-25, 25), ForceMode.Impulse);
        }

       

        if (_ignoreTouches || _ignoreTouchesInitial)
        {
            return;
        }

        Ball ball = other.GetComponent<Ball>();

        if(ball)
        {
            if (BallsController.TryMerge(this, ball))
            {
                if (gameObject.activeInHierarchy)
                { 
                    StartCoroutine(Coroutine());
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_ignoreTouchesInitial)
        {
            if (other.gameObject.name.StartsWith("Cylinder"))
            {
                _stackTime += Time.deltaTime;

                if (_stackTime >= 0.33f)
                {
                    _stackTime = 0;
                    _rigidbody.AddForce(Vector3.right * Random.Range(-25, 25), ForceMode.Impulse);
                }
            }
        }
    }

    private IEnumerator Coroutine()
    {
        UpdateView();
        _ignoreTouches = true;
        yield return new WaitForSeconds(0.07f);
        _ignoreTouches = false;
    }

    private void UpdateView()
    {
        _text.text = _amount.ToString();
        BallData data = Master.Instance.GetBallData(_amount);
        _renderer.material = data.Material;
        transform.localScale = data.Size;
    }

    private void UpdateViewNoSize()
    {
        _text.text = _amount.ToString();
        BallData data = Master.Instance.GetBallData(_amount);
        _renderer.material = data.Material;
    }

    private void CreateEffect()
    {
        PoolDespawner go = ObjectsPool.Spawn(_effect, transform.position, Quaternion.identity);
    }

    private void CreateEffectMultiply()
    {
        PoolDespawner go = ObjectsPool.Spawn(_miultiplyEffect, transform.position, Quaternion.identity);
    }

}
