using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamaras : MonoBehaviour
{
    private GameObject[] CamarasJugadores;

    private bool status, status2;

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        //ACT/DES EL FULLSCREEN DE LAS CAMARAS DE LOS PLAYERS
        if (status) {
            for (int i = 0; i < CamarasJugadores.Length; i++)
            {
                if (Input.GetButtonDown("Fire2") && 
                    CamarasJugadores[i].GetComponent<Camera>().pixelRect.Contains(Input.mousePosition) &&
                    !status2)
                {
                    status2 = true;
                    CamarasJugadores[i].GetComponent<Camera>().depth = 20;
                    CamarasJugadores[i].GetComponent<Camera>().rect = new Rect(0f, 0f, 1f, 1f);
                }
                else if (Input.GetButtonDown("Fire2") &&
                    CamarasJugadores[i].GetComponent<Camera>().pixelRect.Contains(Input.mousePosition) &&
                    status2)
                {
                    status2 = false;
                    CamarasJugadores[i].GetComponent<Camera>().depth = 2;
                    ActCamaraJugadores();
                }
            }
        }    
    }

    public void ActCamaraJugadores()
    {
        status = true;

        CamarasJugadores = GameObject.FindGameObjectsWithTag("CamaraJugador");
        for (int i = 0; i < CamarasJugadores.Length; i++)
        {
            CamarasJugadores[i].SetActive(true);
            CamarasJugadores[i].GetComponent<Camera>().enabled = true;

            if (i == 0) //Camara P1
                CamarasJugadores[0].GetComponent<Camera>().rect = new Rect(0f, 0.5f, 0.33f, 0.5f);
            if (i == 1) //Camara P2
                CamarasJugadores[1].GetComponent<Camera>().rect = new Rect(0.335f, 0.5f, 0.33f, 0.5f);
            if (i == 2) //Camara P3
                CamarasJugadores[2].GetComponent<Camera>().rect = new Rect(0.67f, 0.5f, 0.33f, 0.5f);
            if (i == 3) //Camara P4
                CamarasJugadores[3].GetComponent<Camera>().rect = new Rect(0f, 0f, 0.33f, 0.5f);
            if (i == 4) //Camara P5
                CamarasJugadores[4].GetComponent<Camera>().rect = new Rect(0.335f, 0f, 0.33f, 0.5f);
            if (i == 5) //Camara P6
                CamarasJugadores[5].GetComponent<Camera>().rect = new Rect(0.67f, 0f, 0.33f, 0.5f);
        }
        //PONER UNA IMAGEN CON 6 FONDOS NEGROS (NO PLAYER)
    }

    public void DesCamaraJugadores()
    {
        status = false;

        CamarasJugadores = GameObject.FindGameObjectsWithTag("CamaraJugador");
        for (int i = 0; i < CamarasJugadores.Length; i++)
        {
            CamarasJugadores[i].SetActive(false);
            CamarasJugadores[i].GetComponent<Camera>().enabled = false;
        }
    }
}
