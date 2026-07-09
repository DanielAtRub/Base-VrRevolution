using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MsjRecargando : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textTiempo;
    private float tiempo;
    public float ValorIniRef;
    [SerializeField]
    private Image imagen;

    [SerializeField]
    private AudioSource SonidoRifle, SonidoSub, SonidoEscopeta;

    // Start is called before the first frame update
    void Start()
    {
        imagen = imagen.GetComponent<Image>();
    }
    
    void OnEnable()
    {
        //SONIDOS DE RECARGA
        if (ValorIniRef == 1)
        {
            tiempo = 1f;
            SonidoRifle.Play();
        }
        if (ValorIniRef == 2)
        {
            tiempo = 2f;
            SonidoSub.Play();
        }
        if (ValorIniRef == 4)
        {
            tiempo = 0.5f;
            SonidoEscopeta.Play();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;
        if (tiempo <= 0)
        {
            tiempo = 0;
            GetComponentInParent<DisparoHitPlayer>().Recargar();
            gameObject.SetActive(false);
        }
        int tp = (int)tiempo;
        textTiempo.text = tp.ToString();
        //Circulo Level
        imagen.fillAmount = tiempo / ValorIniRef;
    }

}
