using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class TooltipCollider : MonoBehaviour
{
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
        skinObj.SetActive(true);
        skeletonObj.SetActive(false);
    }

    public void DisplaySkeleton()
    {
        skinObj.SetActive(false);
        skeletonObj.SetActive(true);
    }
}
