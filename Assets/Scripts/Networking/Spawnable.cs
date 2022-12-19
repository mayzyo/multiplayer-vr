using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Spawnable : NetworkBehaviour
{   
    void awake() {
        Debug.Log("ReADY");
    }
}
