using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPT_FacaDeArremesso : MonoBehaviour
{
    public float danoFaca;

    void Start()
    {
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "InimigoCabeca" || col.gameObject.tag == "InimigoTronco")
        {
            var inimigo = col.transform.gameObject.GetComponentInParent<SCPT_Inimigo>();

            inimigo.vidaMaxima -= danoFaca* 10;
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "InimigoPernas")
        {
            var inimigo = col.transform.gameObject.GetComponentInParent<SCPT_Inimigo>();

            inimigo.vidaMaxima -= danoFaca;
        }
    }
}
