using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardConfig Config { get => config; set => config = value; }
    private CardConfig config;
    public GameObject Skeleton { get => skeleton; set => skeleton = value; }

    private GameObject skeleton;
    public Image avatarImg;
    private bool isSkin = true;
    private GameObject buttonApp;
    public GameObject ButtonApp { get => buttonApp; }


    public void InitCardUI(GameObject buttonAppInit)
    {
        buttonApp = buttonAppInit;
        if (Config != null)
        {
            avatarImg.sprite = Config.avatarImg;
            skeleton = PhotonNetwork.Instantiate("Prefabs/Dino/" + Config.skeletonModel.name, new Vector3(100, 100, 100), Quaternion.identity);

            skeleton.GetComponent<Dino>().card = this;
            //Display Tooltip
            if (skeleton.GetComponentInChildren<TooltipCollider>() != null)
            {
                skeleton.GetComponentInChildren<TooltipCollider>().DisplayTooltip();
            }

            // Box Collider for Grab
            if (skeleton.GetComponent<Collider>())
            {
                skeleton.GetComponent<Collider>().enabled = true;
            }

            skeleton.SetActive(false);
            InitButtonApp();
        }
    }

    public void ChangePrefabSkin()
    {
        if (isSkin)
        {
            skeleton.GetComponent<TooltipCollider>().DisplaySkeleton();
        }
        else
        {
            skeleton.GetComponent<TooltipCollider>().DisplaySkin();
        }

        isSkin = !isSkin;
    }

    private void InitButtonApp()
    {
        buttonApp.GetComponent<ButtonFollowObject>().SetCardControl(this);

    }


    public void OnCardPressed()
    {
        if (GameManager.Instance.playerManager.CardChose != null)
            GameManager.Instance.playerManager.CardChose.ButtonApp.SetActive(false);

        GameManager.Instance.playerManager.CardChose = this;
        GameManager.Instance.playerManager.onCardSelect = true;
        Skeleton.SetActive(true);
        Skeleton.GetComponent<Collider>().enabled = false;
        buttonApp.SetActive(true);
    }

}
