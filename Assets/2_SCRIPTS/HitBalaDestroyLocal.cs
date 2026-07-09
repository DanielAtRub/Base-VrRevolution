using UnityEngine;
//using Mirror;

public class HitBalaDestroyLocal : MonoBehaviour
{
    [SerializeField]
    private float destroyAfter = 10;
    //public Transform hit;

    // se ejecuta cuando aparece este objeto en el servidor
    void Start()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // destroy for everyone on the serve
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
