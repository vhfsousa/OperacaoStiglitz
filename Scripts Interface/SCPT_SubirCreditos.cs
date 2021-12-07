using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCPT_SubirCreditos : MonoBehaviour
{
    [SerializeField] private int alturaMax;
    [SerializeField] private int alturaMin;
    [SerializeField] private int velocidade;
    [SerializeField] private RectTransform transformCreditos;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if(transformCreditos.anchoredPosition.y >= alturaMax){
            transformCreditos.anchoredPosition = new Vector3 (0, alturaMin, 0);
        }

        transform.Translate(Vector3.up * Time.deltaTime * velocidade, Space.World);
    }
}