using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    //tnak respawn dudedad
    public TankSpawner SpawnController;

    //the amount of health
    public float m_StartingHealth = 100f;
    //tank dies
    public GameObject m_ExplosionPrefab;

    private float m_CurrentHealth;
    private bool m_Dead;
    //particle system
    private ParticleSystem m_ExplosionParticles;


    private void Awake()
    {
        SpawnController = GameObject.FindObjectOfType<TankSpawner>();
        m_ExplosionParticles =
            Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        //disable the prefab so it can be activated when required
        m_ExplosionParticles.gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        //when enabled reset health and whetehr or not it dead
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }

    private void SetHealthUI()
    {
        //update user interface with health
    }

    public void TakeDamage(float amount)
    {
        //reduce current health by the amount of damage
        m_CurrentHealth -= amount;
        //change ui elements apropriate
        SetHealthUI();
        //if the current health below zero call on death
        if(m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        m_Dead = true;
        //move the instantiated explosion prefab
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        //play the partcle system
        m_ExplosionParticles.Play();
        gameObject.SetActive(false);

        if(this.tag != "Player")
        {
            SpawnController.spawnTank();
        }
        

        //spawwn new tank
        //        Gameobject a = Instantiate(m_tankPrefab, new Vector3(2.0f, 0, 0),Quaternion.identity);
        //      Debug.Log("spawned tabjk");
    }
}
