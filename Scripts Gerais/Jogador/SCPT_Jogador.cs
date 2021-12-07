using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Jogador
{
    public class SCPT_Jogador : MonoBehaviour
    {
        #region Variaveis

        [SerializeField] private SCPT_ArmasDoJogador armasScript;
        [SerializeField] private SCPT_Camera cameraMira;
        [SerializeField] private GameObject telaDeMorte;
        [SerializeField] public Image tomouDanoFeedback;
        [SerializeField] private Text vidaJogadorTexto;
        [SerializeField] private AudioSource somJogador;
        [SerializeField] private AudioClip somAndar;


        [Header("Transforms")]
        [SerializeField] private Transform transformCam;
        [SerializeField] private Transform roofCheck;
        [SerializeField] private Transform pontoTiro;
        [SerializeField] private Transform pontoArremesso;

        [Header("Valores")]

        public float gravidade = 10;
        public float forcaSlopeRayDist;
        public float forcaSlope;
        public float pulo = 10;
        public float velAbaixar = 6;
        public float velAndar = 12;
        public float roofDist = 0.4f;
        public float velRotacao;
        public float distanciaRay;

        [HideInInspector] public float velocidade;

        [Header("Camera")]

        Transform cameraTransform;

        [Header("Masks")]

        public LayerMask roofMask;

        [Header("Variáveis do Jogador")]
        public int vidaJogador;

        //Variaveis Privadas

        Vector3 movimentoPlayer;

        CharacterController controlador;
        [HideInInspector] public Animator playerAnim;

        bool pulou;
        bool agachou;
        bool tocouTeto;
        bool levantado = true;
        bool interagiu;

        [Header("Checkpoints do Jogador")]
        [SerializeField] private Transform Fase1Checkpoint1 = null;
        [SerializeField] private Transform Fase1Checkpoint2 = null;
        [SerializeField] private Transform Fase2Checkpoint1 = null;
        [SerializeField] private Transform Fase2Checkpoint2 = null;
        [SerializeField] private Transform Fase2Checkpoint3 = null;
        [SerializeField] private Transform Fase3Checkpoint1 = null;
        [SerializeField] private Transform Fase3Checkpoint2 = null;

        #endregion

        #region Metodos Padrao

        void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controlador = GetComponent<CharacterController>();
            cameraTransform = Camera.main.transform;
            var colorNormal = new Color(255,0, 0, 0);
            tomouDanoFeedback.color = colorNormal;

            if (playerAnim == null)
                playerAnim = GetComponent<Animator>();
        }

        void Start()
        {
            if(SceneManager.GetActiveScene().name == "Fase1"){
                if(PlayerPrefs.GetInt("checkpointFase1") == 1)
                {
                    this.gameObject.transform.position = Fase1Checkpoint1.transform.position;
                    this.gameObject.transform.rotation = Fase1Checkpoint1.transform.rotation;
                }
                if(PlayerPrefs.GetInt("checkpointFase1") == 2)
                {
                    this.gameObject.transform.position = Fase1Checkpoint2.transform.position;
                    this.gameObject.transform.rotation = Fase1Checkpoint2.transform.rotation;
                }
            }

            if(SceneManager.GetActiveScene().name == "Fase2"){
                if(PlayerPrefs.GetInt("checkpointFase2") == 1)
                {
                    this.gameObject.transform.position = Fase2Checkpoint1.transform.position;
                    this.gameObject.transform.rotation = Fase2Checkpoint1.transform.rotation;
                }
                if(PlayerPrefs.GetInt("checkpointFase2") == 2)
                {
                    this.gameObject.transform.position = Fase2Checkpoint2.transform.position;
                    this.gameObject.transform.rotation = Fase2Checkpoint2.transform.rotation;
                }
                if(PlayerPrefs.GetInt("checkpointFase2") == 3)
                {
                    this.gameObject.transform.position = Fase2Checkpoint3.transform.position;
                    this.gameObject.transform.rotation = Fase2Checkpoint3.transform.rotation;
                }
            }

            if(SceneManager.GetActiveScene().name == "Fase3"){
                if(PlayerPrefs.GetInt("checkpointFase3") == 1)
                {
                    this.gameObject.transform.position = Fase3Checkpoint1.transform.position;
                    this.gameObject.transform.rotation = Fase3Checkpoint1.transform.rotation;
                }
                if(PlayerPrefs.GetInt("checkpointFase3") == 2)
                {
                    this.gameObject.transform.position = Fase3Checkpoint2.transform.position;
                    this.gameObject.transform.rotation = Fase3Checkpoint2.transform.rotation;
                }
            }

            velocidade = velAndar;
            vidaJogador = 100;
        }

        void Update()
        {
            Debug.Log(PlayerPrefs.GetInt("checkpointFase3"));
            Pulo();
            Movimento();
            CheckarTeto();
            Interagir();
            Agachar();

            Quaternion rotacao = Quaternion.Euler(cameraTransform.eulerAngles.x, cameraTransform.eulerAngles.y, cameraTransform.eulerAngles.z);
            pontoTiro.transform.rotation = Quaternion.Lerp(pontoTiro.transform.rotation, rotacao, velRotacao * Time.deltaTime);
            pontoArremesso.transform.rotation = Quaternion.Lerp(pontoArremesso.transform.rotation, rotacao, velRotacao * Time.deltaTime);

            vidaJogadorTexto.text = vidaJogador.ToString();

            if(vidaJogador > 100){
                vidaJogador = 100;
            }

            if(vidaJogador <= 0){
                telaDeMorte.SetActive(true);
            }
        }

        #endregion

        #region Metodos

        #region Andar

        void Movimento()
        {
            float horizInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");
            movimentoPlayer.x = 0;
            movimentoPlayer.z = 0;
            movimentoPlayer += horizInput * transform.right * velocidade;
            movimentoPlayer += vertInput * transform.forward * velocidade;
            //Gravidade
            movimentoPlayer.y -= gravidade * Time.deltaTime;
            //Controlador de movimento
            controlador.Move(movimentoPlayer * Time.deltaTime);

            //Idle
            if((horizInput == 0 && vertInput == 0) && controlador.isGrounded && !agachou && !cameraMira.mirou)
            {
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.FACA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.GRANADA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.PEDRA)
                {
                    playerAnim.SetInteger("estado", 15);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.P08)
                {
                    playerAnim.SetInteger("estado", 1);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.THOMPSON || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.MG42)
                {
                    playerAnim.SetInteger("estado", 10);
                }
            }
            if ((horizInput == 0 && vertInput == 0) && controlador.isGrounded && agachou && !cameraMira.mirou)
            {
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.FACA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.GRANADA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.PEDRA)
                {
                    playerAnim.SetInteger("estado", 6);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.P08)
                {
                    playerAnim.SetInteger("estado", 6);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.THOMPSON || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.MG42)
                {
                    playerAnim.SetInteger("estado", 6);
                }
            }

            //Andar
            if ((horizInput != 0 || vertInput != 0) && controlador.isGrounded && !agachou && !cameraMira.mirou)
            {
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.FACA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.GRANADA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.PEDRA)
                {
                    playerAnim.SetInteger("estado", 16);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.P08)
                {
                    playerAnim.SetInteger("estado", 2);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.THOMPSON || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.MG42)
                {
                    playerAnim.SetInteger("estado", 11);
                }
            }
            if ((horizInput != 0 || vertInput != 0) && controlador.isGrounded && agachou && !cameraMira.mirou)
            {
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.FACA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.GRANADA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.PEDRA)
                {
                    playerAnim.SetInteger("estado", 7);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.P08)
                {
                    playerAnim.SetInteger("estado", 7);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.THOMPSON || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.MG42)
                {
                    playerAnim.SetInteger("estado", 7);
                }
            }

            if ((vertInput != 0 || horizInput != 0) && OnSlope())
            {
                controlador.Move(Vector3.down * controlador.height / 2 * forcaSlope * Time.deltaTime);
            }

            Quaternion rotacao = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacao, velRotacao * Time.deltaTime);
        }

        #endregion

        #region Detectar o Chao

        public bool OnSlope()
        {
            if (pulou)
                return false;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, controlador.height / 2 * forcaSlopeRayDist))
            {
                if (hit.normal != Vector3.up)
                    return true;
                #region Sons
                /*if (menu.GetComponent<Menu>().menuActive == false)
                {
                    if (hit.collider.CompareTag("Terreno") && somAndar.isPlaying == false && correndo == false)
                    {
                        somAndar.Stop();
                        somAndar.PlayOneShot(andarFloresta);
                    }

                    if (hit.collider.CompareTag("Terreno") && somAndar.isPlaying == false && correndo == true)
                    {
                        somAndar.Stop();
                        somAndar.PlayOneShot(correrFloresta);
                    }

                    if (hit.collider.CompareTag("Indoor") && somAndar.isPlaying == false && correndo == false)
                    {
                        somAndar.Stop();
                        somAndar.PlayOneShot(andarIndoor);
                    }

                    if (hit.collider.CompareTag("Indoor") && somAndar.isPlaying == false && correndo == true)
                    {
                        somAndar.Stop();
                        somAndar.PlayOneShot(correrIndoor);
                    }
                }
                else
                {
                    somAndar.Stop();
                }*/
                #endregion
            }
            return false;
        }
        #endregion

        #region Pulo

        void Pulo()
        {
            if (controlador.isGrounded && movimentoPlayer.y < 0)
            {
                movimentoPlayer.y = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && controlador.isGrounded)
            {
                movimentoPlayer.y = pulo;

                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.FACA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.GRANADA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.PEDRA)
                {
                    playerAnim.SetInteger("estado", 17);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.P08)
                {
                    playerAnim.SetInteger("estado", 3);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.THOMPSON || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.MG42)
                {
                    playerAnim.SetInteger("estado", 12);
                }
            }
            controlador.Move(movimentoPlayer * Time.deltaTime);
        }

        #endregion

        #region Agachar

        public void Agachar()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && controlador.isGrounded)
            {
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.FACA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.GRANADA || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.PEDRA && !armasScript.esfaqueou)
                {
                    playerAnim.SetInteger("estado", 6);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.P08)
                {
                    playerAnim.SetInteger("estado", 6);
                }
                if (armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.THOMPSON || armasScript.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.MG42)
                {
                    playerAnim.SetInteger("estado", 6);
                }
                velocidade = velAbaixar;
                controlador.height = 1f;
                controlador.center = new Vector3(0, 1f, 0);
                agachou = true;
            }

            else if (Input.GetKeyUp(KeyCode.LeftControl) && controlador.isGrounded)
            {
                velocidade = velAndar;
                controlador.height = 4.9f;
                controlador.center = new Vector3(0, 2.5f, 0);
                agachou = false;
            }
        }

        #endregion

        #region Checkar o teto

        void CheckarTeto()
        {
            tocouTeto = Physics.CheckSphere(roofCheck.position, roofDist, roofMask);
            if (tocouTeto && !agachou)
            {
                controlador.height = 1.9f;
                controlador.center = new Vector3(0, -0.85f, 0);
                transformCam.localPosition = new Vector3(0, 0.4f, 0);
                velocidade = velAbaixar;
                agachou = true;
                levantado = false;

            }
            else if (!tocouTeto && !levantado)
            {
                controlador.height = 3.8f;
                controlador.center = new Vector3(0, 0, 0);
                transformCam.localPosition = new Vector3(0, 1.3f, 0);
                velocidade = velAndar;
                agachou = false;
                levantado = true;
            }
        }

        #endregion

        #region Interacao

        void Interagir()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interagiu = true;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                interagiu = false;
            }

            Debug.DrawRay(cameraTransform.transform.position, cameraTransform.transform.forward * distanciaRay, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, distanciaRay) && interagiu)
            {

                if (hit.transform.tag == "Faca")
                {
                    hit.transform.gameObject.SetActive(false);
                    armasScript.municaoFaca += 1;
                }
                if(hit.transform.tag == "Thompson")
                {
                    hit.transform.gameObject.SetActive(false);
                    armasScript.PegarThompson();
                }
                if(hit.transform.tag == "MG42")
                {
                    hit.transform.gameObject.SetActive(false);
                    armasScript.PegarMG42();
                }
                if (hit.transform.tag == "Bomba")
                {
                    hit.transform.gameObject.SetActive(false);
                }

                interagiu = false;
            }
        }

        #endregion

        #region PiscarTela
        public IEnumerator PiscarTela(){
            var corPiscarTela = new Color(255, 0, 0, 0.1f);
            tomouDanoFeedback.color = corPiscarTela;
            yield return new WaitForSeconds(0.1f);
            var colorNormal = new Color(255, 0, 0, 0);
            tomouDanoFeedback.color = colorNormal;
            yield return null;
        }

        #endregion

        #endregion
    }
}