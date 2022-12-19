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

    public static PlayerSpawner Instance { get; private set; }

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }


    public override void OnNetworkSpawn()
    {
        if(playerType == 0) {
            go = Instantiate(vr, Vector3.zero, Quaternion.identity);
            SpawnPlayerServerRpc();
        } else if(playerType == 1) {
            go = Instantiate(vr, Vector3.zero, Quaternion.identity);
            SpawnPlayerServerRpc();
        } else {
            go = Instantiate(desktop, Vector3.zero, Quaternion.identity);
            SpawnPlayerServerRpc();
        }
    }

    [ServerRpc(RequireOwnership=false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ServerRpcParams serverRpcParams = default) {
        if (!IsServer) return;

        var clientId = serverRpcParams.Receive.SenderClientId;

        NetworkObject networkObject = Instantiate(body).GetComponent<NetworkObject>();
        networkObject.SpawnAsPlayerObject(clientId, true);

        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[]{clientId}
            }
        };

        AddBodyClientRpc(clientRpcParams);
    }

    [ClientRpc]
    private void AddBodyClientRpc(ClientRpcParams clientRpcParams = default)
    {
        // Run your client-side logic here!!
        if(playerType == 0) {
            //PlayerBody player = NetworkManager.LocalClient.PlayerObject.GetComponent<PlayerBody>();
            //GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            //player.parentObject = camera.GetComponent<Transform>();
        } else if(playerType == 1) {
            // Oculus logic goes here. Just Tag the object that actually moves with "MainCamera"
            //PlayerBody player = NetworkManager.LocalClient.PlayerObject.GetComponent<PlayerBody>();
            //GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            //player.parentObject = camera.GetComponent<Transform>();
        } else {
            PlayerBody player = NetworkManager.LocalClient.PlayerObject.GetComponent<PlayerBody>();
            player.isVr = false;
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            player.parentObject = camera.GetComponent<Transform>();
        }
    }

    private GameObject temp;

    public void CallSpawn(GameObject go) {
        this.temp = go;
        SpawnServerRpc();
    }

    [ServerRpc(RequireOwnership=false)] //server owns this object but client can request a spawn
    public void SpawnServerRpc(ServerRpcParams serverRpcParams = default) {
        Debug.Log("Spawn server rpc");
        if (!IsServer) return;

        var clientId = serverRpcParams.Receive.SenderClientId;

        NetworkObject networkObject = Instantiate(temp).GetComponent<NetworkObject>();
        networkObject.Spawn(true);
        Debug.Log(networkObject);
    }
}
