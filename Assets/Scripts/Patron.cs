using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Patron.cs
/// Christopher Marks
/// 4/9/2019
/// </summary>

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Patron : MonoBehaviour
{
    private GameObject[] gameObjectArray;

    private Rigidbody _patronRigidbody;

    private Beer _patronCurrentBeer;                                                       

    public float _patronWalkSpeed;
    public float _patronWalkingDelay;
    private float _patronWalkingTimer = 0;

    public float _patronSlideTime;
    private float _patronSlideTimer = 0;
    public float _patronSlideSpeed;

    public float _patronDrinkTime;
    private float _patronDrinkTimer = 0;

    private PatronState _patronStateController;                                             
    private enum PatronState                                                                
    {
        Spawn,
        Walking,
        Idle,
        Sliding,
        Drinking,
    }

    private void Awake()
    {
        _patronRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        

        _patronStateController = Patron.PatronState.Walking;

        _patronRigidbody.velocity = Vector3.zero;

        StartCoroutine("PatronStateMachine");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Beer(Clone)" && _patronCurrentBeer == null)
        {
            _patronCurrentBeer = other.GetComponent<Beer>();

            other.transform.parent = transform;

            _patronStateController = Patron.PatronState.Sliding;
        }
    }


    IEnumerator PatronStateMachine()
    {
        while (true)
        {
            switch (_patronStateController)
            {
                case Patron.PatronState.Spawn:
                    Spawn();
                    break;
                case Patron.PatronState.Walking:
                    Walking();
                    break;
                case Patron.PatronState.Idle:
                    Idle();
                    break;
                case Patron.PatronState.Sliding:
                    Sliding();
                    break;
                case Patron.PatronState.Drinking:
                    Drinking();
                    break;
            }
            yield return null;
        }
    }

    private void Spawn()
    {

    }

    private void Walking()
    {
        if(_patronRigidbody.velocity == Vector3.zero)
        {
            _patronRigidbody.velocity = transform.right * _patronWalkSpeed;
        }

        _patronWalkingTimer += (1f * Time.deltaTime);

        if(_patronWalkingTimer >= _patronWalkingDelay)
        {
            _patronStateController = Patron.PatronState.Idle;
            _patronWalkingTimer = 0;
        }       
    }

    private void Idle()
    {
        if (_patronRigidbody.velocity != Vector3.zero)
        {
            _patronRigidbody.velocity = Vector3.zero;
        }

        _patronWalkingTimer += (1f * Time.deltaTime);

        if (_patronWalkingTimer >= _patronWalkingDelay)
        {
            _patronStateController = Patron.PatronState.Walking;
            _patronWalkingTimer = 0;
        }
    }

    private void Sliding()
    {
        if(_patronRigidbody.velocity != (transform.right * -1) * _patronSlideSpeed)
        {
            _patronRigidbody.velocity = (transform.right * -1) * _patronSlideSpeed;
        }

        _patronSlideTimer += (1f * Time.deltaTime);

        if(_patronSlideTimer >= _patronSlideTime)
        {
            _patronStateController = Patron.PatronState.Drinking;
            _patronSlideTimer = 0;
        }
    }

    private void Drinking()
    {
        if (_patronRigidbody.velocity != Vector3.zero)
        {
            _patronRigidbody.velocity = Vector3.zero;
        }

        if(_patronCurrentBeer._beerStateController != Beer.BeerState.Drink)
        {
            _patronCurrentBeer._beerStateController = Beer.BeerState.Drink;
        }

        if (_patronRigidbody.velocity == Vector3.zero)
        {
            _patronDrinkTimer += (1f * Time.deltaTime);
        }

        if(_patronDrinkTimer >= _patronDrinkTime)
        {
            _patronCurrentBeer.transform.parent = null;
            _patronCurrentBeer._beerStateController = Beer.BeerState.ReturnToBartender;
            _patronCurrentBeer = null;
            _patronDrinkTimer = 0;
            _patronStateController = Patron.PatronState.Walking;
        }
    }
}
