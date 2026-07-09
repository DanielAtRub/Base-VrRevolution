using UnityEngine;

public class SpawnEffect : MonoBehaviour {

    [SerializeField]
    private float spawnEffectTime = 2;
    //[SerializeField]
    //private float pause = 0;
    [SerializeField]
    private AnimationCurve fadeIn;

    ParticleSystem ps;
    float timer = 0;
    Renderer _renderer;

    int shaderProperty;

    //[SerializeField]
    //private GameObject Original;

	void OnEnable ()
    {
        timer = 0;

        shaderProperty = Shader.PropertyToID("_cutoff");
        _renderer = GetComponent<Renderer>();
        ps = GetComponentInChildren <ParticleSystem>();

        var main = ps.main;
        main.duration = spawnEffectTime;

        ps.Play();
        //Original.SetActive(false);
    }
	
	void Update ()
    {
        if (timer < spawnEffectTime)
        {
            timer += Time.deltaTime;
        }/*
        else //FINALIZA
        {
            ps.Play();
            timer = 0;
        }
        */
        _renderer.material.SetFloat(shaderProperty, fadeIn.Evaluate( Mathf.InverseLerp(0, 
            spawnEffectTime, timer)));
    }

}
