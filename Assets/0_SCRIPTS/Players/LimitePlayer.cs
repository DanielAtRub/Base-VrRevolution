using UnityEngine;
using Mirror;

// COLOCADO EN EL LIMITE DEL JUGADOR
// ACT/DES LA REJILLA DEL PERIMETRO DEL ESPACIO DE JUEGO
public class LimitePlayer : NetworkBehaviour
{
    [SerializeField]
    private AudioSource alertaAudio;

    void Start()
    {
    }

    [ClientCallback]
    void OnTriggerEnter(Collider co)
    {
        if (co.CompareTag ("Finish"))
        {
            MeshRenderer mr = co.GetComponent<MeshRenderer>();
            mr.material.SetColor("_TintColor", new Color(1, 0.46f, 0, 1f));
            alertaAudio.Play();
        }
    }

    [ClientCallback]
    void OnTriggerExit(Collider co)
    {
        if (co.CompareTag("Finish"))
        {
            MeshRenderer mr = co.GetComponent<MeshRenderer>();
            mr.material.SetColor("_TintColor", new Color(1, 0.46f, 0, 0f));
        }
    }

}
