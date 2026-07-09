using UnityEngine;
using Mirror;
using TMPro; //PRUEBAS

using UnityEngine.InputSystem; //PICO

public class SOLOPICODisparoPlayer : NetworkBehaviour
{
    [Header("Firing")]
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileMount;
    [SyncVar(hook = nameof(SetNivelDisparo))]
    public int Items;
    [SerializeField]
    private int NivelDisparo;
    [SerializeField]
    private float fireRate = 0.4f;
    private float nextFire;
    [SerializeField]
    private GameObject Fogonazo;
    [SerializeField]
    private bool canShoot;
    [SerializeField]
    private AudioSource SonidoSinBalas, SonidoItem;
    [SerializeField]
    private TextMeshPro textNivelDisparo;
    [SerializeField]
    private GameObject GameManager;

    [Header("PICO")]
    [SerializeField]
    private InputActionReference rightTriggerAction; //PICO

    void Start()
    {
    }

    void Update()
    {
        // for local player
        if (!isLocalPlayer) return;

        if (!GameManager)
            GameManager = GameObject.Find("GameManager");

        // shoot
        //if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && Time.time > nextFire)
        if (rightTriggerAction.action.ReadValue<float>() > 0.8f && Time.time > nextFire) //PICO
        {
            if (!GetComponent<Player>().isDead)
            {
                CmdFire(projectileMount.position, projectileMount.rotation, projectileMount.TransformDirection(Vector3.forward));
                // Update the time when our player can fire next
                nextFire = Time.time + fireRate;
            }
            else
            {
                if (SonidoSinBalas) SonidoSinBalas.Play();
                nextFire = Time.time + fireRate;
            }
        }
    }

    // this is ejecutado on the Player server
    //ESTA OPCION COGE LA POSICION Y ROTACION DEL CLIENTE YA QUE LO QUE HAY DENTRO DEL COMMAND ES DEL SERVIDOR
    [Command]
    void CmdFire(Vector3 firePos, Quaternion eulerAngles, Vector3 fireDirection)
    {
        canShoot = true;
        RaycastHit hit;
        if (Physics.Raycast(firePos, fireDirection, out hit, 1f))
        {
            if (hit.collider.tag == "Interactive")
                canShoot = false;
        }
        if (canShoot)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePos, eulerAngles);
            projectile.GetComponent<BalaPlayer>().PlayerPropietario = gameObject; //Envia a la bala el Gameobject del Player tirador
            NetworkServer.Spawn(projectile);

            fogonazoServidor();//SOLO PARA EL SERVIDOR POR EJECUTARSE DENTRO DEL COMMAND
            RpcFogonazo();//DESDE EL SERVIDOR PARA ALLS LOS CLIENTES
        }
    }

    // this is ejecutado on the Player that fired for all observers
    [ClientRpc]
    void RpcFogonazo()
    {
        if (Fogonazo != null)
            Fogonazo.GetComponent<ParticleSystem>().Play();
    }
    void fogonazoServidor()
    {
        if (Fogonazo != null)
            Fogonazo.GetComponent<ParticleSystem>().Play();
    }

    //FUNCIONES HOOK - LO HACE SOBRE TODOS LOS CLIENTES
    void SetNivelDisparo(int oldNivelDisparo, int newNivelDisparo)
    {
        if (SonidoItem) SonidoItem.Play();

        //if (newNivelDisparo >= 0 && newNivelDisparo < 2)
        if (newNivelDisparo == 1)
        {
            NivelDisparo = 1;
            fireRate = 0.45f;
        }
        //if (newNivelDisparo >= 2 && newNivelDisparo < 4)
        if (newNivelDisparo == 2)
        {
            NivelDisparo = 2;
            fireRate = 0.4f;
        }
        //if (newNivelDisparo >= 4 && newNivelDisparo < 6)
        if (newNivelDisparo == 3)
        {
            NivelDisparo = 3;
            fireRate = 0.35f;
        }
        //if (newNivelDisparo >= 6 && newNivelDisparo < 8)
        if (newNivelDisparo == 4)
        {
            NivelDisparo = 4;
            fireRate = 0.3f;
        }
        //if (newNivelDisparo >= 8)
        if (newNivelDisparo == 5)
        {
            NivelDisparo = 5;
            fireRate = 0.25f;
        }
        //ACTUALIZA TEXTO
        if (NivelDisparo < 5)
            textNivelDisparo.text = NivelDisparo.ToString();
        else
            textNivelDisparo.text = "max";
    }
}
