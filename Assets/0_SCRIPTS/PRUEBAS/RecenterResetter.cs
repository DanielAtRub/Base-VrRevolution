using UnityEngine;
using TMPro;

// This behaviour disables 're-centering' on the Oculus Quest,
// instead forcing the camera origin to remain centered and
// facing the same direction within the Guardian boundaries, 
// even between app restarts.
public class RecenterResetter : MonoBehaviour
{
    public OVRCameraRig CameraRig = null;

    //PRUEBAS
    public Transform CERO, POSGUARDADA, ROTGUARDADA; 
    public TextMeshPro debug1, debug2, debug3, debug4;
    public GameObject CentroVirtual, AZUL, ROJO;
    private float Y;

    public enum FacingEdge
    {
        Unspecified,
        LongEdge,
        ShortEdge
    }

    private Quaternion rotationQuat;

    [Tooltip("Specifies whether the 'Forward' (+Z) direction of the camera origin should be facing a Long or Short edge of the rectangular Guardian Play Area.")]
    public FacingEdge Facing = FacingEdge.Unspecified;

    public float RotationOffset { get; private set; } = 0.0f;
    public Vector3 CenterOffset { get; private set; } = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        POSGUARDADA.position = new Vector3 (PlayerPrefs.GetFloat("PosX"), 0.0f, PlayerPrefs.GetFloat("PosZ"));
        //ROTGUARDADA.rotation = Quaternion.Euler(0.0f, PlayerPrefs.GetFloat("RotY"), 0.0f);
        Y = PlayerPrefs.GetFloat("RotY");
        ResetRecenter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Floor(Time.timeSinceLevelLoad) > Mathf.Floor(Time.timeSinceLevelLoad - Time.deltaTime))
        {
            ResetRecenter();
        }

        //PRUEBAS
        if (OVRInput.Get(OVRInput.Button.Two)) //REAJUSTE DE POSICION
        {
            PlayerPrefs.DeleteAll();
        }
        
        if (OVRInput.Get(OVRInput.Button.One)) //GUARDA POS Y ROT
        {
            POSGUARDADA.position = ROJO.transform.position;//POSICIONA POS Y GUARDADA AL RECENTRAR CON EL BOTO A
            PlayerPrefs.SetFloat("PosX", POSGUARDADA.position.x);
            PlayerPrefs.SetFloat("PosZ", POSGUARDADA.position.z);
            //ROTGUARDADA.rotation = ROJO.transform.rotation;//ROTA ROT Y GUARDADA AL RECENTRAR CON EL BOTO A
            //PlayerPrefs.SetFloat("RotY", ROTGUARDADA.rotation.eulerAngles.y);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        {
            Y++;
            PlayerPrefs.SetFloat("RotY", Y);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
        {
            Y--;
            PlayerPrefs.SetFloat("RotY", Y);
        }
        //PRUEBAS
    }

    void ResetRecenter()
    {
        Vector3[] boundaryPoints = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);
        CenterOffset = Vector3.zero;
        Vector3 v;

        for (int i = 0; i < boundaryPoints.Length; ++i)
        {
            v = boundaryPoints[i];
            v.y = 0.0f;

            CenterOffset += v;
        }
        CenterOffset /= boundaryPoints.Length;

        if (boundaryPoints.Length > 3)
        {
            float firstLineLength = (boundaryPoints[1] - boundaryPoints[0]).magnitude;
            float secondLineLength = (boundaryPoints[2] - boundaryPoints[1]).magnitude;

            Vector3 firstLineNormal = Vector3.Cross((boundaryPoints[1] - boundaryPoints[0]).normalized, Vector3.up).normalized;
            float rotationOffset = (Mathf.Atan2(firstLineNormal.x, firstLineNormal.z) * Mathf.Rad2Deg) - 90.0f;

            if (Facing == FacingEdge.LongEdge && firstLineLength > secondLineLength
                || Facing == FacingEdge.ShortEdge && secondLineLength > firstLineLength)
            {
                rotationOffset += 90.0f;
            }

            rotationQuat = Quaternion.Euler(0.0f, rotationOffset, 0.0f);

            if (CameraRig != null)
            {
                CameraRig.trackingSpace.localPosition = Quaternion.Inverse(rotationQuat) * (-CenterOffset);
                CameraRig.trackingSpace.localPosition += -POSGUARDADA.position;
                CameraRig.trackingSpace.localRotation = Quaternion.Inverse(rotationQuat);
                CameraRig.trackingSpace.RotateAround(Vector3.zero,Vector3.up, Y);

                debug1.text = "CameraRig localPosition: " + CameraRig.trackingSpace.localPosition.ToString();
                debug2.text = "ROTACION ROJO: " + ROJO.transform.rotation.eulerAngles.y.ToString();
                debug3.text = "CameraRig localRotation: " + CameraRig.trackingSpace.localRotation.ToString();
                debug4.text = "ROTGUARDADA Y: " + ROTGUARDADA.rotation.eulerAngles.y.ToString();
                //AZUL.transform.position = Quaternion.Inverse(rotationQuat) * (-CenterOffset);
            }
        }
    }
}