using UnityEngine;
using TMPro;

public class MsjAutoDesMuerte : MonoBehaviour
{
    //[SerializeField]
    //private Camera camara;
    //[SerializeField]
    //private LayerMask mascaraTODO;
    [SerializeField]
    private TextMeshProUGUI textTiempo;
    [SerializeField]
    private float tiempo;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        tiempo = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;
        if (tiempo <= 0)
            tiempo = 0;
        int tp = (int)tiempo;
        textTiempo.text = tp.ToString();
    }
}
