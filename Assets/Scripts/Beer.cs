using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Beer.cs
/// Christopher Marks
/// 4/9/2019
/// </summary>

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Beer : MonoBehaviour
{

    private Rigidbody _beerRigidBody;                                       // Rigidbody component

    private MeshRenderer _beerMeshRenderer;                                 // Mesh Renderer component

    private AudioSource _beerAudioSource;                                   // Audio source for the beer

    public float _beerSlideSpeed = 2f;                                      // speed that beer slides along bar
    public float _beerReturnSpeed = 2f;                                     // speed that beer slides back to bartender at
   
    public ParticleSystem _beerVFX_SlideToPatron;                           // Particle effect for serving beer

    private ParticleSystem _tempParticleSystem;                             // Temp container for holding particle systems

    public Material _emptyBeerMaterial;                                     // Material for empty beer glass

    public BeerState _beerStateController;                                  // controlls the state of the beer

    public enum BeerState                                                   // declares enum for different state beer can be in
    {
        Spawn,
        Idle,
        SlideToPatron,
        Drink,
        ReturnToBartender
    };

    private void Awake()
    {
        _beerRigidBody = GetComponent<Rigidbody>();                         // stores the beers rigidbody component
        _beerRigidBody.Sleep();                                             // sets rigidbody to sleep at awake

        _beerAudioSource = GetComponent<AudioSource>();                     // stores the beers audio source component

        _beerMeshRenderer = GetComponent<MeshRenderer>();                   // stores the mesh renderer component
    }

    // Start is called before the first frame update
    void Start()
    {
        _beerStateController = Beer.BeerState.Spawn;                        // set starting state to spawn

        StartCoroutine("BeerStateManager");                                 // call the beer state manager coroutine
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator BeerStateManager()
    {
        while (true)
        {
            switch (_beerStateController)
            {
                case BeerState.Spawn:
                    Spawn();
                    break;
                case BeerState.Idle:
                    Spawn();
                    break;
                case BeerState.SlideToPatron:
                    SlideToPatron();
                    break;
                case BeerState.Drink:
                    Drink();
                    break;
                case BeerState.ReturnToBartender:
                    ReturnToBartender();
                    break;
            }
            yield return null;
        }
    }

    private void Spawn()
    {
        _beerRigidBody.velocity = new Vector3(0, 0, 0);                         // set starting velocity
        _beerStateController = BeerState.Idle;                                  // set beer state to idle
    }

    private void Idle()
    {
        return;                                                                 // return and do nothing
    }

    private void SlideToPatron()
    {
        if (_beerRigidBody.velocity == new Vector3(0, 0, 0))                    // if beer isn't moving
        {
            _beerRigidBody.velocity =
                (transform.right * -1) * _beerSlideSpeed;                       // sets the beers velocity to slide down the bar
        }

        if(_tempParticleSystem == null)                                         // if our temp particle system is null
        {
            _tempParticleSystem =                                               // give it the looping slide particle system
                Instantiate(
                _beerVFX_SlideToPatron,
                transform.position,
                Quaternion.identity
                );
            _tempParticleSystem.transform.parent = gameObject.transform;        // parent the particle system to the sliding beer
        }
    }

    private void Drink()
    {
        if(_beerRigidBody.velocity != Vector3.zero)
        {
            _beerRigidBody.velocity = Vector3.zero;                         // stop the beer glass from moving
            _beerMeshRenderer.material = _emptyBeerMaterial;
            _tempParticleSystem.Stop();
            Destroy(_tempParticleSystem.gameObject);
        }

    }

    private void ReturnToBartender()
    {
        _beerRigidBody.velocity =
            transform.right * _beerReturnSpeed;                                 // sets the beers velocity when sliding down bar
    }

    public void Slide()
    {
        _beerStateController = BeerState.SlideToPatron;
    }

    public void SlideBack()
    {
        _beerStateController = Beer.BeerState.ReturnToBartender;
    }

    public void DrinkBeer()
    {
        _beerStateController = BeerState.Drink;
    }

}
