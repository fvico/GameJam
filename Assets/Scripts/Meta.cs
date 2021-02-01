using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    [SerializeField]
    PauseMenu _pauseMenu;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (MovePlayer._actualLevel >= MenuNiveles._nivelesSuperados)
            {
                MenuNiveles._nivelesSuperados++;
            }
            /*switch (MovePlayer._actualLevel)
            {
                case (1):
                    if(MovePlayer._actualLevel >= MenuNiveles._nivelesSuperados)
                    {
                        MenuNiveles._nivelesSuperados++;
                    }
                    break;

                case (2):
                    if (MovePlayer._actualLevel >= MenuNiveles._nivelesSuperados)
                    {
                        MenuNiveles._nivelesSuperados++;
                    }
                    break;

                case (3):
                    if (MovePlayer._actualLevel >= MenuNiveles._nivelesSuperados)
                    {
                        MenuNiveles._nivelesSuperados++;
                    }
                    break;

                case (4):
                    if (MovePlayer._actualLevel >= MenuNiveles._nivelesSuperados)
                    {
                        MenuNiveles._nivelesSuperados++;
                    }
                    break;

                case (5):
                    if (MovePlayer._actualLevel >= MenuNiveles._nivelesSuperados)
                    {
                        MenuNiveles._nivelesSuperados++;
                    }
                    break;
            }*/
            _pauseMenu.Win();
            MovePlayer._win = true;
        }
    }
}
