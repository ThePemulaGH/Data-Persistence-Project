using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private float multiplier; 

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        if (GameManager.instance != null )
        {
            multiplier = GameManager.instance.difficulty;
        }
        else multiplier = 1.0f;
    }
    
    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f * multiplier;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > 3.0f * multiplier)
        {
            velocity = velocity.normalized * 3.0f * multiplier;
        }

        m_Rigidbody.velocity = velocity;
    }
}
