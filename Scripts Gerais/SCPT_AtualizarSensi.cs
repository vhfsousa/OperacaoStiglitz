using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SCPT_AtualizarSensi : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cameraPadrao;
    [SerializeField] private CinemachineVirtualCamera cameraMira;

    void Update()
    {
        cameraPadrao.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = PlayerPrefs.GetFloat("Sensibilidade");
        cameraPadrao.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = PlayerPrefs.GetFloat("Sensibilidade");
        cameraMira.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = (PlayerPrefs.GetFloat("Sensibilidade") / 2);
        cameraMira.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = (PlayerPrefs.GetFloat("Sensibilidade") / 2);
    }
}
