using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine; 

public class OblectsOnRoadController : MonoBehaviour
{
    public static OblectsOnRoadController Instance { get; private set; }


    [SerializeField] private ObjectOnRoad _prefabRoadMark;
    [SerializeField] private ObjectSlot[] _objectSlots;
    [SerializeField] private ObjectOnRoad[] _prefabs;

    [SerializeField] private ObjectOnRoad[] _prefabsSide;

    [SerializeField] private BoxCollider _creationArea;
    [SerializeField] private BoxCollider _creationAreaOnSideOfRoad;
    [SerializeField] private BoxCollider _creationAreaOnSideOfRoad2;

    [SerializeField] private Renderer _roadRenderer;
    [SerializeField] private float _roadRendererParamerter;
    [SerializeField] private Transform _roadMarkPoint;

    [SerializeField] private float _speed = 10;
      
    private float _cd;

    private float _cdRoadMark;

    private bool _enabled;
    private List<GameObject> _currentObjects;


    private void Start()
    { 
        Instance = this;
    }


    public void StartLevel()
    {
        _enabled = true;

        if (_currentObjects == null)
        {
            _currentObjects = new();
        }
    }

    public void StopLevel()
    {
        for (int i = 0; i < _currentObjects.Count; i++)
        {
            Destroy(_currentObjects[i]);
        }

        _currentObjects.Clear();

        _enabled = false;
    }   
     
    private void Update()
    {
        if(_enabled == false)
        {
            return;
        }

        _cdRoadMark -= Time.deltaTime; 
        _cd -= Time.deltaTime;


        if (_cdRoadMark <= 0)
        {
            CreateRoadMark();
            _cdRoadMark = 0.3f;
        }

        if (_cd <= 0)
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

            TryCreateBonuses();

                _cd = UnityEngine. Random.Range(0.3f, 1f);
        }

        

        _speed += Time.deltaTime / 25f; 
    }

    private void TryCreateBonuses()
    {
        for (int i = 0; i < _objectSlots.Length; i++)
        {
            if (UnityEngine.Random.Range(0, 100) <= _objectSlots[i].Possibility)
            {
                Vector3 pos = RandomPointInBounds(_creationArea.bounds);
            //    ObjectOnRoad newObject = Instantiate(_objectSlots[i].ObjectOnRoad, pos, Quaternion.identity);
                ObjectOnRoad newObject = ObjectsPool.Spawn<ObjectOnRoad>(_objectSlots[i].ObjectOnRoad, pos, Quaternion.identity);
                newObject.Setup(_speed);
                _currentObjects.Add(newObject.gameObject);
            }
        }
    }

    private void CreateRoadMark()
    {
        Vector3 pos = _roadMarkPoint.position;

        ObjectOnRoad newObject = ObjectsPool.Spawn<ObjectOnRoad>(_prefabRoadMark, pos, Quaternion.identity);
        newObject.Setup(_speed);
        _currentObjects.Add(newObject.gameObject);
    }

    private void CreateObjectOnRoad()
    {
        Vector3 pos = RandomPointInBounds(_creationArea.bounds);

        ObjectOnRoad newObject = ObjectsPool.Spawn<ObjectOnRoad>(_prefabs[UnityEngine.Random.Range(0, _prefabs.Length)], pos, Quaternion.identity);
        newObject.Setup(_speed);
        _currentObjects.Add(newObject.gameObject);
    } 

    private void CreateObjectOnSideOfRoad()
    {
        Vector3 pos = RandomPointInBounds(_creationAreaOnSideOfRoad.bounds);

        ObjectOnRoad newObject = ObjectsPool.Spawn<ObjectOnRoad>(_prefabsSide[UnityEngine.Random.Range(0, _prefabsSide.Length)], pos, Quaternion.identity);
        newObject.Setup(_speed);
        _currentObjects.Add(newObject.gameObject);
    }

    private void CreateObjectOnSideOfRoad2()
    {
        Vector3 pos = RandomPointInBounds(_creationAreaOnSideOfRoad2.bounds);

        ObjectOnRoad newObject = ObjectsPool.Spawn<ObjectOnRoad>(_prefabsSide[UnityEngine.Random.Range(0, _prefabsSide.Length)], pos, Quaternion.identity);
        newObject.Setup(_speed);
        _currentObjects.Add(newObject.gameObject);
    }

    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        Vector3 result = new Vector3(
          UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
          0,
          UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
      );


        while (CanCreate(result) == false)
        {
            result = new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            0,
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
        }


        return result;
    }

    private bool CanCreate(Vector3 pos)
    {
        if (_currentObjects == null)
        {
            _currentObjects = new();
            return true;
        }

        for (int i = 0; i < _currentObjects.Count; i++)
        {
            if (Vector3.Distance(pos, _currentObjects[i].transform.position) < 1f)
            {
                return false;
            }
        }

        return true;
    }

    public void RemoveObject(GameObject gameObjectToRemove)
    {
        ObjectsPool.Despawn(gameObjectToRemove);
        _currentObjects.Remove(gameObjectToRemove);
        //Destroy(gameObjectToRemove);
    }
}


[System.Serializable]
public class ObjectSlot
{
    public ObjectOnRoad ObjectOnRoad;
    public int Possibility; 
}
