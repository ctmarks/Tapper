using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPatronTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Patron")
        {
            Debug.Log("You Lose Patron");
        }
    }
}
