using UnityEngine;
using Firebase.Messaging;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;


public class FirebaseWrapper : MonoBehaviour
{
    //// Start is called once before the te after the MonoBehaviour is created

    //private string fcmToken;

    //private void Start()
    //{
    //    Debug.Log("Firebase ini start ");

    //    Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
    //    {
    //        var dependencyStatus = task.Result;
    //        if (dependencyStatus == Firebase.DependencyStatus.Available)
    //        {
    //            // Create and hold a reference to your FirebaseApp,
    //            // where app is a Firebase.FirebaseApp property of your application class.
    //            Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

    //            // Set a flag here to indicate whether Firebase is ready to use by your app.


    //            // Firebase.Messaging.FirebaseMessaging.reg



    //            // On iOS, this will display the prompt to request permission to receive
    //            // notifications if the prompt has not already been displayed before. (If
    //            // the user already responded to the prompt, thier decision is cached by
    //            // the OS and can be changed in the OS settings).
    //            // On Android, this will return successfully immediately, as there is no
    //            // equivalent system logic to run.


    //            //StartCoroutine(GetTokenAsync()); 


    //            InitializeFirebase();
    //        }
    //        else
    //        {
    //            UnityEngine.Debug.LogError(System.String.Format(
    //              "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    //            // Firebase Unity SDK is not safe to use here.
    //        }
    //    });
    //}



    //private IEnumerator  GetTokenAsync()
    //{
    //    var task = FirebaseMessaging.GetTokenAsync();

    //    while (!task.IsCompleted)
    //        yield return new WaitForEndOfFrame();

    //    Debug.Log("GET TOKEN ASYNC " + task.Result);

    //    Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenRecieved;
    //    Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    //    Firebase.Messaging.FirebaseMessaging.SubscribeAsync("TestTopic").ContinueWithOnMainThread(task =>
    //    {
    //        Debug.Log("SubscribeAsync");
    //    });
    //    Debug.Log("Firebase Messaging Initialized");

    //    Firebase.Messaging.FirebaseMessaging.RequestPermissionAsync().ContinueWithOnMainThread(
    //             task =>
    //             {
    //                 Debug.Log("RequestPermissionAsync");
    //             }
    //           );

    //    RequestController.FirebaseMessagingToken = task.Result; // e.Token;

    //    RequestController.FirebaseDataRecieved = true;




    //    RequestController.Check();
    //}


    //async void InitializeFirebase()
    //{
    //    FirebaseMessaging.MessageReceived += OnMessageReceived;
    //    await RegenerateFcmToken();
    //}

    //private async Task RegenerateFcmToken()
    //{
    //    try
    //    {
    //        await FirebaseMessaging.RequestPermissionAsync(); 

    //        Debug.Log("Requesting new FCM Registration Token ...");
    //        await FirebaseMessaging.DeleteTokenAsync();
    //        await FirebaseMessaging.GetTokenAsync().ContinueWithOnMainThread(task =>
    //        {
    //            if (task.Exception != null)
    //            {
    //                Debug.LogError("#1##" + task.Exception.Message);
    //                return;
    //            }

    //            fcmToken = task.Result;
    //            Debug.Log("Received Registration Token: " + fcmToken);

    //        });

    //    }
    //    catch (Exception e)
    //    {
    //        Master.Instance.LaunchGame();
    //        Debug.LogError("#1##" + e.Message);
    //        throw;
    //    }
    //}


    private void Start() 
    {  
    Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
    {
        var status = task.Result;

        if (status == Firebase.DependencyStatus.Available)
        {
            Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

            //StartCoroutine(FirebaseStart());

            Debug.Log("Firebase ini = " + status);
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenRecieved;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        }
        else
        {
            Debug.LogError("Firebase ERROR = " + status);
        }
    });
    }

    private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        string str = "";

        foreach (var data in e.Message.Data)
        {
            if (data.Key == "url")
            {
                Debug.Log("###PUSH successful" + data.Key + " " + data.Value);
                Master.Instance.afterPush = true;

                Master.Instance.OpenView(data.Value);
            }

            str += "\n key = " + data.Key.ToString() + " val =" + data.Value.ToString();
        }



        Debug.Log("OnMessageRecieved  = " + str);
        Debug.Log("OnMessageRecieved  = " + e.Message.ToString());
    }

    private void OnTokenRecieved(object sender, TokenReceivedEventArgs e)
    {
        Debug.Log("FirebaseWrapper OnTokenRecieved  = " + e.Token);

        RequestController.FirebaseMessagingToken = e.Token;

        RequestController.FirebaseDataRecieved = true;
        RequestController.Check();
    }

}
