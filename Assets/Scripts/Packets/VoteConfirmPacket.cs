using Game.Enum;
using NUnit.Framework;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoteConfirmPacket : RPCPacket
{
    public override PacketType type => PacketType.VoteConfirm;

    public int ActorNumber;
    public int[] SelectionArr;

    public VoteConfirmPacket(int actorNumber, int[] selectionArr)
    {
        ActorNumber = actorNumber;
        SelectionArr = selectionArr;
    }

    public override void Send()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Request), RpcTarget.MasterClient, type, new object[] { ActorNumber, SelectionArr });
        }
        else
        {
            RPCManager.Instance.photonView.RPC(nameof(RPCManager.RPC_Apply), RpcTarget.All, type, new object[] { ActorNumber, SelectionArr });
        }
    }

    public override bool Check()
    {
        if (!PhotonNetwork.IsMasterClient)
            return false;
        
        //isLeader check
        return true;
    }

    public override void Response()
    {
        GameManager.Instance.DeliverSelectionArray(ActorNumber, SelectionArr);
    }
}
