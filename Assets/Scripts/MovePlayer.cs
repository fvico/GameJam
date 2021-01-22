using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]
public class MovePlayer : MonoBehaviour
{
    public static float _speed;
    
    private CharacterController myCC;


    [SerializeField]
    Camera cam;
    [SerializeField]
    GameObject[] cerebrosCamara;


    private void Start()
    {
        myCC = GetComponent<CharacterController>();
        cam = Camera.main;
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
        //transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
        float horizontal, vertical;
        Vector3 direccion = Vector3.zero;

        switch (cam.transform.eulerAngles.y)
        {
            case (0f): //
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                direccion = new Vector3(horizontal, 0, vertical).normalized;
                break;

            case (90f):
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                direccion = new Vector3(vertical, 0, -horizontal).normalized;
                break;

            case (180f):
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                direccion = new Vector3(-horizontal, 0, -vertical).normalized;
                break;


            case (270f): // x = 8  y = 8  z = 0
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                direccion = new Vector3(- vertical, 0, horizontal).normalized;
                break;
        }





        if (direccion.magnitude >= 0.1f)
        {
            myCC.Move(direccion.normalized * _speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TriggerCambiaAngulos"))
        {
            print("Hola");
            TransitionData td = other.gameObject.GetComponent<TransitionData>();

            /*print("H");
            cam.transform.rotation = Quaternion.Euler(45f, 0f, 0f);
            cam.transform.position = new Vector3(0f, 8f, -8f);*/
            /*cerebrosCamara[0].gameObject.SetActive(false);
            cerebrosCamara[2].gameObject.SetActive(true);*/
            switch (td.origen)
            {
                case (Orientation.CeroGrados):
                    if(cam.transform.eulerAngles.y == 0f)
                    {

                        cerebrosCamara[0].gameObject.SetActive(false);

                        switch (td.destino) {
                            case (Orientation.NoventaGrados):
                                cerebrosCamara[1].gameObject.SetActive(true);
                                break;

                            case (Orientation.CientoOchentaGrados):
                                cerebrosCamara[2].gameObject.SetActive(true);
                                break;

                            case (Orientation.DoscientosSetentaGrados):
                                cerebrosCamara[3].gameObject.SetActive(true);
                                break;
                        }
                    }
                    else
                    {
                        switch (cam.transform.eulerAngles.y)
                        {
                            case (90f):
                                cerebrosCamara[1].gameObject.SetActive(false);
                                break;
                            case (180f):
                                cerebrosCamara[2].gameObject.SetActive(false);
                                break;
                            case (270f):
                                cerebrosCamara[3].gameObject.SetActive(false);
                                break;
                        }

                        cerebrosCamara[0].gameObject.SetActive(true);
                    }
                    break;

                case (Orientation.NoventaGrados):
                    if (cam.transform.eulerAngles.y == 90f)
                    {

                        cerebrosCamara[1].gameObject.SetActive(false);

                        switch (td.destino)
                        {
                            case (Orientation.CeroGrados):
                                cerebrosCamara[0].gameObject.SetActive(true);
                                break;

                            case (Orientation.CientoOchentaGrados):
                                cerebrosCamara[2].gameObject.SetActive(true);
                                break;

                            case (Orientation.DoscientosSetentaGrados):
                                cerebrosCamara[3].gameObject.SetActive(true);
                                break;
                        }
                    }
                    else
                    {
                        switch (cam.transform.eulerAngles.y)
                        {
                            case (0f):
                                cerebrosCamara[0].gameObject.SetActive(false);
                                break;
                            case (180f):
                                cerebrosCamara[2].gameObject.SetActive(false);
                                break;
                            case (270f):
                                cerebrosCamara[3].gameObject.SetActive(false);
                                break;
                        }

                        cerebrosCamara[1].gameObject.SetActive(true);
                    }
                    break;

                case (Orientation.CientoOchentaGrados):
                    if (cam.transform.eulerAngles.y == 180f)
                    {

                        cerebrosCamara[2].gameObject.SetActive(false);

                        switch (td.destino)
                        {
                            case (Orientation.CeroGrados):
                                cerebrosCamara[0].gameObject.SetActive(true);
                                break;

                            case (Orientation.NoventaGrados):
                                cerebrosCamara[1].gameObject.SetActive(true);
                                break;
                                
                            case (Orientation.DoscientosSetentaGrados):
                                cerebrosCamara[3].gameObject.SetActive(true);
                                break;
                        }
                    }
                    else
                    {
                        switch (cam.transform.eulerAngles.y)
                        {
                            case (0f):
                                cerebrosCamara[0].gameObject.SetActive(false);
                                break;
                            case (90f):
                                cerebrosCamara[1].gameObject.SetActive(false);
                                break;
                            case (270f):
                                cerebrosCamara[3].gameObject.SetActive(false);
                                break;
                        }

                        cerebrosCamara[2].gameObject.SetActive(true);
                    }
                    break;

                case (Orientation.DoscientosSetentaGrados):
                    if (cam.transform.eulerAngles.y == 270f)
                    {

                        cerebrosCamara[3].gameObject.SetActive(false);

                        switch (td.destino)
                        {
                            case (Orientation.CeroGrados):
                                cerebrosCamara[0].gameObject.SetActive(true);
                                break;

                            case (Orientation.NoventaGrados):
                                cerebrosCamara[1].gameObject.SetActive(true);
                                break;

                            case (Orientation.CientoOchentaGrados):
                                cerebrosCamara[2].gameObject.SetActive(true);
                                break;
                        }
                    }
                    else
                    {
                        switch (cam.transform.eulerAngles.y)
                        {
                            case (0f):
                                cerebrosCamara[0].gameObject.SetActive(false);
                                break;
                            case (90f):
                                cerebrosCamara[1].gameObject.SetActive(false);
                                break;
                            case (180f):
                                cerebrosCamara[2].gameObject.SetActive(false);
                                break;
                        }

                        cerebrosCamara[3].gameObject.SetActive(true);
                    }
                    break;
            }

        }
    }
}
