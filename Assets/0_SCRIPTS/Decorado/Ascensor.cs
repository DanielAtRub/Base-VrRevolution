using UnityEngine;
//using Mirror;

public class Ascensor : MonoBehaviour
{
    [SerializeField]
    private Transform Player, Ascens;
    [SerializeField]
    private float velCaida = 1f;
    [SerializeField]
    private float alturaNivel = 0f;
    //[SyncVar]
    //public bool cayendoAsc;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Ascens)
            Player.position = new Vector3(Player.position.x, Ascens.position.y, Player.position.z);
        else
        {
            if (Player.position.y > alturaNivel) //ALTURA OFICIAL DEL SUELO DEL NIVEL
            {
                //Player.position -= new Vector3(0.0f, Time.deltaTime * velCaida, 0.0f);
                //Player.position = new Vector3(Player.position.x, Player.position.y - velCaida, Player.position.z);
            }
            /*else
            {
                Player.position = new Vector3(Player.position.x, alturaNivel, Player.position.z);
            }*/
        }
    }

    void OnTriggerExit(Collider co)
    {
        // Quita vida al player
        if (co.tag == "Ascensor")
        {
            Ascens = null;
        }
    }

    void OnTriggerStay(Collider co)
    {
        // Quita vida al player
        if (co.tag == "Ascensor")// TAG EN PLAYER
        {
            Ascens = co.transform;
        }
    }

}
