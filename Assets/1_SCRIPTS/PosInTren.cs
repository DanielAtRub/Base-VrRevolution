using UnityEngine;

public class PosInTren : MonoBehaviour
{
    [SerializeField]
    private Transform Plataf;
    [SerializeField]
    private Transform Tren;
    [SerializeField]
    private float offsetY;
    //private float PosPlatforFinalY;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        //PosPlatforFinalY = Plataf.position.y + offsetY;
    }

    // Update is called once per frame
    void Update()
    {
        Plataf.position = new Vector3(Tren.position.x, Tren.position.y + offsetY, Tren.position.z);
        Plataf.rotation = Tren.rotation;
    }
}
