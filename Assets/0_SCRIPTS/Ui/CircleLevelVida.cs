using UnityEngine;
using UnityEngine.UI;

public class CircleLevelVida : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private float ValorIniRef;
    private Image imagen;

    // Start is called before the first frame update
    void Start()
    {
        imagen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        imagen.fillAmount = Player.GetComponent<Player>().PlayerHealth / ValorIniRef;
    }
}
