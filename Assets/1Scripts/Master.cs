
using AppsFlyerSDK;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.Android; 


public class Master : MonoBehaviour //, IAppsFlyerConversionData
{
    public static Master Instance;

    public static UIController UIController => Instance._uIController;
    public static GameStageController GameStageController => Instance._gameStageController;

    public static bool GameLaunched {  get; private set; }

    public string LastURL;

    public bool afterPush;


    [SerializeField] private UIController _uIController;


    [SerializeField] private GameObject _bgBlack;
    [SerializeField] private UniWebView _webviewPrefab;
    [SerializeField] private Transform _viewTransform;

    private GameStageController _gameStageController;
     
    private UniWebView _webview;
    private float _timeToShow = 0.4f;
    private bool _loading = false;

    private DeviceOrientation _lastFrameOrientation;
    private string _userAgent = string.Empty;


    private float ScreenOffset => Screen.height > Screen.width ? Screen.height * 0.04f: Screen.width * 0.04f;


    private void Awake()
    { 
        Instance = this;

        DontDestroyOnLoad(this);
        SaveManager.Load();
        _gameStageController = new GameStageController();
         
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = ScreenOrientation.Portrait;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60; 
    }


    private void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            NoInternetPopup popup = UIController.GetPopup<NoInternetPopup>();
            popup.Setup(null);

            return;
        }


        if (SaveManager.SaveModel.Mode == 0 && SaveManager.SaveModel.FirstLaunch == false)
        {
            UIController.SetLoadingScreen();
            LaunchGame();
            return;
        }


#if UNITY_EDITOR ///|| UNITY_ANDROID
        UIController.SetLoadingScreen();
        LaunchGame(); 

        return;
#endif

        UIController.SetLoadingScreen();

        EventsProvider.OnConfigRequestDone += ConfigResult;
    }

    private void ConfigResult(bool success)
    {
        if(afterPush)
        {
            return;
        }

        if (RequestController.Stage == Enums.RequestStage.Successfull)
        {
            UIController.RemoveLoadingScreen();

            if (Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS") == false)
            {
                var minutes = (System.DateTime.Now - SaveManager.SaveModel.LastAskForNotifications).TotalMinutes;

                Debug.Log("### Minutes between = " +  minutes);
                 
                if (minutes > 4320 && SaveManager.SaveModel.NotificationPermissionAsked < 2)
                {
                    SaveManager.SaveModel.LastAskForNotifications = System.DateTime.Now;
                    SaveManager.Save();

                    NotificationsPopup popup = UIController.GetPopup<NotificationsPopup>();
                    popup.Setup(null, AskPermission, () => OpenView(RequestController.WebViewUrl));
                } 
                else
                {
                    UIController.SetLoadingScreen();
                    OpenView(RequestController.WebViewUrl);
                }
            }
            else
            {
                UIController.SetLoadingScreen();

                OpenView(RequestController.WebViewUrl);
            }

            if (SaveManager.SaveModel.FirstLaunch)
            {
                SaveManager.SaveModel.FirstLaunch = false;
                SaveManager.SaveModel.Mode = 1;
                SaveManager.Save();
            }
        }
        else
        {
            SaveManager.SaveModel.FirstLaunch = false;
            SaveManager.SaveModel.Mode = 0;
            SaveManager.Save();
            LaunchGame();
        }
    }

    private void AskPermission()
    {
        SaveManager.SaveModel.NotificationPermissionAsked++; 
        SaveManager.Save();

        // if (Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS") == false)
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
            
        }

        

        OpenView(RequestController.WebViewUrl);
    }

    private void LaunchGame()
    {
        GameLaunched = true;
        _bgBlack.SetActive(false);

        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = ScreenOrientation.Portrait;

        if (SaveManager.SaveModel.Gold < 1000)
        {
            SaveManager.SaveModel.Gold += Random.Range(200, 600);
        }

        _gameStageController.ChangeStage(Enums.GameStage.Menu,
            () =>
            {
                Master.UIController.OpenScreen(Enums.GameStage.Menu);
                UIController.RemoveLoadingScreen();
            }
            );
    }

    private void Update()
    {
        if (_webview != null)
        {
            if (_loading == false)
            {
                _timeToShow -= Time.unscaledDeltaTime;

                if (_timeToShow <= 0)
                {
                    _webview.Show();
                    _loading = true;
                    _timeToShow = Constants.DELAY_TO_SHOW_VIEW;
                    Master.UIController.RemoveLoadingScreen();
                }
            }
        }
    }


    public void OpenView(string url)
    {
        if (_webview == null)
        {
            _webview = Instantiate(_webviewPrefab, _viewTransform);


            if (UnityEngine.Screen.orientation == UnityEngine.ScreenOrientation.Portrait)
            {
                _webview.Frame = new Rect(0, 0, UnityEngine.Screen.width, UnityEngine.Screen.height);
                _webview.EmbeddedToolbar.SetBackgroundColor(Color.black);
                _webview.EmbeddedToolbar.SetButtonTextColor(Color.black);
                _webview.EmbeddedToolbar.HideNavigationButtons();
                _webview.EmbeddedToolbar.SetMaxHeight(ScreenOffset);
                _webview.EmbeddedToolbar.Show();
            }
            else if (UnityEngine.Screen.orientation == UnityEngine.ScreenOrientation.LandscapeLeft)
            {
                _webview.Frame = new Rect(100, 0, UnityEngine.Screen.width - 100, UnityEngine.Screen.height);
               // _webview.Frame = new Rect(0, 0, UnityEngine.Screen.width, UnityEngine.Screen.height);
                _webview.EmbeddedToolbar.SetBackgroundColor(Color.black);
                _webview.EmbeddedToolbar.SetButtonTextColor(Color.black);
                _webview.EmbeddedToolbar.HideNavigationButtons();
                _webview.EmbeddedToolbar.SetMaxHeight(ScreenOffset);
                _webview.EmbeddedToolbar.Hide();
            }
            else if (UnityEngine.Screen.orientation == UnityEngine.ScreenOrientation.LandscapeRight)
            {
                _webview.Frame = new Rect(0, 0, UnityEngine.Screen.width - 100, UnityEngine.Screen.height);
               // _webview.Frame = new Rect(0, 0, UnityEngine.Screen.width, UnityEngine.Screen.height);
                _webview.EmbeddedToolbar.SetBackgroundColor(Color.black);
                _webview.EmbeddedToolbar.SetButtonTextColor(Color.black);
                _webview.EmbeddedToolbar.HideNavigationButtons();
                _webview.EmbeddedToolbar.SetMaxHeight(ScreenOffset);
                _webview.EmbeddedToolbar.Hide();
            }


            _webview.BackgroundColor = Color.black;
            _webview.RegisterOnRequestMediaCapturePermission(permission => UniWebViewMediaCapturePermissionDecision.Grant);

            UniWebView.SetEnableKeyboardAvoidance(true);
            _webview.AddUrlScheme("bankid");
            _webview.AddUrlScheme("paytmmp");
            _webview.AddUrlScheme("phonepe");

            StartCoroutine(CheckUserAgent());

            _webview.SetContentInsetAdjustmentBehavior(UniWebViewContentInsetAdjustmentBehavior.Always);

            _webview.UpdateFrame();


            _webview.OnPageFinished += (view, statusCode, url) =>
            {
                _timeToShow = Constants.DELAY_TO_SHOW_VIEW;
                _loading = false;
                LastURL = url;
                //_webview.Show()
            };

            _webview.OnLoadingErrorReceived += (view, code, message, payload) => {
                Debug.Log("ERROR STATUS" + url + " = " + code + message);
                _webview.Reload();
                _loading = true;
            };



            _webview.OnOrientationChanged += (view, orientation) =>
            {
                if (orientation == ScreenOrientation.Portrait)
                {
                    _webview.Frame = new Rect(0, 0, Screen.width, Screen.height); 

                    _webview.EmbeddedToolbar.Show();
                }
                else if (orientation == ScreenOrientation.LandscapeLeft )
                {
                    _webview.Frame = new Rect(ScreenOffset, 0, Screen.width - ScreenOffset, Screen.height); 
                    _webview.EmbeddedToolbar.Hide();
                }
                else if (orientation == ScreenOrientation.LandscapeRight)
                {
                    _webview.Frame = new Rect(0, 0, Screen.width - ScreenOffset, Screen.height);
                    _webview.EmbeddedToolbar.Hide();
                }

                _webview.UpdateFrame();
            };

            _webview.OnPageStarted += OnPageStarted;

            _webview.OnMessageReceived += (view, message) =>
            {
                Application.OpenURL(message.Scheme + ":///");

                Debug.Log("OnMessageReceived" + url + " = " + message.Scheme);
            };
            _bgBlack.SetActive(true);
            _webview.Load(url); 
        }
    }



    private void OnPageStarted(UniWebView webView, string url)
    {
    }





    private IEnumerator CheckUserAgent()
    {
        Debug.Log("###CheckUserAgent  " + " = " + _userAgent);
        while (_userAgent == string.Empty)
        {
            Debug.Log("###userAgent  " + " = " + _userAgent);

            string userAgent = string.Empty;

            if(_webview != null)
            {
                userAgent =_webview.GetUserAgent();
            }

            if (userAgent != string.Empty)
            {
                userAgent = userAgent.Replace("; wv", "");
                userAgent = userAgent.Replace("wv", "");
                userAgent = userAgent.Replace("Version/4.0", "");

                Debug.Log("###userAgent  " + " = " + userAgent);

                _webview.SetUserAgent(userAgent);
                _userAgent = userAgent;
            }

            yield return null;
        }

        Debug.Log("###END CheckUserAgent  " + " = " + _userAgent);
    }


    

     


        [SerializeField] private BallData[] _ballsData; 

    public BallData GetBallData(float value)
    {
        for (int i = 0; i < _ballsData.Length; i++)
        {
            if (_ballsData[i].Value == value)
            {
                return _ballsData[i];
            }
        }

        return null;
    }

    //public void onConversionDataSuccess(string conversionData)
    //{
    //    AppsFlyer.AFLog("didReceiveConversionData", conversionData);

    //    Dictionary<string, object> conversionDataDictionary = AppsFlyer.CallbackStringToDictionary(conversionData);


    //    foreach (KeyValuePair<string, object> entry in conversionDataDictionary)
    //    {
    //        if (entry.Value != null)

    //            Debug.Log(entry.Key + " " + entry.Value.ToString());
    //    }
    //}

    //public void onConversionDataFail(string error)
    //{
    //    //throw new System.NotImplementedException();
    //}

    //public void onAppOpenAttribution(string attributionData)
    //{
    //   // throw new System.NotImplementedException();
    //}

    //public void onAppOpenAttributionFailure(string error)
    //{
    //    //throw new System.NotImplementedException();
    //}
}

[System.Serializable]
public class BallData
{
    public float Value;
    public Material Material;
    public Vector3 Size;

}

