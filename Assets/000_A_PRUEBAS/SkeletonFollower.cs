using UnityEngine;
using RootMotion.FinalIK;

[RequireComponent(typeof(VRIK))]
[DefaultExecutionOrder(200)]
public class SkeletonFollower : MonoBehaviour
{
    [Tooltip("El componente VRIK del modelo maestro.")]
    public VRIK masterVRIK;

    [Tooltip("El Animator del modelo maestro (necesario para encontrar los huesos de los pies).")]
    public Animator masterAnimator;

    [Tooltip("Sincronizar la posicion del GameObject completo?")]
    public bool syncRoot = true;

    private VRIK slaveVRIK;

    void Start()
    {
        slaveVRIK = GetComponent<VRIK>();

        if (masterVRIK == null || masterAnimator == null)
        {
            Debug.LogError("Faltan referencias del maestro en el script VRIKSharedTargets.");
            return;
        }

        // Compartir los Targets de la parte superior (Visor y Mandos)
        slaveVRIK.solver.spine.headTarget = masterVRIK.solver.spine.headTarget;
        slaveVRIK.solver.spine.pelvisTarget = masterVRIK.solver.spine.pelvisTarget;
        slaveVRIK.solver.leftArm.target = masterVRIK.solver.leftArm.target;
        slaveVRIK.solver.rightArm.target = masterVRIK.solver.rightArm.target;

        // Asignar los pies reales del maestro como targets del esclavo
        Transform masterLeftFoot = masterAnimator.GetBoneTransform(HumanBodyBones.LeftFoot);
        Transform masterRightFoot = masterAnimator.GetBoneTransform(HumanBodyBones.RightFoot);

        if (masterLeftFoot != null && masterRightFoot != null)
        {
            //slaveVRIK.solver.leftLeg.target = masterLeftFoot;
            //slaveVRIK.solver.rightLeg.target = masterRightFoot;

            // Apagamos la locomoción automática del esclavo.
            // Ahora las piernas se moverán puramente por el IK siguiendo a los pies del maestro.
            slaveVRIK.solver.locomotion.weight = 0f;
        }
        else
        {
            Debug.LogError("No se pudieron encontrar los huesos de los pies en el Animator del maestro. Asegúrate de que esté configurado como Humanoid.");
        }

        CopySolverWeights();
    }

    void LateUpdate()
    {
        if (syncRoot && masterVRIK != null)
        {
            transform.position = masterVRIK.transform.position;
            transform.rotation = masterVRIK.transform.rotation;
        }
    }

    private void CopySolverWeights()
    {
        slaveVRIK.solver.spine.positionWeight = masterVRIK.solver.spine.positionWeight;
        slaveVRIK.solver.spine.rotationWeight = masterVRIK.solver.spine.rotationWeight;

        slaveVRIK.solver.leftArm.positionWeight = masterVRIK.solver.leftArm.positionWeight;
        slaveVRIK.solver.leftArm.rotationWeight = masterVRIK.solver.leftArm.rotationWeight;

        slaveVRIK.solver.rightArm.positionWeight = masterVRIK.solver.rightArm.positionWeight;
        slaveVRIK.solver.rightArm.rotationWeight = masterVRIK.solver.rightArm.rotationWeight;

        // Le damos total prioridad al seguimiento de los pies asignados
        slaveVRIK.solver.leftLeg.positionWeight = 1f;
        slaveVRIK.solver.leftLeg.rotationWeight = 1f;
        slaveVRIK.solver.rightLeg.positionWeight = 1f;
        slaveVRIK.solver.rightLeg.rotationWeight = 1f;
    }
}