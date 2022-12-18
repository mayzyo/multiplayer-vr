using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject vr;
    [SerializeField] private GameObject oculus;
    [SerializeField] private GameObject desktop;
    [SerializeField] private GameObject body;

    public static int playerType = 0;
    private GameObject go;

    
    public override void OnNetworkSpawn()
    {
       if (!IsClient) return;

        if(playerType == 0) {
            go = Instantiate(vr, Vector3.zero, Quaternion.identity);
            SpawnPlayerServerRpc();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            player.GetComponent<PlayerBody>().parentObject = camera.GetComponent<Transform>();

        } else if(playerType == 1) {
            go = Instantiate(oculus, Vector3.zero, Quaternion.identity);
        } else {
            go = Instantiate(desktop, Vector3.zero, Quaternion.identity);
            SpawnPlayerServerRpc();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            player.GetComponent<PlayerBody>().parentObject = camera.GetComponent<Transform>();
        }
    }

    [ServerRpc(RequireOwnership=false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ServerRpcParams serverRpcParams = default) {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            // Do things for this client
        }

        NetworkObject networkObject = Instantiate(body).GetComponent<NetworkObject>();
        networkObject.SpawnAsPlayerObject(clientId, true);

        // if (!IsServer) return;
        // ClientRpcParams clientRpcParams = new ClientRpcParams
        // {
        //     Send = new ClientRpcSendParams
        //     {
        //         TargetClientIds = new ulong[]{clientId}
        //     }
        // };

        // AddBodyClientRpc(clientRpcParams);

        // if(IsServer) {
        //     if(playerType == 0) {
        //         go.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        //     } else if(playerType == 1) {
        //         go.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        //     } else {
        //         go.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        //     }
        // }

        // NetworkObject networkObject = Instantiate(oculus).GetComponent<NetworkObject>();
        // networkObject.SpawnAsPlayerObject(clientId, true);

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

    [ClientRpc]
    private void AddBodyClientRpc(ClientRpcParams clientRpcParams = default)
    {
        if (IsOwner) return;

        // Run your client-side logic here!!
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Transform>().SetParent(go.GetComponent<Transform>());
    }
}
