using UnityEngine;
using Firebase.Messaging;
using Firebase.Extensions;
using System;
using System.Collections;


public class FirebaseWrapper : MonoBehaviour
{
    // Start is called once before the te after the MonoBehaviour is created
  

    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var status = task.Result;

            if (status == Firebase.DependencyStatus.Available)
            {
                Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

                //StartCoroutine(FirebaseStart());


                Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenRecieved;
                Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageRecieved;
            }
            else
            {
                Debug.LogError("Firebase ERROR = " + status);
            }


        });
    }

    private IEnumerator FirebaseStart()
    {
        yield return new WaitForSeconds(0.1f);
        
    }

    private void OnMessageRecieved(object sender, MessageReceivedEventArgs e)
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

            str += "\n key = " + data.Key.ToString() + " val =" +data.Value.ToString();
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
