using UnityEngine;

public class GetTarget : MonoBehaviour
{
    public GameObject N_target;

    // Start is called before the first frame update
    void Start()
    {   
    }

    public GameObject myTarget
    {
        get { return N_target; }
        set { N_target = value; }
    }
}
