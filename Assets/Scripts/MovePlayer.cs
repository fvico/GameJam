using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]
public class MovePlayer : MonoBehaviour
{
    public static float _speed;
    
    private CharacterController myCC;
    private MeshRenderer meshCube;


    [SerializeField]
    Camera cam;
    [SerializeField]
    GameObject[] cerebrosCamara;
    [SerializeField]
    LayerMask stickyLayer;
    [SerializeField]
    float verticalSpeed;
    [SerializeField]
    float verticalGravity;
    [SerializeField]
    float verticalFreeFallingGravity;
    [SerializeField]
    GameObject meshParticles;


    private void Start()
    {
        myCC = GetComponent<CharacterController>();
        cam = Camera.main;
        meshCube = GetComponentInChildren<MeshRenderer>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {

        //MOVIMIENTO

        float horizontal, vertical;
        Vector3 direccion = Vector3.zero;

        switch (cam.transform.eulerAngles.y)
        {
            case (0f): //Movimiento con la camara en 0 grados en y
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                direccion = new Vector3(horizontal, 0, vertical).normalized;
                break;

            case (90f): //Movimiento con la camara en 90 grados en y
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                direccion = new Vector3(vertical, 0, -horizontal).normalized;
                break;

            case (180f): //Movimiento con la camara en 180 grados en y
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                direccion = new Vector3(-horizontal, 0, -vertical).normalized;
                break;


            case (270f): //Movimiento con la camara en 270 grados en y
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                direccion = new Vector3(- vertical, 0, horizontal).normalized;
                break;
        }

        if (direccion.magnitude >= 0.1f)  //Se aplica el movimiento tras calcularlo
        {
            myCC.Move(direccion.normalized * _speed * Time.deltaTime);
        }


        //SUBIR PAREDES

        RaycastHit hit;
        Vector3 origen = new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z);
        Debug.DrawRay(origen, new Vector3(0f,0f,0.6f), Color.green);
        
        if (Physics.Raycast(origen, new Vector3(0f, 0f, 0.6f), out hit, 1f, stickyLayer)) {

            if (Input.GetKey(KeyCode.Space))
            {
                meshCube.transform.rotation = Quaternion.AngleAxis(180f, Vector3.right);
                meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);
                myCC.Move(Vector3.up * verticalSpeed * Time.deltaTime);
            }
            else
            {
                meshCube.transform.rotation = Quaternion.AngleAxis(-145f, Vector3.right);           //Rotacion por defecto del mesh
                meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);      //Rotacion por defecto del sistema de particulas
                myCC.Move(Vector3.down * verticalGravity * Time.deltaTime);
            }

        }
        else
        {
            meshCube.transform.rotation = Quaternion.AngleAxis(-145f, Vector3.right);
            meshParticles.transform.rotation = Quaternion.AngleAxis(-90f, Vector3.right);
            myCC.Move(Vector3.down *  verticalFreeFallingGravity * Time.deltaTime);
        }

        /*Debug.DrawRay(transform.position, new Vector3(0f, 0f, -0.6f), Color.green);
        if (Physics.Raycast(transform.position, new Vector3(0f, 0f, -0.6f), out hit, 1f, stickyLayer))
        {

            print("Hola");
            if (Input.GetKey(KeyCode.Space))
            {
                meshCube.transform.rotation = Quaternion.AngleAxis(180f, Vector3.right);
                meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);
                myCC.Move(Vector3.up * verticalSpeed * Time.deltaTime);
            }
            else
            {
                meshCube.transform.rotation = Quaternion.AngleAxis(-145f, Vector3.right);           //Rotacion por defecto del mesh
                meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);      //Rotacion por defecto del sistema de particulas
                myCC.Move(Vector3.down * verticalGravity * Time.deltaTime);
            }
        }
        else
        {
            meshCube.transform.rotation = Quaternion.AngleAxis(-145f, Vector3.right);
            meshParticles.transform.rotation = Quaternion.AngleAxis(-90f, Vector3.right);
            myCC.Move(Vector3.down  * verticalFreeFallingGravity * Time.deltaTime);
        }

        Debug.DrawRay(transform.position, new Vector3(0.6f, 0f, 0f), Color.green);
        if (Physics.Raycast(transform.position, new Vector3(0.6f, 0f, 0f), out hit, 1f, stickyLayer))
        {

            print("Hola");
        }

        Debug.DrawRay(transform.position, new Vector3(-0.6f, 0f, 0f), Color.green);
        if (Physics.Raycast(transform.position, new Vector3(-0.6f, 0f, 0f), out hit, 1f, stickyLayer))
        {

            print("Hola");
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TriggerCambiaAngulos"))
        {

            TransitionData td = other.gameObject.GetComponent<TransitionData>();
            CambiarAngulo(td);

        }
    }



    private void CambiarAngulo(TransitionData td)                               //Dependiendo de la posición inicial de la cámara, haremos una transición a la posición final
    {
        switch (td.origen)
        {
            case (Orientation.CeroGrados):                                      //Posicion de inicio de la transición 0 grados

                if (cam.transform.eulerAngles.y == 0f)                          //Si la posición inicial de la cámara es igual que la de la transición
                {

                    cerebrosCamara[0].gameObject.SetActive(false);

                    switch (td.destino)
                    {
                        case (Orientation.NoventaGrados):                       //Posición final de la transición 90 grados     
                            cerebrosCamara[1].gameObject.SetActive(true);
                            break;

                        case (Orientation.CientoOchentaGrados):                 //Posición final de la transición 180 grados   
                            cerebrosCamara[2].gameObject.SetActive(true);
                            break;

                        case (Orientation.DoscientosSetentaGrados):             //Posición final de la transición 270 grados   
                            cerebrosCamara[3].gameObject.SetActive(true);
                            break;
                    }
                }
                else
                {
                    switch (cam.transform.eulerAngles.y)                        //Si la posición inicial de la cámara es distinta que la de la transición(Se hace la transición al reves)
                    {
                        case (90f):                                             //Posición actual de la cámara 90 grados
                            cerebrosCamara[1].gameObject.SetActive(false);
                            break;

                        case (180f):                                            //Posición actual de la cámara 180 grados
                            cerebrosCamara[2].gameObject.SetActive(false);
                            break;

                        case (270f):                                            //Posición actual de la cámara 270 grados
                            cerebrosCamara[3].gameObject.SetActive(false);
                            break;
                    }

                    cerebrosCamara[0].gameObject.SetActive(true);
                }
                break;

            case (Orientation.NoventaGrados):                                   //Posicion de inicio de la transición 90 grados

                if (cam.transform.eulerAngles.y == 90f)                         //Si la posición inicial de la cámara es igual que la de la transición
                {

                    cerebrosCamara[1].gameObject.SetActive(false);

                    switch (td.destino)
                    {
                        case (Orientation.CeroGrados):                          //Posición final de la transición 0 grados  
                            cerebrosCamara[0].gameObject.SetActive(true);
                            break;

                        case (Orientation.CientoOchentaGrados):                 //Posición final de la transición 180 grados  
                            cerebrosCamara[2].gameObject.SetActive(true);
                            break;

                        case (Orientation.DoscientosSetentaGrados):             //Posición final de la transición 270 grados  
                            cerebrosCamara[3].gameObject.SetActive(true);
                            break;
                    }
                }
                else
                {
                    switch (cam.transform.eulerAngles.y)                        //Si la posición inicial de la cámara es distinta que la de la transición(Se hace la transición al reves)
                    {
                        case (0f):                                              //Posición actual de la cámara 0 grados
                            cerebrosCamara[0].gameObject.SetActive(false);
                            break;

                        case (180f):                                            //Posición actual de la cámara 180 grados
                            cerebrosCamara[2].gameObject.SetActive(false);
                            break;

                        case (270f):                                            //Posición actual de la cámara 270 grados
                            cerebrosCamara[3].gameObject.SetActive(false);
                            break;
                    }

                    cerebrosCamara[1].gameObject.SetActive(true);
                }
                break;

            case (Orientation.CientoOchentaGrados):                             //Posicion de inicio de la transición 180 grados

                if (cam.transform.eulerAngles.y == 180f)                        //Si la posición inicial de la cámara es igual que la de la transición
                {

                    cerebrosCamara[2].gameObject.SetActive(false);

                    switch (td.destino)
                    {
                        case (Orientation.CeroGrados):                          //Posición final de la transición 0 grados
                            cerebrosCamara[0].gameObject.SetActive(true);
                            break;

                        case (Orientation.NoventaGrados):                       //Posición final de la transición 90 grados
                            cerebrosCamara[1].gameObject.SetActive(true);
                            break;

                        case (Orientation.DoscientosSetentaGrados):             //Posición final de la transición 270 grados
                            cerebrosCamara[3].gameObject.SetActive(true);
                            break;
                    }
                }
                else
                {
                    switch (cam.transform.eulerAngles.y)                        //Si la posición inicial de la cámara es distinta que la de la transición(Se hace la transición al reves)
                    {
                        case (0f):                                              //Posición actual de la cámara 0 grados
                            cerebrosCamara[0].gameObject.SetActive(false);
                            break;

                        case (90f):                                             //Posición actual de la cámara 90 grados
                            cerebrosCamara[1].gameObject.SetActive(false);
                            break;

                        case (270f):                                            //Posición actual de la cámara 270 grados
                            cerebrosCamara[3].gameObject.SetActive(false);
                            break;
                    }

                    cerebrosCamara[2].gameObject.SetActive(true);
                }
                break;

            case (Orientation.DoscientosSetentaGrados):                         //Posicion de inicio de la transición 270 grados

                if (cam.transform.eulerAngles.y == 270f)                        //Si la posición inicial de la cámara es igual que la de la transición
                {

                    cerebrosCamara[3].gameObject.SetActive(false);

                    switch (td.destino)
                    {
                        case (Orientation.CeroGrados):                          //Posición final de la transición 0 grados
                            cerebrosCamara[0].gameObject.SetActive(true);
                            break;

                        case (Orientation.NoventaGrados):                       //Posición final de la transición 90 grados
                            cerebrosCamara[1].gameObject.SetActive(true);
                            break;

                        case (Orientation.CientoOchentaGrados):                 //Posición final de la transición 180 grados
                            cerebrosCamara[2].gameObject.SetActive(true);
                            break;
                    }
                }
                else
                {
                    switch (cam.transform.eulerAngles.y)                        //Si la posición inicial de la cámara es distinta que la de la transición(Se hace la transición al reves)
                    {
                        case (0f):                                              //Posición actual de la cámara 0 grados
                            cerebrosCamara[0].gameObject.SetActive(false);
                            break;

                        case (90f):                                             //Posición actual de la cámara 90 grados
                            cerebrosCamara[1].gameObject.SetActive(false);
                            break;

                        case (180f):                                            //Posición actual de la cámara 180 grados
                            cerebrosCamara[2].gameObject.SetActive(false);
                            break;
                    }

                    cerebrosCamara[3].gameObject.SetActive(true);
                }
                break;
        }
    }
}
