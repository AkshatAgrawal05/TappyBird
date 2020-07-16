using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    [SerializeField]
    private GameObject[] birds;
    private bool isProcessing = false;
    private bool isFocus = false;

    void MakeInstance() {
        if (instance == null) {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start() {

    }

    private void OnApplicationFocus(bool focus) {
        isFocus = focus;
    }

    public void PlayGame() {
        SceneFader.instance.FadeIn("Gameplay");
    }

    public void ShareGames() {
    #if UNITY_ANDROID
            if (!isProcessing) {
                StartCoroutine(ShareInAnroid());
            }
    #else
		    Debug.Log("No sharing set up for this platform.");
    #endif
    }

    private IEnumerator ShareInAnroid() {

       /* var shareSubject = "I challenge you to beat my high score";
        var shareMessage = "I challenge you to beat my high score in Fire Block. " +
                           "Get the Fire Block app from the link below. \nCheers\n\n" +
                           Application.identifier; */

        isProcessing = true;

        if (!Application.isEditor) {
            //Create intent for action send
            AndroidJavaClass intentClass =
                new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject =
                new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>
                ("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //put text and subject extra
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
           // intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            //intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Hey check out my app at: https://play.google.com/store/apps/details?id=" + Application.identifier);
            //call createChooser method of activity class
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity =
                unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser =
                intentClass.CallStatic<AndroidJavaObject>
                ("createChooser", intentObject, "Share App");
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
    }
}
