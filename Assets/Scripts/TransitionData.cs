using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation { CeroGrados, NoventaGrados, CientoOchentaGrados, DoscientosSetentaGrados };


public class TransitionData : MonoBehaviour
{
    
    public Orientation origen = Orientation.CeroGrados;
    public Orientation destino = Orientation.NoventaGrados;


}
