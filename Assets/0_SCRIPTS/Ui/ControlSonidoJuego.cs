using UnityEngine;

public class ControlSonidoJuego : MonoBehaviour
{
    [SerializeField]
    private AudioListener[] listeners;

    // Start is called before the first frame update
    void Start()
    {  
    }

    public void audioON()
    {
        //AudioListener[] listeners = FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
        /*
        for (int index = 0; index < listeners.Length; ++index)
        {
            listeners[index].enabled = true;
        }
        */
        AudioListener.volume = 1f;
    }

    public void audioOFF()
    {
        //AudioListener[] listeners = FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
        /*
        for (int index = 0; index < listeners.Length; ++index)
        {
            listeners[index].enabled = false;
        }
        */
        AudioListener.volume = 0f;
    }

}
