using TMPro;
using UnityEngine;

public class ScoreAllPlayers : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] P; //PUNTOS
    [SerializeField]
    private TextMeshProUGUI[] J; //JUGADOR
    [SerializeField]
    private GameObject[] Players;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Players = GameObject.FindGameObjectsWithTag("Jugador");
        if (Players != null)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                J[i].text = Players[i].GetComponent<Player>().Name;
                P[i].text = Players[i].GetComponent<Player>().PlayerPuntos.ToString();
            }
        }
    }
}
