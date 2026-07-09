using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espejo : MonoBehaviour
{
    public float reflectionAngle;
    public LineRenderer lineRenderer;
    private Espejo nextMirror;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit))
        {
            Vector3 incomingVector = hit.point - transform.position;
            Vector3 normal = hit.normal;
            Vector3 reflectionVector = Vector3.Reflect(incomingVector, normal);

            DrawReflection(hit.point, reflectionVector);

            if (hit.collider.name == "ESPEJO")
            {
                nextMirror = hit.collider.GetComponent<Espejo>();
                if (nextMirror != null)
                {
                    Vector3 nextDirection = Quaternion.AngleAxis(reflectionAngle, transform.forward) * reflectionVector;
                    nextMirror.ReflectRay(hit.point, nextDirection);
                }
            }
        }
        else
        {
            lineRenderer.enabled = false;
            nextMirror = null;
        }
    }

    public void ReflectRay(Vector3 start, Vector3 direction)
    {
        Vector3 normal = -transform.right;
        Vector3 reflectionVector = Vector3.Reflect(direction, normal);
        DrawReflection(start, reflectionVector);

        if (nextMirror != null)
        {
            Vector3 nextDirection = Quaternion.AngleAxis(reflectionAngle, transform.forward) * reflectionVector;
            nextMirror.ReflectRay(start, nextDirection);
        }
    }

    private void DrawReflection(Vector3 start, Vector3 direction)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, start + direction * 100f);
    }
}
