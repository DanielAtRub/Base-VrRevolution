using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    void Start()
    {
    }

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(obj);
    }
}
