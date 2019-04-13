using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PourBeerParticle.cs
/// Christopher Marks
/// 4/10/2019
/// </summary>

public class ParticleDestroyer : MonoBehaviour
{
    private float DestroyTime;                                   // Time limit that determines when particle system will destroy itself

    private float timer = 0;                                    // Timer for tracking time since particle spawn

    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        DestroyTime = _particleSystem.main.duration;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;                                // Add amount of time passed every frame

        if (timer >= DestroyTime)                               // If timer is greater than or equal to Destroy time
            Destroy(gameObject);                                // Destroy this gameObject
    }
}
