using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Jogador;

public class SCPT_ArmasDoJogador : MonoBehaviour
{
    //Enum com as armas do jogador
    public enum ArmasPossiveis { FACA, P08, THOMPSON, MG42, PEDRA, GRANADA }
    public ArmasPossiveis armasDoJogador;

    public LayerMask layerIgnora;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private SCPT_Medidor medidor;
    [SerializeField] private SCPT_Jogador jogador;

    [Header ("BOOLS PARA ATRIBUIR A ARMA")]
    [SerializeField] private bool possuiFaca;
    [SerializeField] private bool possuiP08;
    [SerializeField] private bool possuiThompson;
    [SerializeField] private bool possuiMG42;
    [SerializeField] private bool possuiGranada;

    [Header ("GAMEOBJECTS PARA ADICIONAR DAS ARMAS")]
    [SerializeField] private GameObject balaP08 = null;
    [SerializeField] private GameObject balaThompson  = null;
    [SerializeField] private GameObject balaMG42  = null;
    [SerializeField] private GameObject facaPrefab  = null;
    [SerializeField] private GameObject granadaPrefab  = null;

    [Space]
    [Header("Armas")]
    [SerializeField] private GameObject faca  = null;
    [SerializeField] private GameObject p08  = null;
    [SerializeField] private GameObject thompson  = null;
    [SerializeField] private GameObject mg42  = null;
    [SerializeField] private GameObject granada  = null;

    [Space]
    [Header("Saidas")]
    [SerializeField] private Transform saidaFaca = null;
    [SerializeField] private Transform saidaP08 = null;
    [SerializeField] private Transform saidaThompson = null;
    [SerializeField] private Transform saidaMG42 = null;

    [Space]
    [Header("Danos")]
    [SerializeField] private int danoFaca;
    [SerializeField] private int forcaArremessoFaca;
    [SerializeField] private int forcaArremessoGranada;
    [SerializeField] private int danoP08;
    [SerializeField] private int danoThompson;
    [SerializeField] private int danoMG42;
    [SerializeField] private int danoGranada;

    [Space]
    [Header("Distancia")]
    [SerializeField] private int distanciaP08;
    [SerializeField] private int distanciaThompson;
    [SerializeField] private int distanciaMG42;
    [SerializeField] private int distanciaEsfaquear;

    [Space]
    [Header("Rastros")]
    [SerializeField] private GameObject rastroFaca = null;
    [SerializeField] private GameObject rastroP08 = null;
    [SerializeField] private GameObject rastroThompson = null;
    [SerializeField] private GameObject rastroMG42 = null;

    [Header ("GAMEOBJECTS PARA ADICIONAR DA HUD")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Sprite crosshairPadrao;
    [SerializeField] private Sprite caveiraMorte;
    [SerializeField] private Sprite caveiraMorteHS;
    [SerializeField] private Text municaoNoPente;
    [SerializeField] private Text municaoTotal;
    [SerializeField] private Text granadasText;

    [Header ("GAMEOBJECTS PARA ADICIONAR DE SOM")]
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip somFaca = null;
    [SerializeField] private AudioClip somP08 = null;
    [SerializeField] private AudioClip somThompson = null;
    [SerializeField] private AudioClip somMG42 = null;
    [SerializeField] private AudioClip somRecarregarP08 = null;
    [SerializeField] private AudioClip somRecarregarThompson = null;
    [SerializeField] private AudioClip falhaNaArma = null;
    [SerializeField] private AudioClip inimigoTomandoDano = null;

    [Header ("VARIÁVEIS DAS ARMAS")]
    public int municaoFaca;
    public int municaoP08Pente;
    public int municaoP08Total;
    public int municaoThompsonPente;
    public int municaoThompsonTotal;
    public int municaoMG42Pente;
    public int municaoMG42Total;
    public int municaoGranada;
    [SerializeField] private Vector3 centroTela;

    [HideInInspector] public bool esfaqueou;
    bool atirouThompson;
    bool atirouMG42;
    bool atirouP08;

    void Start()
    {
        centroTela = new Vector3 (Screen.width/2, Screen.height/2);

        PlayerPrefs.SetInt("possuiP08", 1);

        if(PlayerPrefs.GetInt("possuiP08") == 0)
        {
            possuiP08 = false;
        } 
        if (PlayerPrefs.GetInt("possuiP08") == 1)
        {
            possuiP08 = true;
        }

        if(PlayerPrefs.GetInt("possuiThompson") == 0){
            possuiThompson = false;
        }
        if (PlayerPrefs.GetInt("possuiThompson") == 1)
        {
            possuiThompson = true;
        }

        if(PlayerPrefs.GetInt("possuiMG42") == 0){
            possuiMG42 = false;
        }
        if (PlayerPrefs.GetInt("possuiMG42") == 1)
        {
            possuiMG42 = true;
        }

        //Munições
        municaoFaca = 1;
        municaoP08Pente = 8;
        municaoP08Total = 56;
        municaoThompsonPente = 25;
        municaoThompsonTotal = 75;
        municaoMG42Pente = 100000;
        municaoMG42Total = 100000;
        municaoGranada = 3;
    }

    void Update()
    {
        //Equipar armas
        if (possuiFaca == true && Input.GetKeyDown(KeyCode.Alpha1))
        {
            TrocarArmaAtual(armasDoJogador = ArmasPossiveis.FACA);
        }
        if (possuiP08 == true && Input.GetKeyDown(KeyCode.Alpha2))
        {
            TrocarArmaAtual(armasDoJogador = ArmasPossiveis.P08);
        }
        if (possuiThompson == true && Input.GetKeyDown(KeyCode.Alpha3))
        {
            TrocarArmaAtual(armasDoJogador = ArmasPossiveis.THOMPSON);
        }
        if (possuiMG42 == true && Input.GetKeyDown(KeyCode.Alpha4))
        {
            TrocarArmaAtual(armasDoJogador = ArmasPossiveis.MG42);
        }
        if (possuiGranada == true && Input.GetKeyDown(KeyCode.G))
        {
            TrocarArmaAtual(armasDoJogador = ArmasPossiveis.GRANADA);
        }

        //Trocar funcionamento das armas
        switch (armasDoJogador){
            case ArmasPossiveis.FACA:
                Faca();
                break;

            case ArmasPossiveis.P08:
                P08();
                break;

            case ArmasPossiveis.THOMPSON:
                Thompson();
                break;

            case ArmasPossiveis.MG42:
                MG42();
                break;

            case ArmasPossiveis.GRANADA:
                Granada();
                break;
        }

        if(Input.GetKeyDown(KeyCode.R)){
            Recarregar();
        }

        granadasText.text = municaoGranada.ToString();
    }

    private void TrocarArmaAtual(ArmasPossiveis armaAtual){
        armaAtual = armasDoJogador;
    }

    private void Faca(){
        crosshair.SetActive(true);
        municaoNoPente.gameObject.SetActive(false);
        municaoTotal.gameObject.SetActive(false);
        faca.SetActive(true);
        p08.SetActive(false);
        thompson.SetActive(false);
        mg42.SetActive(false);
        granada.SetActive(false);
        municaoTotal.text = municaoFaca.ToString();
        Esfaquear();
        AtirarFaca();
    }

    private void P08(){
        crosshair.SetActive(true);
        municaoNoPente.gameObject.SetActive(true);
        municaoTotal.gameObject.SetActive(true);
        faca.SetActive(false);
        p08.SetActive(true);
        thompson.SetActive(false);
        mg42.SetActive(false);
        granada.SetActive(false);
        municaoNoPente.text = municaoP08Pente.ToString();
        municaoTotal.text = municaoP08Total.ToString();
        AtirarP08();
    }

    private void Thompson(){
        crosshair.SetActive(true);
        municaoNoPente.gameObject.SetActive(true);
        municaoTotal.gameObject.SetActive(true);
        faca.SetActive(false);
        p08.SetActive(false);
        thompson.SetActive(true);
        mg42.SetActive(false);
        granada.SetActive(false);
        municaoNoPente.text = municaoThompsonPente.ToString();
        municaoTotal.text = municaoThompsonTotal.ToString();
        AtirarThompson();
    }

    private void MG42(){
        crosshair.SetActive(true);
        municaoNoPente.gameObject.SetActive(true);
        municaoTotal.gameObject.SetActive(true);
        faca.SetActive(false);
        p08.SetActive(false);
        thompson.SetActive(false);
        granada.SetActive(false);
        mg42.SetActive(true);
        municaoNoPente.text = municaoMG42Pente.ToString();
        municaoTotal.text = municaoMG42Total.ToString();
        AtirarMg42();
    }

    private void Granada()
    {
        crosshair.SetActive(true);
        municaoNoPente.gameObject.SetActive(true);
        municaoTotal.gameObject.SetActive(true);
        faca.SetActive(false);
        p08.SetActive(false);
        thompson.SetActive(false);
        mg42.SetActive(false);
        granada.SetActive(true);
        municaoNoPente.text = municaoGranada.ToString();
        JogarGranada();
    }

    public void PegarFaca(){
        possuiFaca = true;
    }

    public void PegarP08(){
        PlayerPrefs.SetInt("possuiP08", 1);
        possuiP08 = true;
    }

    public void PegarThompson(){
        possuiThompson = true;
        PlayerPrefs.SetInt("possuiThompson", 1);
    }

    public void PegarMG42(){
        possuiMG42 = true;
        PlayerPrefs.SetInt("possuiMG42", 1);
    }

    public void RecarregarBalasP08(){
        municaoP08Total += 16;
    }

    public void RecarregarBalasThompson(){
        municaoP08Total += 25;
    }

    public void RecarregarBalasMG42(){
        municaoP08Total += 50;
    }

    #region Faca

    void Esfaquear()
    {
        if (Input.GetMouseButtonDown(0))
        {
            esfaqueou = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            esfaqueou = false;
        }

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, distanciaEsfaquear, ~layerIgnora) && esfaqueou)
        {
            if (possuiFaca)
            {
                jogador.playerAnim.Play("ANIM_Main_Char_knife_kill_Anim");
                jogador.playerAnim.SetInteger("estado", 18);
                if (hit.collider.tag == "InimigoCabeca" || hit.collider.tag == "InimigoPernas")
                {
                    var inimigo = hit.collider.gameObject.GetComponentInParent<SCPT_Inimigo>();
                    inimigo.tomouHS = true;
                    inimigo.vidaMaxima -= danoFaca;
                }

                if (hit.collider.tag == "InimigoTronco")
                {
                    var inimigo = hit.collider.gameObject.GetComponent<SCPT_Inimigo>();

                    inimigo.vidaMaxima -= danoFaca;
                }
            }
        }
    }

    public void AtirarFaca()
    {
        if (Input.GetKeyDown(KeyCode.C) && possuiFaca && municaoFaca > 0)
        {
            municaoFaca -= 1;
            audioSource.PlayOneShot(somFaca);
            jogador.playerAnim.Play("ANIM_Main_Char_throw_Anim");
            StartCoroutine(JogarAnim());
        }
    }

    #endregion

    #region Thompson

    void AtirarThompson()
    {
        if (Input.GetMouseButtonDown(0))
        {
            atirouThompson = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            atirouThompson = false;
        }

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, distanciaThompson, ~layerIgnora))
        {
            if(possuiThompson && municaoThompsonPente == 0 && atirouThompson){
                audioSource.PlayOneShot(falhaNaArma);
            }

            if (possuiThompson && atirouThompson && municaoThompsonPente > 0)
            {
                jogador.playerAnim.SetInteger("estado", 14);
                audioSource.PlayOneShot(somThompson);
                Rigidbody rb = Instantiate(rastroThompson, saidaThompson.transform.position + transform.forward/2, Quaternion.identity).GetComponent<Rigidbody>();
                Vector3 forca = (hit.point - rb.transform.position).normalized * 10000;
                rb.AddForce(forca);

                medidor.nivelMedidor += 1;
                municaoThompsonPente -= 1;

                //audioMaster.PlayOneShot(somTiro);

                if (hit.collider.tag == "InimigoCabeca")
                {
                    var inimigo = hit.collider.gameObject.GetComponentInParent<SCPT_Inimigo>();
                    inimigo.tomouHS = true;
                    inimigo.gameObject.GetComponent<SCPT_Inimigo>().somInimigo.PlayOneShot(inimigoTomandoDano);
                    inimigo.vidaMaxima -= danoThompson * 10;
                }

                if (hit.collider.tag == "InimigoTronco")
                {
                    var inimigo = hit.collider.gameObject.GetComponent<SCPT_Inimigo>();
                    inimigo.gameObject.GetComponent<SCPT_Inimigo>().somInimigo.PlayOneShot(inimigoTomandoDano);
                    inimigo.vidaMaxima -= danoThompson;
                    inimigo.sangueCorpo.Play();
                    inimigo.spawnado = true;
                }

                if (hit.collider.tag == "InimigoPernas")
                {
                    var inimigo = hit.collider.gameObject.GetComponentInParent<SCPT_Inimigo>();
                    inimigo.gameObject.GetComponent<SCPT_Inimigo>().somInimigo.PlayOneShot(inimigoTomandoDano);
                    inimigo.vidaMaxima -= danoThompson / 2;
                    inimigo.sanguePe.Play();
                    inimigo.spawnado = true;
                }

                atirouThompson = false;
            }
        }
    }

    #endregion

    #region MG42

    void AtirarMg42()
    {
        if (Input.GetMouseButtonDown(0))
        {
            atirouMG42 = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            atirouMG42 = false;
        }

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, distanciaMG42, ~layerIgnora))
        {
            if (possuiMG42 && atirouMG42 && municaoMG42Pente > 0)
            {
                jogador.playerAnim.SetInteger("estado", 14);
                audioSource.PlayOneShot(somMG42);

                Rigidbody rb = Instantiate(rastroMG42, saidaMG42.transform.position + transform.forward/2, Quaternion.identity).GetComponent<Rigidbody>();
                Vector3 forca = (hit.point - rb.transform.position).normalized * 10000;
                rb.AddForce(forca);

                medidor.nivelMedidor += 1;
                //audioMaster.PlayOneShot(somTiro);

                if (hit.collider.tag == "InimigoCabeca")
                {
                    var inimigo = hit.collider.gameObject.GetComponentInParent<SCPT_Inimigo>();
                    inimigo.gameObject.GetComponent<SCPT_Inimigo>().somInimigo.PlayOneShot(inimigoTomandoDano);
                    inimigo.tomouHS = true;
                    inimigo.vidaMaxima -= danoMG42 * 10;
                }

                if (hit.collider.tag == "InimigoTronco")
                {
                    var inimigo = hit.collider.gameObject.GetComponent<SCPT_Inimigo>();
                    inimigo.gameObject.GetComponent<SCPT_Inimigo>().somInimigo.PlayOneShot(inimigoTomandoDano);
                    inimigo.vidaMaxima -= danoMG42;
                    inimigo.sangueCorpo.Play();
                    inimigo.spawnado = true;
                }

                if (hit.collider.tag == "InimigoPernas")
                {
                    var inimigo = hit.collider.gameObject.GetComponentInParent<SCPT_Inimigo>();
                    inimigo.gameObject.GetComponent<SCPT_Inimigo>().somInimigo.PlayOneShot(inimigoTomandoDano);
                    inimigo.vidaMaxima -= danoMG42 / 2;
                    inimigo.sanguePe.Play();
                    inimigo.spawnado = true;
                }

                atirouMG42 = false;
            }
        }
    }

    #endregion

    #region P08

    void AtirarP08()
    {
        if (Input.GetMouseButtonDown(0))
        {
            atirouP08 = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            atirouP08 = false;
        }

        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, distanciaP08, ~layerIgnora))
        {
            if(possuiP08 && municaoP08Pente == 0 && atirouP08){
                audioSource.PlayOneShot(falhaNaArma);
            }

            if (possuiP08 && atirouP08 && municaoP08Pente > 0)
            {
                jogador.playerAnim.SetInteger("estado", 5);
                audioSource.PlayOneShot(somP08);

                Rigidbody rb = Instantiate(rastroP08, saidaP08.transform.position + transform.forward/2, Quaternion.identity).GetComponent<Rigidbody>();
                Vector3 forca = (hit.point - rb.transform.position).normalized * 10000;
                rb.AddForce(forca);

                medidor.nivelMedidor += 1;
                municaoP08Pente -= 1;

                //audioMaster.PlayOneShot(somTiro);

                if (hit.collider.tag == "InimigoCabeca")
                {
                    var inimigo = hit.collider.gameObject.GetComponentInParent<SCPT_Inimigo>();
                    inimigo.gameObject.GetComponent<SCPT_Inimigo>().somInimigo.PlayOneShot(inimigoTomandoDano);
                    inimigo.tomouHS = true;
                    inimigo.vidaMaxima -= danoP08 * 10;
                }

                if (hit.collider.tag == "InimigoTronco")
                {
                    var inimigo = hit.collider.gameObject.GetComponent<SCPT_Inimigo>();
                    inimigo.gameObject.GetComponent<SCPT_Inimigo>().somInimigo.PlayOneShot(inimigoTomandoDano);
                    inimigo.vidaMaxima -= danoP08;
                    inimigo.sangueCorpo.Play();
                    inimigo.spawnado = true;
                }

                if (hit.collider.tag == "InimigoPernas")
                {
                    var inimigo = hit.collider.gameObject.GetComponentInParent<SCPT_Inimigo>();
                    inimigo.gameObject.GetComponent<SCPT_Inimigo>().somInimigo.PlayOneShot(inimigoTomandoDano);
                    inimigo.vidaMaxima -= danoP08 / 2;
                    inimigo.sanguePe.Play();
                    inimigo.spawnado = true;
                }

                atirouP08 = false;
            }
        }
    }

    #endregion

    #region Crosshair

    public void EliminouInimigo(){
        StartCoroutine("TrocarMiraInimigoEliminado");
    }

    public void EliminouInimigoComHS(){
        StartCoroutine("TrocarMiraInimigoEliminadoHS");
    }

    IEnumerator TrocarMiraInimigoEliminado(){
        crosshair.GetComponent<Image>().sprite = caveiraMorte;
        yield return new WaitForSecondsRealtime(0.25f);
        crosshair.GetComponent<Image>().sprite = crosshairPadrao;
        yield return null;
    }

    IEnumerator TrocarMiraInimigoEliminadoHS(){
        crosshair.GetComponent<Image>().sprite = caveiraMorteHS;
        yield return new WaitForSecondsRealtime(0.25f);
        crosshair.GetComponent<Image>().sprite = crosshairPadrao;
        yield return null;
    }

    #endregion

    #region Recarregar
    void Recarregar(){
        switch (armasDoJogador)
        {
            case (ArmasPossiveis.P08):
                jogador.playerAnim.SetInteger("estado", 8);
                audioSource.PlayOneShot(somRecarregarP08);
                if(municaoP08Total >= 8)
                {
                municaoP08Total -= (8 - municaoP08Pente);
                municaoP08Pente = 8;
                }
                else
                {
                    if(municaoP08Pente + municaoP08Total > 8)
                    {
                        municaoP08Total -= (8 - municaoP08Pente);
                        municaoP08Pente = 8;
                    }
                else
                    {
                        municaoP08Pente += municaoP08Total;
                        municaoP08Total = 0;
                    }
                }
            break;

            case(ArmasPossiveis.THOMPSON):
                jogador.playerAnim.SetInteger("estado", 8);
                audioSource.PlayOneShot(somRecarregarThompson);
                if(municaoThompsonTotal >= 25)
                {
                municaoThompsonTotal -= (25 - municaoThompsonPente);
                municaoThompsonPente = 25;
                }
                else
                {
                    if(municaoThompsonPente + municaoThompsonTotal > 25)
                    {
                        municaoThompsonTotal -= (25 - municaoThompsonPente);
                        municaoThompsonPente = 25;
                    }
                else
                    {
                        municaoThompsonPente += municaoThompsonTotal;
                        municaoThompsonTotal = 0;
                    }
                }
            break;
        }
    }

    #endregion

    #region Granada

    public void JogarGranada()
    {
        if (Input.GetMouseButtonDown(0) && possuiGranada && municaoGranada > 0)
        {
            municaoGranada -= 1;
            jogador.playerAnim.Play("ANIM_Main_Char_throw_Anim");
            StartCoroutine(JogarAnim());
        }
    }

    #endregion

    IEnumerator JogarAnim()
    {
        yield return new WaitForSeconds(0.6f);

        if(armasDoJogador == ArmasPossiveis.FACA)
        {
            GameObject facaTemp = Instantiate(facaPrefab, saidaFaca.transform.position, saidaFaca.transform.rotation);
            Rigidbody rb = facaTemp.GetComponent<Rigidbody>();
            rb.AddForce(facaTemp.transform.forward * forcaArremessoFaca, ForceMode.VelocityChange);
        }
        if (armasDoJogador == ArmasPossiveis.GRANADA)
        {
            GameObject granadaTemp = Instantiate(granadaPrefab, saidaFaca.transform.position, saidaFaca.transform.rotation);
            Rigidbody rb = granadaTemp.GetComponent<Rigidbody>();
            rb.AddForce(granadaTemp.transform.forward * forcaArremessoGranada, ForceMode.VelocityChange);
        }
    }
}