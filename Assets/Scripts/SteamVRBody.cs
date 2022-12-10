using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVRBody : MonoBehaviour
{
    public Transform vrHead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(vrHead.position.x, 0.5f, vrHead.position.z);
    }
}
