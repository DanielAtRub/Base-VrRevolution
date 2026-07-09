using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesAlarma : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        StartCoroutine(DesactSelf());
    }

    IEnumerator DesactSelf()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}

