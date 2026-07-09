using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class SangreEnemy : NetworkBehaviour
{
    public GameObject BloodFX;

    //public GameObject BloodAttach;
    //public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    /*Transform GetNearestObject(Transform hit, Vector3 hitPos)
    {
        var closestPos = 100f;
        Transform closestBone = null;
        var childs = hit.GetComponentsInChildren<Transform>();

        foreach (var child in childs)
        {
            var dist = Vector3.Distance(child.position, hitPos);
            if (dist < closestPos)
            {
                closestPos = dist;
                closestBone = child;
            }
        }

        var distRoot = Vector3.Distance(hit.position, hitPos);
        if (distRoot < closestPos)
        {
            closestPos = distRoot;
            closestBone = hit;
        }
        return closestBone;
    }*/

    [Server]
    public void Sangre(Vector3 pos, Quaternion rot, Transform root, string hit) // LLAMADO DESDE DISPAROHIT
    {
        if (hit != "" && hit != null)
        {
            //Transform a = GameObject.Find(hit).transform;
            GameObject a = gameObject.GetComponentsInChildren<Transform>().
                FirstOrDefault(c => c.gameObject.name == hit)?.gameObject;
            if (a)
            {
                var instance = Instantiate(BloodFX, pos, rot);
                instance.transform.SetParent(a.transform, true);
                //var instance = Instantiate(BloodFX, pos, rot, root);
                //instance.GetComponent<HitBalaDestroyLocal>().hit = a.transform;
            }
        }

        //var instance = Instantiate(BloodFX, pos, Quaternion.Euler(0, angle + 90, 0));
        //var instance = Instantiate(BloodFX, pos, rot);
        //var settings = instance.GetComponent<BFX_BloodSettings>();
        //settings.DecalLiveTimeInfinite = false;
        //Destroy(instance, 10);
        //NetworkServer.Spawn(instance);

        /*var nearestBone = GetNearestObject(root, pos);
        if (nearestBone != null)
        {
            var attachBloodInstance = Instantiate(BloodAttach);
            var bloodT = attachBloodInstance.transform;
            bloodT.position = pos;
            bloodT.localRotation = Quaternion.identity;
            bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
            bloodT.LookAt(pos + normal, direction);
            bloodT.Rotate(90, 0, 0);
            bloodT.transform.parent = nearestBone;
            Destroy(attachBloodInstance, 20);
        }*/

        RpcSangre(pos, rot, root, hit);
    }
    [ClientRpc]
    void RpcSangre(Vector3 fPos, Quaternion frot, Transform root, string hit)
    {
        //var instance = Instantiate(BloodFX, fPos, frot, root);
        //instance.GetComponent<HitBalaDestroyLocal>().hit = transform.Find(hit).transform;

        if (hit != "" && hit != null)
        {
            GameObject a = gameObject.GetComponentsInChildren<Transform>().
                FirstOrDefault(c => c.gameObject.name == hit)?.gameObject;
            if (a)
            {
                var instance = Instantiate(BloodFX, fPos, frot);
                instance.transform.SetParent(a.transform,true);
            }
        }

        //var instance = Instantiate(BloodFX, fPos, frot);
        //var instance = Instantiate(BloodFX, fPos, fRot);
        //var settings = instance.GetComponent<BFX_BloodSettings>();
        //settings.DecalLiveTimeInfinite = false;
        //Destroy(instance, 10);

        /*var nearestBone = GetNearestObject(root, fPos);
        if (nearestBone != null)
        {
            var attachBloodInstance = Instantiate(BloodAttach);
            var bloodT = attachBloodInstance.transform;
            bloodT.position = fPos;
            bloodT.localRotation = Quaternion.identity;
            bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
            bloodT.LookAt(fPos + normal, direction);
            bloodT.Rotate(90, 0, 0);
            bloodT.transform.parent = nearestBone;
            Destroy(attachBloodInstance, 20);
        }*/
    }
}
