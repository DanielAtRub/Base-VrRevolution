using System;
using UnityEngine;
using Mirror;

public class PoolSpawnManager : NetworkBehaviour
{
    public int m_ObjectPoolSize = 5;
    public GameObject m_Prefab;
    public GameObject[] m_Pool;

    public uint assetId { get; set; }

    public delegate GameObject SpawnDelegate(Vector3 position, Guid assetId);
    public delegate void UnSpawnDelegate(GameObject spawned);

    void Start()
    {
        assetId = m_Prefab.GetComponent<NetworkIdentity>().assetId;
        m_Pool = new GameObject[m_ObjectPoolSize];
        for (int i = 0; i < m_ObjectPoolSize; ++i)
        {
            m_Pool[i] = (GameObject)Instantiate(m_Prefab, Vector3.zero, Quaternion.identity, this.transform);
            m_Pool[i].name = "PoolObject" + i;
            m_Pool[i].SetActive(false);
        }

        NetworkClient.RegisterSpawnHandler(assetId, SpawnObject, UnSpawnObject);
    }

    public GameObject GetFromPool(Vector3 position)
    {
        foreach (var obj in m_Pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }

   /* public GameObject SpawnObject(Vector3 position, Guid assetId)
    {
        return GetFromPool(position);
    }*/
    public GameObject SpawnObject(SpawnMessage msg)
    {
        return GetFromPool(msg.position);
    }
    public void UnSpawnObject(GameObject spawned)
    {
        spawned.SetActive(false);
    }
}
