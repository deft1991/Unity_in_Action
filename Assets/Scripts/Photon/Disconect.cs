using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Disconect : MonoBehaviour
{
    //Leave the room.
    public void OnClickDisconectButton()
    {
        PhotonNetwork.LeaveRoom();
    }
}