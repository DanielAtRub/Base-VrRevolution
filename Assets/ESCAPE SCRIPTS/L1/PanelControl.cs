using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using NodeCanvas.BehaviourTrees;

public class PanelControl : NetworkBehaviour
{
    public bool Armario = false;

    [SyncVar]
    public bool CodigoOK;

    [SerializeField]
    private int p;
    [SerializeField]
    private TextMeshPro pantalla_1, pantalla_2, pantalla_3, pantalla_4;
    [SerializeField]
    private GameObject ManagerL1;
    [SerializeField]
    private GameObject HangarBehaviour, PuertaHangar;

    [Header("Referencias a las puertas")]
    public Transform puerta1;
    public Transform puerta2;

    private void Update()
    {
        if (Armario && pantalla_1.text == "6" && pantalla_2.text == "6" && pantalla_3.text == "3" && pantalla_4.text == "8")
        {
            CodigoOK = true;
            Armario = false; // Evitamos que entre aquí otra vez

            // Iniciamos la animación por código
            StartCoroutine(AbrirPuertasCoroutine());
        }
    }

    private System.Collections.IEnumerator AbrirPuertasCoroutine()
    {
        float duracion = 2.0f; // Los 2 segundos que quieres que tarde
        float tiempoPasado = 0f;

        // Guardamos las rotaciones iniciales
        Quaternion rotInicial1 = puerta1.localRotation;
        Quaternion rotInicial2 = puerta2.localRotation;

        // Definimos las rotaciones finales (90 y -90 en Y)
        // Nota: Revisa si tus puertas rotan en Y o en otro eje, cámbialo si es necesario.
        Quaternion rotFinal1 = Quaternion.Euler(0, 90, 0);
        Quaternion rotFinal2 = Quaternion.Euler(0, -90, 0);

        while (tiempoPasado < duracion)
        {
            // Aumentamos el tiempo basado en los frames
            tiempoPasado += Time.deltaTime;

            // Calculamos el porcentaje de avance de 0 a 1
            float t = tiempoPasado / duracion;

            // Slerp hace una rotación suave entre el punto A y el B
            puerta1.localRotation = Quaternion.Slerp(rotInicial1, rotFinal1, t);
            puerta2.localRotation = Quaternion.Slerp(rotInicial2, rotFinal2, t);

            yield return null; // Esperamos al siguiente frame
        }

        // Para asegurarnos de que quede EXACTAMENTE en 90 y -90 al final
        puerta1.localRotation = rotFinal1;
        puerta2.localRotation = rotFinal2;
    }

    [Server]
    public void reset()
    {
        p = 0;
        pantalla_1.text = "0";
        pantalla_2.text = "0";
        pantalla_3.text = "0";
        pantalla_4.text = "0";

        RpcPantalla1("0");
        RpcPantalla2("0");
        RpcPantalla3("0");
        RpcPantalla4("0");
    }
    [Server]
    public void n_1()
    {
        p++;
        if (p == 1)
        {
            pantalla_1.text = "1";
            RpcPantalla1("1");
        }
        if (p == 2)
        {
            pantalla_2.text = "1";
            RpcPantalla2("1");
        }
        if (p == 3)
        {
            pantalla_3.text = "1";
            RpcPantalla3("1");
        }
        if (p == 4)
        {
            pantalla_4.text = "1";
            RpcPantalla4("1");
        }
        comprobacion();
    }
    [Server]
    public void n_2()
    {
        p++;
        if (p == 1)
        {
            pantalla_1.text = "2";
            RpcPantalla1("2");
        }
        if (p == 2)
        {
            pantalla_2.text = "2";
            RpcPantalla2("2");
        }
        if (p == 3)
        {
            pantalla_3.text = "2";
            RpcPantalla3("2");
        }
        if (p == 4)
        {
            pantalla_4.text = "2";
            RpcPantalla4("2");
        }
        comprobacion();
    }
    [Server]
    public void n_3()
    {
        p++;
        if (p == 1)
        {
            pantalla_1.text = "3";
            RpcPantalla1("3");
        }
        if (p == 2)
        {
            pantalla_2.text = "3";
            RpcPantalla2("3");
        }
        if (p == 3)
        {
            pantalla_3.text = "3";
            RpcPantalla3("3");
        }
        if (p == 4)
        {
            pantalla_4.text = "3";
            RpcPantalla4("3");
        }
        comprobacion();
    }
    [Server]
    public void n_4()
    {
        p++;
        if (p == 1)
        {
            pantalla_1.text = "4";
            RpcPantalla1("4");
        }
        if (p == 2)
        {
            pantalla_2.text = "4";
            RpcPantalla2("4");
        }
        if (p == 3)
        {
            pantalla_3.text = "4";
            RpcPantalla3("4");
        }
        if (p == 4)
        {
            pantalla_4.text = "4";
            RpcPantalla4("4");
        }
        comprobacion();
    }
    [Server]
    public void n_5()
    {
        p++;
        if (p == 1)
        {
            pantalla_1.text = "5";
            RpcPantalla1("5");
        }
        if (p == 2)
        {
            pantalla_2.text = "5";
            RpcPantalla2("5");
        }
        if (p == 3)
        {
            pantalla_3.text = "5";
            RpcPantalla3("5");
        }
        if (p == 4)
        {
            pantalla_4.text = "5";
            RpcPantalla4("5");
        }
        comprobacion();
    }
    [Server]
    public void n_6()
    {
        p++;
        if (p == 1)
        {
            pantalla_1.text = "6";
            RpcPantalla1("6");
        }
        if (p == 2)
        {
            pantalla_2.text = "6";
            RpcPantalla2("6");
        }
        if (p == 3)
        {
            pantalla_3.text = "6";
            RpcPantalla3("6");
        }
        if (p == 4)
        {
            pantalla_4.text = "6";
            RpcPantalla4("6");
        }
        comprobacion();
    }
    [Server]
    public void n_7()
    {
        p++;
        if (p == 1)
        {
            pantalla_1.text = "7";
            RpcPantalla1("7");
        }
        if (p == 2)
        {
            pantalla_2.text = "7";
            RpcPantalla2("7");
        }
        if (p == 3)
        {
            pantalla_3.text = "7";
            RpcPantalla3("7");
        }
        if (p == 4)
        {
            pantalla_4.text = "7";
            RpcPantalla4("7");
        }
        comprobacion();
    }
    [Server]
    public void n_8()
    {
        p++;
        if (p == 1)
        {
            pantalla_1.text = "8";
            RpcPantalla1("8");
        }
        if (p == 2)
        {
            pantalla_2.text = "8";
            RpcPantalla2("8");
        }
        if (p == 3)
        {
            pantalla_3.text = "8";
            RpcPantalla3("8");
        }
        if (p == 4)
        {
            pantalla_4.text = "8";
            RpcPantalla4("8");
        }
        comprobacion();
    }
    [Server]
    public void n_9()
    {
        p++;
        if (p == 1)
        {
            pantalla_1.text = "9";
            RpcPantalla1("9");
        }
        if (p == 2)
        {
            pantalla_2.text = "9";
            RpcPantalla2("9");
        }
        if (p == 3)
        {
            pantalla_3.text = "9";
            RpcPantalla3("9");
        }
        if (p == 4)
        {
            pantalla_4.text = "9";
            RpcPantalla4("9");
        }
        comprobacion();
    }
    [Server]
    private void comprobacion()
    {
        // codigo 1234
        if (!Armario && pantalla_1.text == "2" && pantalla_2.text == "5" && pantalla_3.text == "1" && pantalla_4.text == "7")
        {
            CodigoOK = true;
            PuertaHangar.GetComponent<PuertaBunker>().AbrePuerta();
            HangarBehaviour.GetComponent<BehaviourTreeOwner>().enabled = true;
            ManagerL1.GetComponent<Manager_L1>().Angar_Abierto_OK = true;
        }

        
        if (Armario && pantalla_1.text == "6" && pantalla_2.text == "6" && pantalla_3.text == "3" && pantalla_4.text == "8")
        {
            CodigoOK = true;
        }
    }

    [ClientRpc]
    void RpcPantalla1(string n)
    {
        pantalla_1.text = n;
    }
    [ClientRpc]
    void RpcPantalla2(string n)
    {
        pantalla_2.text = n;
    }
    [ClientRpc]
    void RpcPantalla3(string n)
    {
        pantalla_3.text = n;
    }
    [ClientRpc]
    void RpcPantalla4(string n)
    {
        pantalla_4.text = n;
    }
}
