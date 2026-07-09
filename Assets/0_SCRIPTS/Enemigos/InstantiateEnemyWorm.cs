using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;
using System.Collections.Generic;

public class InstantiateEnemyWorm : NetworkBehaviour
{
    public GameObject instancia;
    [SerializeField]
    private GameObject Behavior;
    [SerializeField]
    private Transform point1, point2, point3, point4;
    [SerializeField]
    private int punto;

    public bool haveTime;
    public float OrdaTime;

    public bool OrdaKilled;

    private GameObject obj;

    void Start()
    {
    }

    [ServerCallback]
    void Update()
    {
        if (haveTime)
        {
            OrdaTime -= Time.deltaTime;
            if (OrdaTime <= 0)
            {
                OrdaTime = 0;
                Behavior.GetComponent<BehaviourTreeOwner>().enabled = false;

                var found = new List<Transform>();
                foreach (Transform t in Behavior.transform)
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
        punto = Random.Range(1, 5);
        if (punto == 1)
            obj = Instantiate(instancia, point1.transform.position, point1.transform.rotation, Behavior.transform);
        if (punto == 2)
            obj = Instantiate(instancia, point2.transform.position, point2.transform.rotation, Behavior.transform);
        if (punto == 3)
            obj = Instantiate(instancia, point3.transform.position, point3.transform.rotation, Behavior.transform);
        if (punto == 4)
            obj = Instantiate(instancia, point4.transform.position, point4.transform.rotation, Behavior.transform);

        NetworkServer.Spawn(obj);
    }

}
