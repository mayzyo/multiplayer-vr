using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject vr;
    [SerializeField] private GameObject oculus;
    [SerializeField] private GameObject desktop;

    [ServerRpc(RequireOwnership=false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ServerRpcParams serverRpcParams = default) {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            // Do things for this client
        }

        NetworkObject networkObject = Instantiate(oculus).GetComponent<NetworkObject>();
        networkObject.SpawnAsPlayerObject(clientId, true);

        // GameObject newPlayer;

        // if (prefabId == 0)
        //      newPlayer = Instantiate(vr) as GameObject;
        // else if(prefabId == 1)
        //     newPlayer = Instantiate(oculus) as GameObject;
        // else
        //     newPlayer = Instantiate(desktop) as GameObject;

        // var netObj = newPlayer.GetComponent<NetworkObject>();
        // newPlayer.SetActive(true);
        // netObj.SpawnAsPlayerObject(clientId, true);
    }
}
