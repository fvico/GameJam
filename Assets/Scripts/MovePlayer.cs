using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]
public class MovePlayer : MonoBehaviour
{
    public static float _speed;
    
    private CharacterController myCC;

    private void Start()
    {
        myCC = GetComponent<CharacterController>();
    }

    /*private void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        float movez = Input.GetAxis("Vertical") * Time.deltaTime * _speed;
        Vector3 direction = new Vector3(moveX, 0, movez);
        transform.position += direction;
    }*/

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direccion = new Vector3(horizontal, 0, vertical).normalized;

        if (direccion.magnitude >= 0.1f)
        {
            myCC.Move(direccion.normalized * _speed * Time.deltaTime);
        }
    }
}
