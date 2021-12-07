using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Jogador;

public class SCPT_Inimigo : MonoBehaviour
{
    public enum VARIANTE { VAR1, VAR2, VAR3 }
    public VARIANTE varianteInimigo;

    [Header ("VARIÁVEIS MODIFICÁVEIS")]
    [SerializeField] private float tempoDelayParaAtirar;
    public float vidaMaxima;
    [SerializeField] private float visaoDist;
    public bool consegueSeMover;
    [SerializeField] private Transform[] pontosDePatrulha;
    [SerializeField] private float tempoDelayAoChegarNoPonto1;
    [SerializeField] private float tempoDelayAoChegarNoPonto2;
    [SerializeField] private float tempoDelayAoChegarNoPonto3;
    [SerializeField] private float tempoDelayAoChegarNoPonto4;
    [SerializeField] private float tempoDelayAoChegarNoPonto5;
    [SerializeField] private float tempoDelayAoChegarNoPonto6;
    [SerializeField] private float tempoDelayAoChegarNoPonto7;
    [SerializeField] private float tempoDelayAoChegarNoPonto8;
    public LayerMask layerIgnora;

    [Header("VARIÁVEIS PARA NÃO MEXER")]
    //[SerializeField] private SCPT_VisaoInimigo visaoInimigo;
    [SerializeField] private float tempoDelayAoChegarNoPonto;
    [SerializeField] private NavMeshAgent agenteDaNaveMesh;
    [SerializeField] private Transform jogador;
    [SerializeField] private int pontoAtual;
    [SerializeField] private float tempoPassadoParaAndar;
    private float tempoPassadoParaAtirar;
    [SerializeField] private Transform spawnTiro;
    [SerializeField] private GameObject tiro;
    public AudioSource somInimigo;
    [SerializeField] private AudioClip somTiro;
    [SerializeField] private GameObject medidor;
    [SerializeField] private ParticleSystem sangue;
    public ParticleSystem sangueCorpo;
    public ParticleSystem sanguePe;
    [SerializeField] private List<AudioClip> listaFalasInimigos;
    [SerializeField] private float tempoPassadoFalaInimigos;
    [SerializeField] private float tempoDelayFalasInimigos;
    private Animator inimigoAnim;
    public bool tomouHS;
    public bool spawnado;

    public Transform pontoVisao;

    public float velRotacao;

    private void Awake()
    {
        inimigoAnim = GetComponent<Animator>();
        agenteDaNaveMesh = GetComponent<NavMeshAgent>();
        tempoDelayFalasInimigos = Random.Range(10f, 500f);
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        pontoAtual = 0;
        if(consegueSeMover == true){
            agenteDaNaveMesh.SetDestination(pontosDePatrulha[pontoAtual].position);
        }
    }

    void Update()
    {
        VisaoInimigo();

        switch (pontoAtual)
        {
            case 0:
                tempoDelayAoChegarNoPonto =  tempoDelayAoChegarNoPonto1;
            break;
            case 1:
                tempoDelayAoChegarNoPonto =  tempoDelayAoChegarNoPonto2;
            break;
            case 2:
                tempoDelayAoChegarNoPonto =  tempoDelayAoChegarNoPonto3;
            break;
            case 3:
                tempoDelayAoChegarNoPonto =  tempoDelayAoChegarNoPonto4;
            break;
            case 4:
                tempoDelayAoChegarNoPonto =  tempoDelayAoChegarNoPonto5;
            break;
            case 5:
                tempoDelayAoChegarNoPonto =  tempoDelayAoChegarNoPonto6;
            break;
            case 6:
                tempoDelayAoChegarNoPonto =  tempoDelayAoChegarNoPonto7;
            break;
            case 7:
                tempoDelayAoChegarNoPonto =  tempoDelayAoChegarNoPonto8;
            break;
        }

        if(spawnado == true){
            agenteDaNaveMesh.SetDestination(jogador.position);
            if(varianteInimigo == VARIANTE.VAR1)
            {
                inimigoAnim.SetBool("Correndo", true);
            }
            if (varianteInimigo == VARIANTE.VAR2)
            {
                inimigoAnim.SetBool("Correndo", true);
            }
            if (varianteInimigo == VARIANTE.VAR3)
            {
                inimigoAnim.SetBool("Correndo", true);
            }
        }

        Quaternion rotacao = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        pontoVisao.rotation = Quaternion.Lerp(pontoVisao.rotation, rotacao, velRotacao * Time.deltaTime);

        if(Vector3.Distance(transform.position, jogador.position) < 10){
            Ray raio = new Ray(transform.position, jogador.position - transform.position);
            RaycastHit hit;
            if (Physics.Raycast(raio, out hit, 10)){
                if (Vector3.Distance(this.transform.position, jogador.transform.position) <= 3){
                    agenteDaNaveMesh.SetDestination(jogador.position);
                    if(varianteInimigo == VARIANTE.VAR1)
                    {
                        inimigoAnim.SetBool("Correndo", true);
                    }
                    if (varianteInimigo == VARIANTE.VAR2)
                    {
                        inimigoAnim.SetBool("Correndo", true);
                    }
                    if (varianteInimigo == VARIANTE.VAR3)
                    {
                        inimigoAnim.SetBool("Correndo", true);
                    }
                }
            }
        }else{
            if(consegueSeMover == true){
                if(agenteDaNaveMesh.remainingDistance < 0.5f){
                    tempoPassadoParaAndar += Time.deltaTime;
                    
                    if (varianteInimigo == VARIANTE.VAR1)
                    {
                        inimigoAnim.SetBool("Correndo", false);
                    }
                    if (varianteInimigo == VARIANTE.VAR2)
                    {
                        inimigoAnim.SetBool("Correndo", false);
                    }
                    if (varianteInimigo == VARIANTE.VAR3)
                    {
                        inimigoAnim.SetBool("Correndo",false);
                    }

                    if(tempoDelayAoChegarNoPonto <= tempoPassadoParaAndar){
                        pontoAtual++;
                        tempoPassadoParaAndar = 0;
                        if(pontoAtual >= pontosDePatrulha.Length){
                            pontoAtual = 0;
                        }
                        agenteDaNaveMesh.SetDestination(pontosDePatrulha[pontoAtual].position);
                        if (varianteInimigo == VARIANTE.VAR1)
                        {
                            inimigoAnim.SetBool("Correndo", true);
                        }
                        if (varianteInimigo == VARIANTE.VAR2)
                        {
                            inimigoAnim.SetBool("Correndo", true);
                        }
                        if (varianteInimigo == VARIANTE.VAR3)
                        {
                            inimigoAnim.SetBool("Correndo", true);
                        }
                    }
                }
            }
        }

        if(vidaMaxima <= 0)
        {
            sangue.Play();
            if(tomouHS == true)
            {
                jogador.gameObject.GetComponent<SCPT_ArmasDoJogador>().EliminouInimigoComHS();
            }
            else
            {
                jogador.gameObject.GetComponent<SCPT_ArmasDoJogador>().EliminouInimigo();
            }
            Destroy(gameObject, 0.5f);
            //Fazer ele ficar em ragdoll e tirar o Destroy
        }
        
        tempoPassadoFalaInimigos += Time.deltaTime;
        if(tempoPassadoFalaInimigos >= tempoDelayFalasInimigos){
            InimigoFalou();
        }
    }

    IEnumerator AtirarNoPlayer()
    {
        if (varianteInimigo == VARIANTE.VAR1)
        {
            inimigoAnim.SetBool("Atirando", true);
        }
        if (varianteInimigo == VARIANTE.VAR2)
        {
            inimigoAnim.SetBool("Atirando", true);
        }
        if (varianteInimigo == VARIANTE.VAR3)
        {
            inimigoAnim.SetBool("Atirando", true);
        }

        somInimigo.PlayOneShot(somTiro);
        tiro.transform.position = new Vector3(0, 0, 0);
        Vector3 posicaoProjetil = spawnTiro.position;
        Quaternion rotacaoProjetil = Quaternion.FromToRotation(Vector3.zero, spawnTiro.forward);
        GameObject clone = Instantiate(tiro, posicaoProjetil, rotacaoProjetil);
        clone.GetComponent<Rigidbody>().AddForce(spawnTiro.forward * 1000);
        Debug.Log("Jogador Avistado");
        tempoPassadoParaAtirar = 0;
        yield return null;
    }

    void VisaoInimigo()
    {
        RaycastHit m_hitVisao;

        Debug.DrawRay(pontoVisao.position, pontoVisao.forward * visaoDist, Color.blue);
        Debug.DrawRay(pontoVisao.position, (pontoVisao.forward + pontoVisao.right) * visaoDist, Color.blue);
        Debug.DrawRay(pontoVisao.position, (pontoVisao.forward - pontoVisao.right) * visaoDist, Color.blue);

        if (Physics.Raycast(pontoVisao.position, pontoVisao.forward, out m_hitVisao, visaoDist, ~layerIgnora))
        {
            if (m_hitVisao.transform.tag == "Player")
            {
                tempoPassadoParaAtirar += Time.deltaTime;
                if (tempoDelayParaAtirar <= tempoPassadoParaAtirar)
                {
                    medidor.GetComponent<SCPT_Medidor>().nivelMedidor += 1;
                    StartCoroutine(AtirarNoPlayer());
                }
            }
        }

        if (Physics.Raycast(pontoVisao.position, (pontoVisao.forward + pontoVisao.right).normalized, out m_hitVisao, visaoDist, ~layerIgnora))
        {
            if (m_hitVisao.transform.tag == "Player")
            {
                tempoPassadoParaAtirar += Time.deltaTime;
                if (tempoDelayParaAtirar <= tempoPassadoParaAtirar)
                {
                    medidor.GetComponent<SCPT_Medidor>().nivelMedidor += 1;
                    StartCoroutine(AtirarNoPlayer());
                }
            }
        }

        if (Physics.Raycast(pontoVisao.position, (pontoVisao.forward - pontoVisao.right).normalized, out m_hitVisao, visaoDist, ~layerIgnora))
        {
            if (m_hitVisao.transform.tag == "Player")
            {
                tempoPassadoParaAtirar += Time.deltaTime;
                if (tempoDelayParaAtirar <= tempoPassadoParaAtirar)
                {
                    medidor.GetComponent<SCPT_Medidor>().nivelMedidor += 1;
                    StartCoroutine(AtirarNoPlayer());
                }
            }
        }
    }

    void InimigoParaDeAtirar()
    {
        if (varianteInimigo == VARIANTE.VAR1)
        {
            inimigoAnim.SetBool("Atirando", false);
        }
        if (varianteInimigo == VARIANTE.VAR2)
        {
            inimigoAnim.SetBool("Atirando", false);
        }
        if (varianteInimigo == VARIANTE.VAR3)
        {
            inimigoAnim.SetBool("Atirando", false);
        }
    }

    void InimigoFalou(){
        tempoPassadoFalaInimigos = 0;
        tempoDelayFalasInimigos = Random.Range(10f, 300f);
        somInimigo.PlayOneShot(listaFalasInimigos[Random.Range(0, listaFalasInimigos.Count)]);
    }
}