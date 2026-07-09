using UnityEngine;

public class PosCol : MonoBehaviour
{
    [SerializeField]
    private Transform Col;
    [SerializeField]
    private Transform Spine;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Col.position = Spine.position;
        Col.rotation = Spine.rotation;
    }
}
