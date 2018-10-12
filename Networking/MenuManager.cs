using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuManager : NetworkBehaviour
{
    public Dropdown playerSelect;
    public GameObject[] playerArray;

    public void SelectPlayer()
    {
        Debug.Log("Player select value: " + playerSelect.value);

        NetworkManager.singleton.playerPrefab = playerArray[playerSelect.value];
    }

    public void StartServer()
    {
        NetworkManager.singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.singleton.StartClient();
    }
}
