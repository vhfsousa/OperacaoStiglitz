using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPT_VisaoInimigo : MonoBehaviour
{
    public bool viuPlayer;

    public BoxCollider m_collider;

    [SerializeField] private float visaoDist;

    RaycastHit hit;

    private void Start()
    {
        m_collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (Physics.BoxCast(m_collider.bounds.center, m_collider.bounds.center, transform.forward, out hit, transform.rotation, visaoDist))
        {
            Debug.Log(hit.collider.name);
            if (hit.transform.tag == "Player")
            {
                Debug.Log("osjnfgijdsfvijhdvb");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, m_collider.bounds.center/3);
    }
}
