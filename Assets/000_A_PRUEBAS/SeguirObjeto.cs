using UnityEngine;

public class SeguirObjeto : MonoBehaviour
{
    [Header("El objeto al que este objeto va a seguir")]
    public Transform objetivoASeguir;

    private Vector3 offsetPosicion;
    private Quaternion offsetRotacion;

    void Start()
    {
        if (objetivoASeguir != null)
        {
            // Calculamos la distancia y rotación iniciales relativas al objetivo
            offsetPosicion = objetivoASeguir.InverseTransformPoint(transform.position);
            offsetRotacion = Quaternion.Inverse(objetivoASeguir.rotation) * transform.rotation;
        }
    }

    void LateUpdate()
    {
        if (objetivoASeguir != null)
        {
            // Aplicamos la posición y rotación manteniendo el offset inicial
            transform.position = objetivoASeguir.TransformPoint(offsetPosicion);
            transform.rotation = objetivoASeguir.rotation * offsetRotacion;
        }
    }
}