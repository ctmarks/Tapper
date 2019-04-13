using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player.cs
/// Christopher Marks
/// 4/9/2019
/// </summary>

[RequireComponent(typeof(AudioSource))]

public class Player : MonoBehaviour
{
    public BeerSpawner _beerSpawner;
    private Beer _playersCurrentBeer;

    public ParticleSystem _playerVFX_PourBeer;
    public ParticleSystem _playerVFX_ServeBeer;
    private ParticleSystem _tempParticleSystem;

    public AudioClip _playerSFX_PourBeer;                                               
    public AudioClip _playerSFX_ServeBeer;                                              
    private AudioSource _playerAudioSource;                                             

    public bool _playerIsAtTap;                                                         


    private void Awake()
    {
        _playerAudioSource = GetComponent<AudioSource>();                                
    }


    // Start is called before the first frame update
    void Start()
    {
        _playerIsAtTap = true;                                                          
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _playerIsAtTap)                          
        {
            if (_playersCurrentBeer != null)
            {
                ServeBeer();                                                            
            }
            else
            {
                PourBeer();                                                             
            }
        }
    }


    private void PourBeer()
    {
        _playersCurrentBeer = _beerSpawner.SpawnBeer();                                 
    }


    private void ServeBeer()
    {
        _playersCurrentBeer.Slide();                                                    
        _playerAudioSource.PlayOneShot(_playerSFX_ServeBeer);                           
        _playersCurrentBeer = null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tap")                                                          
        {
            _playerIsAtTap = true;                                                      
        }

        if(other.tag == "Beer" && 
            other.GetComponent<Beer>()._beerStateController == Beer.BeerState.ReturnToBartender)
        {
            Destroy(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Tap")                                                          
        {
            _playerIsAtTap = false;                                                     
        }
    }
}

