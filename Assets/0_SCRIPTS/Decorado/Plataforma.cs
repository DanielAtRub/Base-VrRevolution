using UnityEngine;

public class Plataforma : MonoBehaviour
{
    [SerializeField]
    private Transform Player;
    public Transform plataforma;
    [SerializeField]
    private bool sobrePlataforma;

    // Start is called before the first frame update
    void Start()
    {
        if (!plataforma)
        {
            if (GameObject.Find("Plataforma"))
            {
                sobrePlataforma = true;
                plataforma = GameObject.Find("Plataforma").transform;
                Player.position += plataforma.position; 
                Player.parent = plataforma;
            }
        }
    }
    
    void Update() 
    {
        if (plataforma && !Player.GetComponent<Caida>().cayendo) //SI HAY CAIDA NO SE EJECUTA
        {
            Player.position = new Vector3(Player.position.x, plataforma.position.y, Player.position.z);
        }
    }
    
}
