using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class CustomNetwork : NetworkManager {
    public string playerCharacter;

    public void BecomeHost()
    {
        base.StartHost();
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
     {
         
        StringMessage character = new StringMessage(playerCharacter);
       // if (!clientLoadedScene)
       // {
            ClientScene.Ready(conn);
            if (autoCreatePlayer)
            {

                ClientScene.AddPlayer(conn, 0, character);
            }
       // }
     }
    public override void OnClientConnect(NetworkConnection conn)
    {
        //base.OnClientConnect(conn);
    }

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
}
