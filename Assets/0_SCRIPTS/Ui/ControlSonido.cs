using UnityEngine;

public class ControlSonido : MonoBehaviour
{
    [SerializeField]
    private AudioListener audioListener;

    // Start is called before the first frame update
    void Start()
    {  
    }

    public void audioON()
    {
        //audioListener.enabled = true;
        AudioListener.volume = 1f;
    }

    public void audioOFF()
    {
        //audioListener.enabled = false;
        AudioListener.volume = 0f;
    }
}
