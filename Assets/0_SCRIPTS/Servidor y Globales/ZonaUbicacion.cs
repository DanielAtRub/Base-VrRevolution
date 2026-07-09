using UnityEngine;
using Mirror;

public class ZonaUbicacion : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManager;
    [SerializeField]
    private int Level;

    // Start is called before the first frame update
    void Start()
    {
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ubicacion")
        {
            if (other.GetComponentInParent<Player>().isDead)
                return;

            if (Level == 0)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL0 += 1;
            if (Level == 1)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL1 += 1;
            if (Level == 2)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL2 += 1;
            if (Level == 3)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL3 += 1;
            if (Level == 4)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL4 += 1;
            if (Level == 5)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL5 += 1;
            if (Level == 61)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL6_1 += 1;
            if (Level == 62)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL6_2 += 1;
            if (Level == 71)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL7_1 += 1;
            if (Level == 72)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL7_2 += 1;
            if (Level == 8)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL8 += 1;
            if (Level == 91)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL9_1 += 1;
            if (Level == 92)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL9_2 += 1;
            if (Level == 10)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL10 += 1;
            if (Level == 11)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL11 += 1;
            if (Level == 12)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL12 += 1;
        }
    }
    [ServerCallback]
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ubicacion")
        {
            if (other.GetComponentInParent<Player>().isDead)
                return;

            if (Level == 0)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL0 -= 1;
            if (Level == 1)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL1 -= 1;
            if (Level == 2)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL2 -= 1;
            if (Level == 3)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL3 -= 1;
            if (Level == 4)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL4 -= 1;
            if (Level == 5)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL5 -= 1;
            if (Level == 61)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL6_1 -= 1;
            if (Level == 62)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL6_2 -= 1;
            if (Level == 71)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL7_1 -= 1;
            if (Level == 72)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL7_2 -= 1;
            if (Level == 8)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL8 -= 1;
            if (Level == 91)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL9_1 -= 1;
            if (Level == 92)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL9_2 -= 1;
            if (Level == 10)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL10 -= 1;
            if (Level == 11)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL11 -= 1;
            if (Level == 12)
                GameManager.GetComponent<JuegoManager>().PlayersUbicadosL12 -= 1;
        }
    }

}
