using System;
using UnityEngine; 

public class OblectsOnRoadController : MonoBehaviour
{
    [SerializeField] private ObjectOnRoad[] _prefabs;

    [SerializeField] private ObjectOnRoad[] _prefabsSide;

    [SerializeField] private BoxCollider _creationArea;
    [SerializeField] private BoxCollider _creationAreaOnSideOfRoad;
    [SerializeField] private BoxCollider _creationAreaOnSideOfRoad2;

    [SerializeField] private Renderer _roadRenderer;
    [SerializeField] private float _roadRendererParamerter;

    private Material _materialRoad;

    private float _cd;

    [SerializeField] private float _speed = 10;



    private void Start()
    {
        _materialRoad = _roadRenderer.sharedMaterial; 
    }

    private void Update()
    {
        if(_cd <= 0)
        {
            CreateObjectOnRoad();

            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                CreateObjectOnSideOfRoad();
            }
            else
            {
                CreateObjectOnSideOfRoad2();
            }
                
                _cd = UnityEngine. Random.Range(0.3f, 1f);
        }

        _cd -= Time.deltaTime;

        //_speed += Time.deltaTime / 5f;

        _materialRoad.SetFloat("_scrollSpeed",- _speed / _roadRendererParamerter);


        // 10 1.08
        // 12 1.067
        // 14 1.067
        // 20 1.63
    }

    private void CreateObjectOnRoad()
    {
        Vector3 pos = RandomPointInBounds(_creationArea.bounds);

        ObjectOnRoad newObject = Instantiate(_prefabs[UnityEngine.Random.Range(0, _prefabs.Length)], pos, Quaternion.identity);
        newObject.Setup(_speed);
    } 

    private void CreateObjectOnSideOfRoad()
    {
        Vector3 pos = RandomPointInBounds(_creationAreaOnSideOfRoad.bounds);

        ObjectOnRoad newObject = Instantiate(_prefabsSide[UnityEngine.Random.Range(0, _prefabsSide.Length)], pos, Quaternion.identity);
        newObject.Setup(_speed);
    }

    private void CreateObjectOnSideOfRoad2()
    {
        Vector3 pos = RandomPointInBounds(_creationAreaOnSideOfRoad2.bounds);

        ObjectOnRoad newObject = Instantiate(_prefabsSide[UnityEngine.Random.Range(0, _prefabsSide.Length)], pos, Quaternion.identity);
        newObject.Setup(_speed);
    }

    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            0,
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
