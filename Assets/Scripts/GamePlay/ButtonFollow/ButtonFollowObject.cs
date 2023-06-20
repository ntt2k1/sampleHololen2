using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFollowObject : MonoBehaviour
{
    private GameObject playerCamera;
    private Card targetCard;

    // public void SetGameObject(GameObject selectedGameObj)
    // {
    //     targetObject = selectedGameObj;
    // }

    public void SetCardControl(Card selectedCard)
    {
        targetCard = selectedCard;
    }

    public void ChangeSkin()
    {
        targetCard.Skeleton.GetComponent<Dino>().RequestOwnership();
        targetCard.ChangePrefabSkin();
    }

    public void ChangeToTooltip()
    {
        targetCard.Skeleton.GetComponent<Dino>().RequestOwnership();
        Collider collider = targetCard.Skeleton.GetComponent<Collider>();
        collider.enabled = !collider.enabled;
    }

    public void DisablePrefab()
    {
        targetCard.Skeleton.GetComponent<Dino>().RequestOwnership();
        targetCard.Skeleton.SetActive(false);
    }
}
