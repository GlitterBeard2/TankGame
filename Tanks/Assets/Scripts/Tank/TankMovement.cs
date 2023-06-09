using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

    public float m_Speed = 12f;      // How fast the tank moves forward and back
    public float m_TurnSpeed = 180f; //how fast the tank turns in degrees per second

    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue; // the current value of the movement input
    private float m_TurnInputValue;     // the current value of the turn iunput

    private void Awake()
    {
        m_Rigidbody= GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //when tank is turned on make sure it is not kinematic
        m_Rigidbody.isKinematic = false;

        //allso reset input values
        m_MovementInputValue= 0f;
        m_TurnInputValue = 0f;
    }

    private void OnDisable()
    {
        //when tan is turned off, set it to kinematic so it stops moveing
        m_Rigidbody.isKinematic = true;
    }

    //update is called once per frame
    private void Update()
    {
        m_MovementInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        //create a vector in the direction  the tank is facing with a magnitude 
        //based on the input speed and time between frames
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        //aply this movement to rigid body position
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void Turn()
    {
        //determine the nuumber of degrees to be turned based on the input
        // speed and time between frames
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        //,ake this into a rotation i nthe axis of y
        Quaternion turnRotation = Quaternion.Euler(0f,turn, 0f);

        // apply this rotation to the rigid body
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

}

