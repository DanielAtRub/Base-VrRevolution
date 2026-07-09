using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//SCRIPT EN LA CAMARA SERVER DEL PLAYER

public class SombreroCamServer : NetworkBehaviour
{
    [SerializeField]
    private GameObject Sombrero, SombreroMuerto;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    [ServerCallback]
    void Update()
    {
        if (GetComponent<Camera>().isActiveAndEnabled)
        {
            if (GetComponent<Camera>().depth == 20)
            {
                Sombrero.SetActive(false);
                SombreroMuerto.SetActive(false);
            }
            else
            {
                Sombrero.SetActive(true);
                SombreroMuerto.SetActive(true);
            }
        }
        else
        {
            Sombrero.SetActive(true);
            SombreroMuerto.SetActive(true);
        }
    }
}
