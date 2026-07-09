using UnityEngine;

public class EcesuteAppJUEGO : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("ExeApp", 5.0f);
        //ExeAppJuego();
    }

    public void ExeAppJuego()
    {
        bool fail = false;
        string bundleId = "com.ii.Tse"; // your target bundle id
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
        }
        catch (System.Exception e)
        {
            fail = true;
        }

        if (fail)
        { //open app in store
            //Application.OpenURL("https://google.com");
        }
        else //open the app
            ca.Call("startActivity", launchIntent);

        up.Dispose();
        ca.Dispose();
        packageManager.Dispose();
        launchIntent.Dispose();
        /*
        //Cierras esta app
        using (AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject unityActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
            unityActivity.Call<bool>("moveTaskToBack", false);
        }
        */
    }
}
