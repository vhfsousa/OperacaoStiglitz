using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Jogador;

public class SCPT_Camera : MonoBehaviour
{
    [SerializeField] private SCPT_Jogador player;
    [SerializeField] private SCPT_ArmasDoJogador armasDoJogador;
    [SerializeField] private CinemachineVirtualCamera cameraMira;

    public float sense;

    public int prioridade;

    [HideInInspector] public bool mirou;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (armasDoJogador.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.P08)
            {
                player.playerAnim.SetInteger("estado", 4);
            }
            if (armasDoJogador.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.THOMPSON || armasDoJogador.armasDoJogador == SCPT_ArmasDoJogador.ArmasPossiveis.MG42)
            {
                player.playerAnim.SetInteger("estado", 13);
            }

            cameraMira.Priority += prioridade;
            mirou = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            cameraMira.Priority -= prioridade;
            mirou = false;
        }
    }
}
