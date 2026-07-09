using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;
using System.Collections.Generic;

public class PoolInstantiateEnemy : NetworkBehaviour
{
    public bool haveTime;
    public float OrdaTime;
    public bool OrdaKilled;

    [SerializeField]
    private GameObject rootObjetos;

    void Start()
    {
    }

    [ServerCallback]
    void Update()
    {
        //SE DESACTIVA Y AÑADE A LISTA LOS OBJETOS QUE DE ObjetosToPool
        //Y LES PONE LA VIDA A 0 AL FINALIZAR EL TIEMPO
        if (haveTime)
        {
            OrdaTime -= Time.deltaTime;
            if (OrdaTime <= 0)
            {
                OrdaTime = 0;
                //Behavior.GetComponent<BehaviourTreeOwner>().enabled = false;
                var found = new List<Transform>();
                foreach (Transform t in rootObjetos.transform)
                {
                    t.gameObject.GetComponent<Enemigo>().EnemyHealth = 0; //PRUEBAS
                    t.gameObject.GetComponent<Enemigo>().destruir = true;  //PRUEBAS
                    found.Add(t);
                }
                OrdaKilled = true;
            }
        }
    }
    
}
