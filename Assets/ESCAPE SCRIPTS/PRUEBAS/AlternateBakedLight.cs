using UnityEngine;

public class AlternateBakedLight : MonoBehaviour
{
    private MeshRenderer rend;
    [SerializeField]
    private int LightMapIndex;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        ChangeLightState(LightMapIndex);
    }

    void ChangeLightState(int Value)
    {
        rend.lightmapIndex = Value;
    }

    private void Update()
    {
        rend.lightmapIndex = LightMapIndex;
    }
}
  