using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rejillas : MonoBehaviour
{
    [SerializeField]
    BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider.enabled = true;
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player")
        {
           // _boxCollider.enabled = true;
            if (DetectLight._inLight)
            {
                _boxCollider.enabled = true;
            }
            else if(!DetectLight._inLight)
            {
                _boxCollider.enabled = false;
            }
        }
    }
}
