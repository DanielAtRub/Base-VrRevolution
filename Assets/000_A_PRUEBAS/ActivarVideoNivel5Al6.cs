using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarVideoNivel5Al6 : MonoBehaviour
{

    [SerializeField] private MsjPlayer msjPlayer;

    [SerializeField] private GameObject video;

    private bool videoActivado = false;

    void Update()
    {
        if (msjPlayer != null && msjPlayer.GameManager != null)
        {
            if (msjPlayer.GameManager.GetComponent<JuegoManager>().ActLevel6 && !videoActivado)
            {
                StartCoroutine(RutinaVideo());
            }
        }
    }

    private System.Collections.IEnumerator RutinaVideo()
    {
        videoActivado = true;

        video.SetActive(true);

        yield return new WaitForSeconds(50f);

        video.SetActive(false);
    }
}
