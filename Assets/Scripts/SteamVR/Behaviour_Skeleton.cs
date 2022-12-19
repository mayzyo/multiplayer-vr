using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Valve.VR;

public class Behaviour_Skeleton : SteamVR_Behaviour_Skeleton
{   
    protected override void Awake() {
    }

    public void initialise() {
        SteamVR.Initialize();

        AssignBonesArray();

        proximals = new Transform[] { thumbProximal, indexProximal, middleProximal, ringProximal, pinkyProximal };
        middles = new Transform[] { thumbMiddle, indexMiddle, middleMiddle, ringMiddle, pinkyMiddle };
        distals = new Transform[] { thumbDistal, indexDistal, middleDistal, ringDistal, pinkyDistal };
        tips = new Transform[] { thumbTip, indexTip, middleTip, ringTip, pinkyTip };
        auxs = new Transform[] { thumbAux, indexAux, middleAux, ringAux, pinkyAux };

        CheckSkeletonAction();
    }
}
