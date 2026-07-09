using UnityEngine;
using Mirror;

public class Item : NetworkBehaviour
{
    [SerializeField]
    private int power = 1;
    [SerializeField]
    private int points = 30;
    [SerializeField]
    private float destroyAfter = 5;

    private bool trig;//Para que solo se ejecute una vez por disparo

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ubicacion"))
        {
            other.GetComponentInParent<Player>().PlayerPuntos += points;
            other.GetComponentInParent<DisparoPlayer>().Items++;
            DestroySelf();
        }
    }

    // destroy for everyone on the server
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}
