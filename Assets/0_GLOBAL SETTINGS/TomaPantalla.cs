using UnityEngine;
using System.Collections;
using System.IO;

public class TomaPantalla : MonoBehaviour
{
    private string nArchivo;
    private int n;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            n++;
            nArchivo = "C:/VrAirsoftGames_BD/" + "img" + n.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(nArchivo, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            n++;
            nArchivo = "C:/VrAirsoftGames_BD/" + "img" + n.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(nArchivo, 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            n++;
            nArchivo = "C:/VrAirsoftGames_BD/" + "img" + n.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(nArchivo, 3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            n++;
            nArchivo = "C:/VrAirsoftGames_BD/" + "img" + n.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(nArchivo, 4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            n++;
            nArchivo = "C:/VrAirsoftGames_BD/" + "img" + n.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(nArchivo, 5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            n++;
            nArchivo = "C:/VrAirsoftGames_BD/" + "img" + n.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(nArchivo, 6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            n++;
            nArchivo = "C:/VrAirsoftGames_BD/" + "img" + n.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(nArchivo, 7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            n++;
            nArchivo = "C:/VrAirsoftGames_BD/" + "img" + n.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(nArchivo, 8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            n++;
            nArchivo = "C:/VrAirsoftGames_BD/" + "img" + n.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(nArchivo, 9);
        }
    }
}