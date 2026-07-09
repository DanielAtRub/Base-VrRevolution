using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DESACTIVA LA TAPA FRENTE LA CAMARA DEL JUGADOR
public class Des : MonoBehaviour
{
    [SerializeField]
    private float desactiveAfter;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        Invoke(nameof(DesactiveSelf), desactiveAfter);
    }

    void DesactiveSelf()
    {
        gameObject.SetActive(false);
    }
}
