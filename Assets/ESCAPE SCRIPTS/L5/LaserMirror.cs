using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LaserMirror : NetworkBehaviour
{
    [SerializeField]
    private bool SoyPrincipal;

    RaycastHit hit;

    [SerializeField]
    private Transform Laser;

    [SerializeField]
    private LineRenderer laserLineRenderer;
    [SerializeField]
    private float laserMaxLength = 5f;

    [SerializeField]
    private GameObject ObjetoHit;

    [SerializeField]
    private GameObject Padre;
    [SerializeField]
    private float AnguloMio; //ANGULO SALIDA ENVIADO AL HIT
    public float AnguloEntrada; //DATO ENVIADO POR EL EMISOR
    public bool ActivaRayo; //DATO ENVIADO POR EL EMISOR

    [SerializeField]
    private float AnguloSalida; //ANGULO SALIDA ENVIADO AL HIT

    [SerializeField]
    private bool offset;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AnguloMio = Padre.transform.localEulerAngles.x;
        if (AnguloMio > 180)
            AnguloMio = AnguloMio - 360;

        if (SoyPrincipal)
            AnguloEntrada = 135f;

        AnguloSalida = (AnguloMio * 2) - AnguloEntrada;
        //AnguloSalida = AnguloMio + Mathf.Abs(AnguloEntrada);

        if (SoyPrincipal)
            Laser.localEulerAngles = new Vector3(0, 0, 0);
        else
            Laser.localEulerAngles = new Vector3(AnguloSalida, 0, 0);

        Vector3 PosicionFinal = DetectHit(transform.position, laserMaxLength, -transform.forward);

        if (ActivaRayo || SoyPrincipal)
        {
            laserLineRenderer.enabled = true;

            laserLineRenderer.SetPosition(0, transform.position);
            laserLineRenderer.SetPosition(1, PosicionFinal);
        }
        else
            laserLineRenderer.enabled = false; 

        ActivaRayo = false; 

        if (ObjetoHit)
        {
            if (ObjetoHit.name == "ESPEJO" && laserLineRenderer.enabled)
            {
                ObjetoHit.GetComponentInChildren<LaserMirror>().ActivaRayo = true;
                ObjetoHit.GetComponentInChildren<LaserMirror>().AnguloEntrada = AnguloSalida;
            }
         
            if (isServer)
            {
                if (ObjetoHit.name == "COL_ESFERA")
                {
                    ObjetoHit.GetComponentInChildren<EsferaRayo>().EsferaRayoActivado = true;
                    ObjetoHit.GetComponentInChildren<EsferaRayo>().Refresh();
                }
            }
        }
    }

    Vector3 DetectHit(Vector3 startPos, float distance, Vector3 direction)
    {
        Ray ray = new Ray(startPos, direction);
        Vector3 endPos = startPos + (distance * direction); 
        ObjetoHit = null;

        RaycastHit[] hits = Physics.RaycastAll(ray, distance);

        float distanciaMasCorta = distance + 1f; 

        foreach (RaycastHit impacto in hits)
        {
            if (impacto.collider.CompareTag("ZonaReset") || impacto.collider.CompareTag("Finish"))
            {
                continue;
            }

            if (impacto.distance < distanciaMasCorta)
            {
                distanciaMasCorta = impacto.distance;
                hit = impacto; 
                endPos = hit.point;
                ObjetoHit = hit.collider.gameObject;
            }
        }
        return endPos;
    }
}
