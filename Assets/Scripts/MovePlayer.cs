using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public static float _speed;

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        float movez = Input.GetAxis("Vertical") * Time.deltaTime * _speed;
        Vector3 direction = new Vector3(moveX, 0, movez);
        transform.position += direction;
    }
}
