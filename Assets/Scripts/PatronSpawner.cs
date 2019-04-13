using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PatronSpawner.cs
/// Christopher Marks
/// 4/11/2019
/// </summary>

[RequireComponent(typeof(AudioSource))]

public class PatronSpawner : MonoBehaviour
{
    public Patron _patron;

    public AudioClip _patronSpawnerSFX_Spawn;
    private AudioSource _patronSpawnerAudioSource;

    private void Awake()
    {
        _patronSpawnerAudioSource = GetComponent<AudioSource>();
    }

    public Patron Spawn()
    {
        Patron patron = Instantiate(_patron, transform);
        _patronSpawnerAudioSource.PlayOneShot(_patronSpawnerSFX_Spawn);
        return patron;
    }
}
