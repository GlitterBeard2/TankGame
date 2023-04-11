using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    // prefab of the shell
    public Rigidbody m_Shell;
    // A child of the tank where the shells are spawned
    public Transform m_FireTransform;
    //the force given to the shell when firing
    public float m_launchForce = 30f;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1")) 
        {
            Fire();

        }
    }

    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position,
            m_FireTransform.rotation) as Rigidbody;

        //set the shellls velocity to the lauch force in fire
        //positions forward 
        shellInstance.velocity = m_launchForce * m_FireTransform.forward;
    }


}
