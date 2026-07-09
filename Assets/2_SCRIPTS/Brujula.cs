using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brujula : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            target = GameObject.FindWithTag("TargetBrujula");
        else
        {
            var lookDir = target.transform.position;
            lookDir.y = transform.position.y;
            transform.LookAt(lookDir);
            if (!target.activeInHierarchy)
                target = null;
        }
    }
}
