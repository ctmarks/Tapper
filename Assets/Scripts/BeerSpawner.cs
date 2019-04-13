using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BeerSpawner.cs
/// Christopher Marks
/// 4/10/2019
/// </summary>

[RequireComponent(typeof(AudioSource))]

public class BeerSpawner : MonoBehaviour
{
    public GameObject _beer;                                                        // slot in inspector for Beer prefab

    public AudioClip _beerSpawnerSFX_Spawn;                                         // Audio clip for pouring beer
    private AudioSource _beerSpawnerAudioSource;                                    // variable reference for audio source component

    public ParticleSystem _beerSpawnerVFX_Spawn;                                    // Particle effect for beer spawning

    private void Awake()
    {
        _beerSpawnerAudioSource = GetComponent<AudioSource>();                      // store audio source component for use
    }

    public Beer SpawnBeer()
    {
        Beer beer = Instantiate(_beer, gameObject.transform).GetComponent<Beer>();  // then spawn beer at the beer spawner location
        beer.transform.parent = null;                                               // don't parent the beer to the spawner
        _beerSpawnerAudioSource.PlayOneShot(_beerSpawnerSFX_Spawn);                 // play pour beer sound effect
        Instantiate(_beerSpawnerVFX_Spawn, transform);                              // spawn beer particle effect
        return beer;                                                                // return the spawned beer to the caller
    }
}
