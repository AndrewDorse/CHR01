using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UniWebViewLogger;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;



    public static ChickenController ChickenController => Instance._character;

    public PlayableCharacter[] PlayableCharacters;

    private GameObject _curModel;

    public float Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;

            EventsProvider.OnScoreChanged?.Invoke(_score);
        }
    }
    public int Value => _value;
    public int Amount => _amount;
    public int ValueToWin => _valueToWin;

    public int Lives 
    { 
        get
        {
            return _lives;
        }

        internal set
        {
            _lives = value;

            if(_lives > 3)
            {
                _lives = 3;
            }

            EventsProvider.OnLivesChanged?.Invoke(_lives);
        } 
    }

    [SerializeField] private ChickenController _character;

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _createPosition;
    [SerializeField] private OblectsOnRoadController _oblectsOnRoadController;

    private float _needToWin;

    private int _value = 2;
    private int _amount = 1;
    private GameObject _level;
    private Level _levelSettings;

    private List<Ball> _balls;

    private bool _victory;
    private float _score;
    private int _valueToWin;
    private int _lives;

    private void Awake()
    { 
        if(LevelController.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _balls = new();
         
    }  

    public void GotDamage()
    {
        if (_victory == false)
        {
            if (_lives <= 0)
            {
                GameOver();
            }
        }
    }


    private void GameOver()
    { 
        if (_victory == false)
        {
            _victory = true;

            //if( _levelSettings.level + 1 > SaveManager.SaveModel.Level)
            //{
            //    SaveManager.SaveModel.Level++;  
            //    SaveManager.Save();
            //}

            WinPopup popup = Master.UIController.GetPopup<WinPopup>();

            SaveManager.SendResult(1, _score) ;


            popup.Setup(null,
                () =>
                {
                    Master.GameStageController.ChangeStage(Enums.GameStage.Menu);
                    ResetLevel();
                },
                () =>
                {
                    ResetLevel(); 
                    StartLevel();
                },
                _score,
                SaveManager.GetResult(1, _score)
                );

            ResetLevel();
        }
    }

    public void StartLevel()
    {
        _lives = 3;

        Time.timeScale = 1;

        _oblectsOnRoadController.StartLevel();


        if(_curModel != null)
        {
            Destroy(_curModel);
        }

        _curModel = Instantiate(PlayableCharacters[SaveManager.SaveModel.CharacterNumber - 1].prefabGame, ChickenController.transform);

        return;

        _value = 2;
        _amount = 1;
        _victory = false;
        _score = 1000;

        if (_level != null)
        {
            Destroy( _level);
        }

        if (_balls != null)
        {
            foreach (var ball in _balls)
            {
                Destroy(ball.gameObject); 
            }

            _balls = new List<Ball>();
        }


        Level level = Resources.Load<Level>("Levels/Level" + SaveManager.SaveModel.Level);

        if(level == null)
        {
            level = Resources.Load<Level>("Levels/Level" + Random.Range(1, Constants.MAX_ADDED_LEVEL + 1)); 
            level.level = SaveManager.SaveModel.Level;
        }

        _levelSettings = level;
        
        _valueToWin = _levelSettings.valueToWin;

        if(SaveManager.SaveModel.Level >= 6)
        {
            _valueToWin *= 2;
        }
        else if (SaveManager.SaveModel.Level >= 12)
        {
            _valueToWin *= 4;
        }


            _level = Instantiate(_levelSettings.levelPrefab);
    }

    public void ResetLevel()
    {
        Lives = 3;
         
        _victory = false;
        _score = 0;

        _oblectsOnRoadController.StopLevel();

        ChickenController.transform.position = Vector3.zero;

        Time.timeScale = 0;

        //foreach (var ball in _balls)
        //{
        //    Destroy(ball.gameObject);
        //}

        //_balls = new List<Ball>();

        //if (_level != null)
        //{
        //    Destroy(_level);
        //}

        //_level = Instantiate(_levelSettings.levelPrefab);
    }

    public void SendBall()
    { 
        StartCoroutine(CreateBallsCoroutine()); 
    }

    private IEnumerator CreateBallsCoroutine()
    {
        for(int i = 0; i <  _amount; i++)
        {
            if(SaveManager.SaveModel.Gold < _value)
            {
                break;
            }

            SaveManager.SaveModel.Gold -= _value;

            Vector3 pos = new Vector3(_createPosition.position.x + Random.Range(-1f, 1f), _createPosition.position.y, _createPosition.position.z);

            Ball ball = Instantiate(_ballPrefab, pos, Quaternion.identity);

            ball.Setup(_value);

            _balls.Add(ball);

            _score *= 0.99f;

            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    public void IncreaseAmount()
    {
        _amount += 1;

        if(_amount > 7)
        {
            _amount = 1;
        }

       // EventsProvider.OnAmountChanged?.Invoke(_amount);
    }

    public void IncreaseValue()
    {
        _value *= 2;
        int maxValue = 8;

        if(SaveManager.SaveModel.Level < 5)
        {
            maxValue = 8;
        }
        else if(SaveManager.SaveModel.Level < 10)
        {
            maxValue = 16;

        }
        else if (SaveManager.SaveModel.Level < 15)
        {
            maxValue = 32;

        }
        else
        {
            maxValue = 64;
        }


        if (_value > maxValue)
        {
            _value = 2;
        }

       //EventsProvider.OnValueChanged?.Invoke(_value);
    }

    public void RemoveBall(Ball ball)
    {
        _balls.Remove(ball);
    }


    private void OnDestroy()
    {
         
    }
}



[System.Serializable]
public class BestResultSlot
{
    public int Level;
    public int Result;

    public BestResultSlot(int level, int result)
    {
        Level = level;
        Result = result;
    }
}
