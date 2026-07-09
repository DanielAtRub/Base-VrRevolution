using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public Color color;

    public bool sphere, wireSphere;
    public float radius;

    public bool cube, wireCube;
    public Vector3 size;

    // Start is called before the first frame update
    void Start()
    {  
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        if (sphere)
            Gizmos.DrawSphere(transform.position, radius);
        if (wireSphere)
            Gizmos.DrawWireSphere(transform.position, radius);
        if (cube)
            Gizmos.DrawCube(transform.position, size);
        if (wireCube)
            Gizmos.DrawWireCube(transform.position, size);
        Gizmos.color = Color.white;
    }

}
