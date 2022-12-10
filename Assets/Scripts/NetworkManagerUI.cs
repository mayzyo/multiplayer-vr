using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    
    [SerializeField] private Button hostVRBtn;
    [SerializeField] private Button clientVRBtn;

    [SerializeField] private Button hostOculusBtn;
    [SerializeField] private Button clientOculusBtn;

    [SerializeField] private Button hostDesktopBtn;
    [SerializeField] private Button clientDesktopBtn;

    [SerializeField] private PlayerSpawner playerSpawner;

    private void Awake()
    {
        serverBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });

        initialiseButtonSet(hostVRBtn, clientVRBtn, 0);
        initialiseButtonSet(hostOculusBtn, clientOculusBtn, 1);
        initialiseButtonSet(hostDesktopBtn, clientDesktopBtn, 2);
    }

    private void initialiseButtonSet(Button hostBtn, Button clientBtn, int id) {
        hostBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            playerSpawner.SpawnPlayerServerRpc();
        });

        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            playerSpawner.SpawnPlayerServerRpc();
        });
    }
}
