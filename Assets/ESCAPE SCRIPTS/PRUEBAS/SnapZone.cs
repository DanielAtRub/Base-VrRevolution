using UnityEngine;
using Mirror;

public class SnapZone : NetworkBehaviour
{
    [SerializeField]
    private string OkTag;

    // Sincronizamos el estado de la zona para que todos los clientes sepan si está ocupada
    [SyncVar]
    public bool isOccupied = false;

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        // Solo reproduce el sonido si la zona está libre y el objeto tiene el tag correcto
        if (!isOccupied && other.CompareTag(OkTag))
        {
            if (TryGetComponent<AudioSource>(out AudioSource audio))
            {
                audio.Play();
            }
        }
    }
}