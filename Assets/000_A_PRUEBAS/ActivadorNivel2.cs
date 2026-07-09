using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorNivel2 : MonoBehaviour
{
    [SerializeField] private ManagerPuerta puerta;
    [SerializeField] private ManagerCajaConexiones conexiones;
    [SerializeField] private AudioSource audio;

    private bool activado = false;

    // Update is called once per frame
    void Update()
    {
        if(conexiones.TodoConectadoOK && puerta.ExplosivosOk && !activado)
        {
            audio.Play();
            activado = true;
        }
    }
}
