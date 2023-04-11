using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMovement : MonoBehaviour
{
    // The tank will stop moving towards the player once it reaches this distance 
    public float m_CloseDistance = 8f;
    //the tanks turret object
    public Transform m_Turret;

    //a reference to the player. this will be set when the enemy reloads
    private GameObject m_Player;
    //a rav mesh agent component
    private NavMeshAgent m_NavAgent;
    private Rigidbody m_Rigidbody;
    // a reference to the rigid body comonent
    private bool m_Follow;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Follow == false)
            return;

        //get the distance from the palkayer to enemy tank
        float distance = (m_Player.transform.position - transform.position).magnitude;
        //if distance is less than stop distancel than stop moving
        if (distance > m_CloseDistance)
        {
            m_NavAgent.SetDestination(m_Player.transform.position);
            m_NavAgent.isStopped = false;
        }
        else
        {
            m_NavAgent.isStopped = true;

            
        }

        if (m_Turret != null)
        {
            m_Turret.LookAt(m_Player.transform);
        }
    }

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Follow = false;
    }

    private void OnEnable()
    {
        //when the tank us turned on, make sure it is not kinimatic
        m_Rigidbody.isKinematic= false;
    }

    private void OnDisable()
    {
        //when the thank is turned off set it t kinimatic so it stops moving'
        m_Rigidbody.isKinematic= true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = false;
        }
        
    }
    ///excesris


}
