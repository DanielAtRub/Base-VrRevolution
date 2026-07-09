using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;
using System.Collections.Generic;

public class InstantiateEnemy : NetworkBehaviour
{
    public GameObject instancia;
    //public int TotalMaxEnemigos = 10;

    //public int incEnemigos;
    //public bool DoIncEnemigos;
    [SerializeField]
    private GameObject Behavior;

    public Transform point;

    public bool haveTime;
    public float OrdaTime;

    public bool OrdaKilled;

    void Start()
    {
    }

    [Server]
    void Update()
    {
        /*
        if (DoIncEnemigos)
        {
            DoIncEnemigos = false;
            TotalMaxEnemigos += incEnemigos;
        }
        */
        //SE DESACTIVA Y AÑADE A LISTA LOS OBJETOS QUE DE ObjetosToPool
        //Y LES PONE LA VIDA A 0 AL FINALIZAR EL TIEMPO
        if (haveTime)
        {
            OrdaTime -= Time.deltaTime;
            if (OrdaTime <= 0)
            {
                OrdaTime = 0;
                Behavior.GetComponent<BehaviourTreeOwner>().enabled = false;
                //gameObject.SetActive(false); //ME DESACTIVO, Esto sustituye al anterior linea

                var found = new List<Transform>();
                foreach (Transform t in point)
                //foreach (Transform t in transform)
                {
                    t.gameObject.GetComponent<Enemigo>().EnemyHealth = 0;
                    found.Add(t);
                }

                OrdaKilled = true;
            }
        }
    }

    [Server]
    public void InstanciaEnemy()
    {
        GameObject obj = Instantiate(instancia, point.transform.position, point.transform.rotation,
             point);
        NetworkServer.Spawn(obj);
    }

}
