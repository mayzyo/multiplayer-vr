using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR;

public class PlayerBody : NetworkBehaviour
{
    public Transform parentObject;
    public bool isVr = true;

    [SerializeField]
    private GameObject head;
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private GameObject body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (IsLocalPlayer)
        {
            head.SetActive(false);
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            if (!isVr)
            {
                body.SetActive(true);
                if (parentObject)
                {
                    this.transform.position = new Vector3(parentObject.position.x, 0.5f, parentObject.position.z);
                }
            }
            else
            {
                MapPosition(head.transform, XRNode.Head);
                MapPosition(leftHand.transform, XRNode.LeftHand);
                MapPosition(rightHand.transform, XRNode.RightHand);
            }

        }


    }

    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;
    }
}
