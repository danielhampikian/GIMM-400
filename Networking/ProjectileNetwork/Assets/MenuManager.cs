using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuManager : NetworkBehaviour
{
    public Dropdown playerSelect;
    public GameObject[] playerArray;
    public GameObject serverMenu;
    public Text serverIP;

    public void SelectPlayer()
    {
        NetworkManager.singleton.playerPrefab = playerArray[playerSelect.value];
    }

    public void StartServer()
    {
        NetworkManager.singleton.StartHost();
        serverMenu.SetActive(false);
    }

    public void StartClient()
    {
        getIP();
        NetworkManager.singleton.StartClient();
        serverMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            serverMenu.SetActive(true);
        }
    }
    public void getIP(){
        if (serverIP.text.Length > 0 && serverIP.text!= null){
            NetworkManager.singleton.networkAddress = serverIP.text;
        }
    }

}
