using UnityEngine;
using Mirror;

public class ColTren : NetworkBehaviour
{
    [SerializeField]
    private GameObject Gamemanager;
    private bool trig;

    [ServerCallback]
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tren"))
        {
            if (!trig)
            {
                trig = true;
                Gamemanager.GetComponent<JuegoManager>().Fin();
            }
        }
    }

}
