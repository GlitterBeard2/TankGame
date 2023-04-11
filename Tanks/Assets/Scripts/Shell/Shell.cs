using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    //the time in aseconds before the shell is removed
    public float m_MaxLifeTime = 2f;
    //amount of damage done when it hts the tank
    public float m_MaxDamage = 34f;
    //maximym distance away from the explosion the tanks can still be afected from
    public float m_ExplosionRadius = 5;
    //the amount of forse added to a tank at the center of explosion
    public float m_ExplosionForce = 100f;

    //ExposedReference to the particles that will play
    public ParticleSystem m_ExplosionParticles;

    private void OnCollisionEnter(Collision other)
    {
        // find the rigidbody of the collision object
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
        // only tanks will have rigidbody scripts
        if (targetRigidbody != null)
        {
            // Add an explosion force
            targetRigidbody.AddExplosionForce(m_ExplosionForce,
            transform.position, m_ExplosionRadius);
            // find the TankHealth script associated with the rigidbody
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
            if (targetHealth != null)
            {
                // Calculate the amount of damage the target should take
                // based on it's distance from the shell.
                float damage = CalculateDamage(targetRigidbody.position);
                // Deal this damage to the tank
                targetHealth.TakeDamage(damage);
            }
        }
        //unparent the particles
        m_ExplosionParticles.transform.parent = null;

        //play the particle
        m_ExplosionParticles.Play();

        //once the particles have finished destraoy the game object they are on
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        //destroy sheell
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target
        Vector3 explosionToTarget = targetPosition - transform.position;
        // Calculate the distance from the shell to the target
        float explosionDistance = explosionToTarget.magnitude;
        // Calculate the proportion of the maximum distance (the explosionRadius)
        // the target is away
        float relativeDistance =
        (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;
        // Calculate damage as this proportion of the maximum possible damage
        float damage = relativeDistance * m_MaxDamage;
        // Make sure that the minimum damage is always 0
        damage = Mathf.Max(0f, damage);
        return damage;
    }
}
