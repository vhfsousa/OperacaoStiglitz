using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPT_PedraControl : MonoBehaviour
{
    public float tempo;

    public bool chao;

    void Start()
    {
    }

    void Update()
    {
        tempo += Time.deltaTime;
        if(tempo >= 2f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Chao")
        {
            Debug.Log("chão");
            chao = true;
        }
    }
}
