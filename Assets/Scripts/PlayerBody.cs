using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public Transform parentObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(parentObject) {
            this.transform.position = new Vector3(parentObject.position.x, 0.5f, parentObject.position.z);
        }
    }
}
