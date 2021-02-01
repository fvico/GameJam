using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(CharacterController))]
public class MovePlayer : MonoBehaviour
{
    public static float _speed;
    public static bool _canPlayer;
    public static bool _paused = false;
    public static float _actualLevel = 0;
    public static bool _win; 
    public static AudioSource emisor;
    public static bool reseteando = false;

    private CharacterController myCC;
    private MeshRenderer meshCube;

    private float alturaMax = 100f;
    private float turnSmoothVelocity;
    private bool subiendoFrontalmente = false;
    private bool subiendoLaterlamente = false;

    


    [SerializeField]
    Camera cam;
    [SerializeField]
    GameObject meshParticles;
    [SerializeField]
    GameObject[] cerebrosCamara;
    [SerializeField]
    GameObject panelPausa;
    [SerializeField]
    Timer timer;
    [SerializeField]
    Animator _FadeInOut;
    [SerializeField]
    AudioClip audioMuerte;
    [SerializeField]
    AudioClip audioAndar;
    [SerializeField]
    LayerMask stickyLayer;
    [SerializeField]
    float verticalSpeed;
    [SerializeField]
    float verticalGravity;
    [SerializeField]
    float verticalFreeFallingGravity;
    [SerializeField]
    float turnSmoothTime = 0.1f;



    private void Start()
    {
        SetLevel(PlayerPrefs.GetInt("LevelToLoad"));
        myCC = GetComponent<CharacterController>();
        cam = Camera.main;
        meshCube = GetComponentInChildren<MeshRenderer>();
        _canPlayer = true;
        timer.StartTimer();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _win = false;
        panelPausa.GetComponent<PauseMenu>().InicializarPanel();
        panelPausa.GetComponent<PauseMenu>().AjustarValores();
        panelPausa.SetActive(false);
        emisor = GetComponent<AudioSource>();
        reseteando = false;
        emisor.loop = true;
        emisor.clip = audioAndar;
        emisor.Play();
        //emisor.PlayOneShot(audioAndar);

    }


    private void Update()
    {
        if (!reseteando)
        {
            if (DetectLight._inLight)
            {
                emisor.loop = false;
                emisor.Pause();
            }
            else
            {
                if (!emisor.isPlaying)
                {
                    emisor.loop = true;
                    emisor.UnPause();
                }
            }
        }

        if (_canPlayer)
        {
                                                //PAUSA\\

            if (Input.GetKeyDown(KeyCode.Escape) && !_win)
            {
                _paused = !_paused;
            }

            if (_paused)
            {
                Time.timeScale = 0;
                panelPausa.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                panelPausa.SetActive(false);



                                                //MOVIMIENTO\\

                float horizontal, vertical;
                Vector3 direccion = Vector3.zero;

                switch (cam.transform.eulerAngles.y)
                {
                    case (0f): //Movimiento con la camara en 0 grados en y
                        horizontal = Input.GetAxisRaw("Horizontal");
                        vertical = Input.GetAxisRaw("Vertical");
                        if (!subiendoFrontalmente && !subiendoLaterlamente)
                        {
                            direccion = new Vector3(horizontal, 0, vertical).normalized;
                            if (direccion.magnitude != 0)
                            {
                                float targetAngle = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg;
                                float angle = Mathf.SmoothDampAngle(meshCube.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                                meshCube.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
                            }
                            else
                            {
                                float angle = Mathf.SmoothDampAngle(meshCube.transform.eulerAngles.y, 0f, ref turnSmoothVelocity, turnSmoothTime);
                                meshCube.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
                            }


                        }
                        else if (subiendoFrontalmente)
                        {
                            direccion = new Vector3(horizontal, 0, 0).normalized;
                        }
                        else if (subiendoLaterlamente)
                        {
                            direccion = new Vector3(0, 0, vertical).normalized;
                        }
                        break;

                    case (90f): //Movimiento con la camara en 90 grados en y
                        horizontal = Input.GetAxisRaw("Horizontal");
                        vertical = Input.GetAxisRaw("Vertical");
                        if (!subiendoFrontalmente && !subiendoLaterlamente)
                        {
                            direccion = new Vector3(vertical, 0, -horizontal).normalized;
                            if (direccion.magnitude != 0)
                            {
                                float targetAngle = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg;
                                float angle = Mathf.SmoothDampAngle(meshCube.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                                meshCube.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
                            }
                            else
                            {
                                float angle = Mathf.SmoothDampAngle(meshCube.transform.eulerAngles.y, 90f, ref turnSmoothVelocity, turnSmoothTime);
                                meshCube.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
                            }
                        }
                        else if (subiendoFrontalmente)
                        {
                            direccion = new Vector3(vertical, 0, 0).normalized;
                        }
                        else if (subiendoLaterlamente)
                        {
                            direccion = new Vector3(0, 0, -horizontal).normalized;
                        }
                        break;

                    case (180f): //Movimiento con la camara en 180 grados en y
                        horizontal = Input.GetAxisRaw("Horizontal");
                        vertical = Input.GetAxisRaw("Vertical");
                        if (!subiendoFrontalmente && !subiendoLaterlamente)
                        {
                            direccion = new Vector3(-horizontal, 0, -vertical).normalized;
                            if (direccion.magnitude != 0)
                            {
                                float targetAngle = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg;
                                float angle = Mathf.SmoothDampAngle(meshCube.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                                meshCube.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
                            }
                            else
                            {
                                float angle = Mathf.SmoothDampAngle(meshCube.transform.eulerAngles.y, 180f, ref turnSmoothVelocity, turnSmoothTime);
                                meshCube.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
                            }
                        }
                        else if (subiendoFrontalmente)
                        {
                            direccion = new Vector3(-horizontal, 0, 0).normalized;
                        }
                        else if (subiendoLaterlamente)
                        {
                            direccion = new Vector3(0, 0, -vertical).normalized;
                        }
                        break;


                    case (270f): //Movimiento con la camara en 270 grados en y
                        horizontal = Input.GetAxisRaw("Horizontal");
                        vertical = Input.GetAxisRaw("Vertical");
                        if (!subiendoFrontalmente && !subiendoLaterlamente)
                        {
                            direccion = new Vector3(-vertical, 0, horizontal).normalized;
                            if (direccion.magnitude != 0)
                            {
                                float targetAngle = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg;
                                float angle = Mathf.SmoothDampAngle(meshCube.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                                meshCube.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
                            }
                            else
                            {
                                float angle = Mathf.SmoothDampAngle(meshCube.transform.eulerAngles.y, 270f, ref turnSmoothVelocity, turnSmoothTime);
                                meshCube.transform.rotation = Quaternion.Euler(-90f, angle, 0f);
                            }
                        }
                        else if (subiendoFrontalmente)
                        {
                            direccion = new Vector3(-vertical, 0, 0).normalized;
                        }
                        else if (subiendoLaterlamente)
                        {
                            direccion = new Vector3(0, 0, horizontal).normalized;
                        }
                        break;
                }

                if (direccion.magnitude >= 0.1f)  //Se aplica el movimiento tras calcularlo
                {
                    myCC.Move(direccion.normalized * _speed * Time.deltaTime);
                    if (!reseteando)
                    {
                        if (!emisor.isPlaying)
                        {
                            emisor.loop = true;
                            emisor.UnPause();
                        }
                    }
                }
                else
                {
                    if (!reseteando)
                    {
                        emisor.loop = false;
                        emisor.Pause();
                    }
                }



                //SUBIR PAREDES

                RaycastHit hit1, hit2, hit3, hit4, hit5, hit6, hit7, hit8;
                float distanciaRaycast = 0.6f;
                Vector3 origen = new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z);
                Vector3 tope = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                Vector3 frontal = new Vector3(0f, 0f, distanciaRaycast);
                Vector3 derecha = new Vector3(distanciaRaycast, 0f, 0f);
                Vector3 trasera = new Vector3(0f, 0f, -distanciaRaycast);
                Vector3 izquierda = new Vector3(-distanciaRaycast, 0f, 0f);

                Debug.DrawRay(origen, frontal, Color.green);
                Debug.DrawRay(tope, frontal, Color.green);
                Debug.DrawRay(origen, derecha, Color.green);
                Debug.DrawRay(tope, derecha, Color.green);
                Debug.DrawRay(origen, trasera, Color.green);
                Debug.DrawRay(tope, trasera, Color.green);
                Debug.DrawRay(origen, izquierda, Color.green);
                Debug.DrawRay(tope, izquierda, Color.green);

                if (Physics.Raycast(origen, Vector3.forward, out hit1, distanciaRaycast, stickyLayer) && !DetectLight._inLight)   //Tocamos con la pared de enfrente
                {
                    if (Physics.Raycast(tope, Vector3.forward, out hit2, distanciaRaycast, stickyLayer))
                    {
                        alturaMax = hit2.point.y;
                    }

                    if (hit1.point.y <= alturaMax)
                    {
                        subiendoFrontalmente = true;
                        if (Input.GetKey(KeyCode.Space))
                        {
                            meshCube.transform.rotation = Quaternion.AngleAxis(180f, Vector3.right);            //Rotacion subiendo del mesh
                            meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);      //Rotacion subiendo del sistema de particulas
                            myCC.Move(Vector3.up * verticalSpeed * Time.deltaTime);
                        }
                        else
                        {
                            meshCube.transform.rotation = Quaternion.AngleAxis(180f, Vector3.right);            //Rotacion subiendo del mesh
                            meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);      //Rotacion subiendo del sistema de particulas
                            myCC.Move(Vector3.down * verticalGravity * Time.deltaTime);
                            subiendoFrontalmente = false;
                        }
                    }
                    else
                    {
                        subiendoFrontalmente = false;
                    }
                }
                else if (Physics.Raycast(origen, Vector3.right, out hit3, distanciaRaycast, stickyLayer) && !DetectLight._inLight) //Tocamos con la pared de la derecha
                {
                    if (Physics.Raycast(tope, Vector3.right, out hit4, distanciaRaycast, stickyLayer))
                    {
                        alturaMax = hit4.point.y;
                    }

                    if (hit3.point.y <= alturaMax)
                    {
                        subiendoLaterlamente = true;
                        if (Input.GetKey(KeyCode.Space))
                        {
                            meshCube.transform.rotation = Quaternion.AngleAxis(180f, Vector3.right);            //Rotacion subiendo del mesh
                            meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);      //Rotacion subiendo del sistema de particulas
                            myCC.Move(Vector3.up * verticalSpeed * Time.deltaTime);
                        }
                        else
                        {
                            meshCube.transform.rotation = Quaternion.AngleAxis(180f, Vector3.right);            //Rotacion subiendo del mesh
                            meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);      //Rotacion subiendo del sistema de particulas
                            myCC.Move(Vector3.down * verticalGravity * Time.deltaTime);
                            subiendoLaterlamente = false;
                        }
                    }
                    else
                    {
                        subiendoLaterlamente = false;
                    }
                }
                else if (Physics.Raycast(origen, Vector3.back, out hit5, distanciaRaycast, stickyLayer) && !DetectLight._inLight) //Tocamos con la pared de atras
                {
                    if (Physics.Raycast(tope, Vector3.back, out hit6, distanciaRaycast, stickyLayer))
                    {
                        alturaMax = hit6.point.y;
                    }

                    if (hit5.point.y <= alturaMax)
                    {
                        subiendoFrontalmente = true;
                        if (Input.GetKey(KeyCode.Space))
                        {
                            meshCube.transform.rotation = Quaternion.AngleAxis(180f, Vector3.right);            //Rotacion subiendo del mesh
                            meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);      //Rotacion subiendo del sistema de particulas
                            myCC.Move(Vector3.up * verticalSpeed * Time.deltaTime);
                        }
                        else
                        {
                            meshCube.transform.rotation = Quaternion.AngleAxis(180f, Vector3.right);            //Rotacion subiendo del mesh
                            meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);      //Rotacion subiendo del sistema de particulas
                            myCC.Move(Vector3.down * verticalGravity * Time.deltaTime);
                            subiendoFrontalmente = false;
                        }
                    }
                    else
                    {
                        subiendoFrontalmente = false;
                    }
                }
                else if (Physics.Raycast(origen, Vector3.left, out hit7, distanciaRaycast, stickyLayer) && !DetectLight._inLight) //Tocamos con la pared de la izquierda
                {
                    if (Physics.Raycast(tope, Vector3.left, out hit8, distanciaRaycast, stickyLayer))
                    {
                        alturaMax = hit8.point.y;
                    }

                    if (hit7.point.y <= alturaMax)
                    {
                        subiendoLaterlamente = true;
                        if (Input.GetKey(KeyCode.Space))
                        {
                            meshCube.transform.rotation = Quaternion.AngleAxis(180f, Vector3.right);            //Rotacion subiendo del mesh
                            meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);      //Rotacion subiendo del sistema de particulas
                            myCC.Move(Vector3.up * verticalSpeed * Time.deltaTime);
                        }
                        else
                        {
                            meshCube.transform.rotation = Quaternion.AngleAxis(180f, Vector3.right);            //Rotacion subiendo del mesh
                            meshParticles.transform.rotation = Quaternion.AngleAxis(-180f, Vector3.right);      //Rotacion subiendo del sistema de particulas
                            myCC.Move(Vector3.down * verticalGravity * Time.deltaTime);
                            subiendoLaterlamente = false;
                        }
                    }
                    else
                    {
                        subiendoLaterlamente = false;
                    }
                }
                else
                {
                    myCC.Move(Vector3.down * verticalFreeFallingGravity * Time.deltaTime);
                    subiendoFrontalmente = false;
                    subiendoLaterlamente = false;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TriggerCambiaAngulos"))
        {

            TransitionData td = other.gameObject.GetComponent<TransitionData>();
            CambiarAngulo(td);

        }
        if (other.gameObject.CompareTag("Respawn"))
        {
            _FadeInOut.SetTrigger("IsFadeIn");
            StartCoroutine(ResetCoroutine());

        }
    }

    IEnumerator ResetCoroutine()
    {

        reseteando = true;
        emisor.Stop();
        emisor.mute = false;
        emisor.loop = false;
        emisor.PlayOneShot(audioMuerte);

        timer.StopTimer();


        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public static void SetLevel(int level)
    {
        PlayerPrefs.SetInt("ActualLevel", level);
        GameObject padreDeTodos = GameObject.FindGameObjectWithTag("PadreDeTodos");
        if (level < padreDeTodos.transform.childCount)
        {
            PlayerPrefs.SetInt("LevelToLoad", level);
            for (int i = 0; i < padreDeTodos.transform.childCount; i++)
            {
                padreDeTodos.transform.GetChild(i).gameObject.SetActive(false);
            }
            padreDeTodos.transform.GetChild(level).gameObject.SetActive(true);
        }
    }


}
