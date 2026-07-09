using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotar : MonoBehaviour
{
    [SerializeField]
    private float speedRot = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rot = Time.deltaTime * speedRot;
        transform.Rotate(Vector3.forward * rot);

        //transform.Rotate(new Vector3(0f, rot, 0f));
    }
}
