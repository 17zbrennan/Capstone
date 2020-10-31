//Zachary Brennan; 11/2020
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;

public class CustomNetwork : NetworkManager {
    public string playerCharacter;
    
    //Information for Online Data
    public void OnlineHost(Text t)
    {
        if(matchMaker == null)
             StartMatchMaker();
        matchMaker.CreateMatch(t.text,2,true,"","","",0,0,CreatedMatch);
    }
    //The listener for creating a match
    private void CreatedMatch(bool success, string extendedInfo, MatchInfo responseData)
    {
        //Starts match with matchmaking data
        base.StartHost(responseData);
    }

    //Lan Hosting
    public void BecomeHost()
    {
        base.StartHost();
    }

    //Lan Joining
    public void JoinLocalGame()
    {
        NetworkManager.singleton.networkAddress = GameObject.Find("IPAddress").GetComponent<Text>().text;
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StartClient();
    }


    //Overrided to send a message to the main network, changes character
    public override void OnClientSceneChanged(NetworkConnection conn)
     {
         
        StringMessage character = new StringMessage(playerCharacter);
            ClientScene.Ready(conn);
            if (autoCreatePlayer)
            {

                ClientScene.AddPlayer(conn, 0, character);
            }
     }
    //Overrides OnClientConnect
    public override void OnClientConnect(NetworkConnection conn)
    {
        //base.OnClientConnect(conn);
    }

    //Changes the character based on selection
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader n)
    {
        Debug.Log("world");
        GameObject temp = null;

        switch (n.ReadMessage<StringMessage>().value)
        {
            case "Cactus":
                temp = (GameObject)Instantiate(Resources.Load("CactusPlayer"), transform.position, Quaternion.identity);
                break;
            case "Devil":
                temp = (GameObject)Instantiate(Resources.Load("DevilPlayer"), transform.position, Quaternion.identity);
                break;
        }
        NetworkServer.AddPlayerForConnection(conn, temp, playerControllerId);
    }



    //Refeshing the Online Match Info
  /*  public void OnlineInfo()
    {
        if (!matchMaker)
        {
            StartMatchMaker();
        }

        matchMaker.ListMatches(0, 5, "", true, 0, 0, MatchLists);
    }

    private void MatchLists(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
    {
       
    }*/
}
