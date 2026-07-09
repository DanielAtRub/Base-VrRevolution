using UnityEngine;
using Mirror;

public class AspasEnemigo : NetworkBehaviour
{
    [SerializeField]
    private float velDead = 0.5f;
    private float tempTime;

    void Start()
    {
    }

    [ServerCallback]
    void OnTriggerStay(Collider co)
    {
        // Quita vida al player
        if (co.tag == "Player")
        {
            tempTime += Time.deltaTime;
            if (tempTime > velDead)
            {
                tempTime -= velDead;
                co.GetComponentInParent<Player>().PlayerHealth -= 1;
            }
        }
    }

}
