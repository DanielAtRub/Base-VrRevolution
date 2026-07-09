using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Mirror;
using TMPro;

//ABRE EL LAUNCHER Y CIERRA EL JUEGO
public class AbreLyCierraCLIENT : MonoBehaviour
{
    [SerializeField]
    private GameObject Manager;

    [SerializeField]
    private TextMeshPro textDebug;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager)
            Manager = GameObject.Find("NetworkManager");

        if (Manager.GetComponent<NetworkManager>().SeCerroOnline)
        {
            textDebug.text = "SeCerroOnline " + Manager.GetComponent<NetworkManager>().SeCerroOnline.ToString();
            LanzaLauncher();
        }
    }

    public void LanzaLauncher() //LLAMADO DESDE ONLINE TO OFFLINE ????
    {
        
        bool fail = false;
        //string bundleId = "com.ii." + nombreEsteJuego; // your target bundle id
        string bundleId = "com.ii.Launcher_BD"; // your target bundle id
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
        
        //CIERRA EL JUEGO
        /*
        using (AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject unityActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
            unityActivity.Call<bool>("moveTaskToBack", false);
        }
        */
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("finish");

        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

}
