using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Photon.Pun;
using UnityEngine;

public class TooltipCollider : MonoBehaviour
{
    public PhotonView photonView;
    public TooltipConfig tooltipConfig;
    public GameObject skinObj;
    public GameObject skeletonObj;
    private List<GameObject> tooltipColliders = new List<GameObject>();

    public void DisplayTooltip()
    {
        foreach (var tooltip in tooltipConfig.tooltipColliderInfos)
        {
            GameObject initTooltip = Instantiate(GameManager.Instance.playerManager.tooltip);
            initTooltip.transform.parent = this.transform;
            initTooltip.GetComponent<BoxCollider>().size = tooltip.size;
            initTooltip.transform.localPosition = tooltip.position;
            initTooltip.transform.localRotation = tooltip.rotation;
            initTooltip.transform.localScale = tooltip.scale;
            initTooltip.GetComponent<TooltipSpawnerCustom>().toolTipText = tooltip.text;
            tooltipColliders.Add(initTooltip);
        }
    }

    public void TurnOnTooltipColliders(bool isTurnOn)
    {
        tooltipColliders.ForEach(tooltip =>
        {
            tooltip.SetActive(isTurnOn);
        });
    }

    public void DisplaySkin()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.RemoveBufferedRPCs(photonView.ViewID, "DisplaySkin_RPC");
            photonView.RPC("DisplaySkin_RPC", RpcTarget.AllBuffered);
            PhotonNetwork.SendAllOutgoingCommands();
        }
    }

    public void DisplaySkeleton()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.RemoveBufferedRPCs(photonView.ViewID, "DisplaySkeleton_RPC");
            photonView.RPC("DisplaySkeleton_RPC", RpcTarget.AllBuffered);
            PhotonNetwork.SendAllOutgoingCommands();
        }
    }
    [PunRPC]
    public void DisplaySkin_RPC()
    {
        skinObj.SetActive(true);
        skeletonObj.SetActive(false);
    }

    [PunRPC]
    public void DisplaySkeleton_RPC()
    {
        skinObj.SetActive(false);
        skeletonObj.SetActive(true);
    }

    public void ChangeTooltip(){
        if (photonView.IsMine)
        {
            PhotonNetwork.RemoveBufferedRPCs(photonView.ViewID, "ChangeTooltip_RPC");
            photonView.RPC("ChangeTooltip_RPC", RpcTarget.AllBuffered);
            PhotonNetwork.SendAllOutgoingCommands();
        }
    }

    [PunRPC]
    private void ChangeTooltip_RPC(){
        Collider collider = GetComponent<Collider>();
        collider.enabled = !collider.enabled;
    }


    public void SetSkeletonActive(bool active){
        if (photonView.IsMine)
        {
            PhotonNetwork.RemoveBufferedRPCs(photonView.ViewID, "SetSkeletonActive_RPC");
            photonView.RPC("SetSkeletonActive_RPC", RpcTarget.AllBuffered, active);
            PhotonNetwork.SendAllOutgoingCommands();
        }
    }

    [PunRPC]
    private void SetSkeletonActive_RPC(bool active){
        gameObject.SetActive(active);
    }
}
