using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Photon.Pun;
using UnityEngine;

public class Dino : MonoBehaviourPun, IMixedRealityPointerHandler
{
    public Card card;
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        if (card != null)
        {
            if (GameManager.Instance.playerManager.CardChose != null)
                GameManager.Instance.playerManager.CardChose.ButtonApp.SetActive(false);
            GameManager.Instance.playerManager.CardChose = card;
            card.ButtonApp.SetActive(true);
        }
        print("Click");
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }

    public void RequestOwnership() {
        base.photonView.RequestOwnership();
    }
}
