using System.Collections.Generic; // IMPORTANTE para usar List<>
using UnityEngine;

public class ControlCamaras : MonoBehaviour
{
    // Cambiamos el Array por una Lista para poder añadir jugadores en tiempo real
    public List<GameObject> CamarasJugadores = new List<GameObject>();

    private bool status, status2;

    void Start()
    {
    }

    public void RegistrarCamara(GameObject nuevaCamara)
    {
        if (!CamarasJugadores.Contains(nuevaCamara))
        {
            CamarasJugadores.Add(nuevaCamara);

            // Si las cámaras están apagadas (status = false), apagamos esta nueva también para que no moleste al jugador
            nuevaCamara.SetActive(status);
            if (nuevaCamara.GetComponent<Camera>() != null)
                nuevaCamara.GetComponent<Camera>().enabled = status;
        }
    }

    void Update()
    {
        if (status)
        {
            for (int i = 0; i < CamarasJugadores.Count; i++)
            {
                // Seguridad: comprobar si el jugador se ha desconectado/destruido
                if (CamarasJugadores[i] == null) continue;

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

        for (int i = 0; i < CamarasJugadores.Count; i++)
        {
            if (CamarasJugadores[i] != null)
            {
                CamarasJugadores[i].SetActive(true);
                CamarasJugadores[i].GetComponent<Camera>().enabled = true;

                if (i == 0) CamarasJugadores[i].GetComponent<Camera>().rect = new Rect(0f, 0.5f, 0.33f, 0.5f);
                if (i == 1) CamarasJugadores[i].GetComponent<Camera>().rect = new Rect(0.335f, 0.5f, 0.33f, 0.5f);
                if (i == 2) CamarasJugadores[i].GetComponent<Camera>().rect = new Rect(0.67f, 0.5f, 0.33f, 0.5f);
                if (i == 3) CamarasJugadores[i].GetComponent<Camera>().rect = new Rect(0f, 0f, 0.33f, 0.5f);
                if (i == 4) CamarasJugadores[i].GetComponent<Camera>().rect = new Rect(0.335f, 0f, 0.33f, 0.5f);
                if (i == 5) CamarasJugadores[i].GetComponent<Camera>().rect = new Rect(0.67f, 0f, 0.33f, 0.5f);
            }
        }
    }

    public void DesCamaraJugadores()
    {
        status = false;

        for (int i = 0; i < CamarasJugadores.Count; i++)
        {
            if (CamarasJugadores[i] != null)
            {
                CamarasJugadores[i].SetActive(false);
                CamarasJugadores[i].GetComponent<Camera>().enabled = false;
            }
        }
    }
}