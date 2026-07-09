using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutIn : MonoBehaviour
{
    [SerializeField]
    private GameObject Obj;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void FadeIn()
    {
        StartCoroutine(_FadeIn());
    }

    public void FadeOut()
    {
        Renderer rend = Obj.transform.GetComponent<Renderer>();
        Color initialColor = rend.material.color;
        rend.material.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
    }

    private IEnumerator _FadeIn()
    {
        Renderer rend = Obj.transform.GetComponent<Renderer>();
        Color initialColor = rend.material.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            rend.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / 1f);
            yield return null;
        }
    }
    
}
