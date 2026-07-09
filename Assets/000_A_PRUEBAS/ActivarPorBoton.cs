using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class ActivarPorBoton : NetworkBehaviour
{
    public UnityEvent Componente;

    [SerializeField]
    private bool activarComponente = false;

    [Server]
    public void CambiarEstadoComponente(bool nuevoEstado)
    {
        activarComponente = nuevoEstado;

        if (activarComponente)
        {
            EjecutarServidor();
        }
    }

    [Server]
    void EjecutarServidor()
    {
        if (Componente != null)
        {
            Componente.Invoke();
        }
    }
}