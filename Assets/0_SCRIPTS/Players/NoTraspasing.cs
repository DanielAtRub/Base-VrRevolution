using UnityEngine;
using Mirror;

// COLOCADO EN EL SPINE1 DEL JUGADOR
public class NoTraspasing : NetworkBehaviour
{
    [Header("CON OBJETOS Y LIMITE ESCENARIO")]
    [SerializeField]
    private float velDead = 0.2f;
    private float tempTime;
    
    void Start()
    {
    }

    [ServerCallback]
    void OnTriggerStay(Collider co)
    {
        // Quita vida al player
        if (co.CompareTag ("NoTraspasing"))
        {
            tempTime += Time.deltaTime;
            if (tempTime > velDead)
            {
                tempTime -= velDead;
                GetComponentInParent<Player>().PlayerHealth -= 1;
            }
            GetComponentInParent<Player>().Mensaje = "¡No atravieses objetos sólidos!";
            GetComponentInParent<Player>().aNegro = true;
        }
    }

    [ServerCallback]
    void OnTriggerExit(Collider co)
    {
        if (co.CompareTag ("NoTraspasing"))
        {
            GetComponentInParent<Player>().Mensaje = ".";
            GetComponentInParent<Player>().aNegro = false;
        }
    }

}
