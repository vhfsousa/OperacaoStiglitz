using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jogador;

public class SCPT_Granada : MonoBehaviour
{
    public GameObject granada;

    public float tempoMax = 3f;

    public ParticleSystem explosionEffect;

    public float raio = 5f;
    public float forcaExplosao = 700f;

    float tempo;

    bool explodiu;
    bool podeTocar = true;

    [SerializeField] private AudioClip somExplosao;
    AudioSource audioMaster;

    void Start()
    {
        audioMaster = GetComponent<AudioSource>();
    }

    void Update()
    {
        tempo += Time.deltaTime;

        if (tempo >= tempoMax && !explodiu)
        {
            StartCoroutine(Explosao());
            explodiu = false;
        }
    }

    IEnumerator Explosao()
    {
        //Ativar a particula de explosao
        
        explosionEffect.Play();

        if (podeTocar)
        {
            audioMaster.PlayOneShot(somExplosao);
            podeTocar = false;
        }

        Destroy(granada);
        Collider[] colliders = Physics.OverlapSphere(transform.position, raio);

        foreach (Collider objetosPerto in colliders)
        {
            Rigidbody rb = objetosPerto.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(forcaExplosao, transform.position, raio);

                var tempInimigo = rb.GetComponent<SCPT_Inimigo>();

                if (tempInimigo != null)
                {
                    tempInimigo.vidaMaxima -= 100f;
                }
            }
        }

        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
