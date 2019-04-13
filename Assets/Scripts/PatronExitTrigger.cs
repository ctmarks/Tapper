using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PatronExitTrigger.cs
/// Christopher Marks
/// 4/12/2019
/// </summary>

public class PatronExitTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Patron")
        {
            Destroy(other.gameObject);
        }
    }
}
