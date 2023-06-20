using System;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviour, IMixedRealityPointerHandler
{
    private enum PlayerState
    {
        CHOOSE,
        DISPLAY_MODEL,
    }
    private PlayerState state;

    public GameObject MenuObj;

    public GameObject cardPrefab;
    public GameObject tooltip;
    public Transform[] cardMenuSlots;
    public CardConfig[] cardConfigs;


    public Card CardChose { get => cardChose; set => cardChose = value; }
    private Card cardChose = null;

    private bool initUI = false;

    void Start()
    {
        if (GameManager.Instance.isAudience)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (GameManager.Instance.TrackedWithVuforia && !initUI)
        {
            InitUI();
            initUI = true;
        }
        else if (GameManager.Instance.TrackedWithVuforia)
        {
            UpdateState();
        }
    }

    private float chooseModelPositionDuration = 3f;
    private Vector3 modelPrevPos = Vector3.zero;
    public bool onCardSelect = true;
    private void UpdateState()
    {
        switch (state)
        {
            case PlayerState.CHOOSE:
                {
                    if (CardChose != null && onCardSelect)
                    {
                        cardChose.Skeleton.GetComponent<TooltipCollider>().TurnOnTooltipColliders(false);
                        state = PlayerState.DISPLAY_MODEL;
                    }
                    break;
                }
            case PlayerState.DISPLAY_MODEL:
                {
                    var raycastHit = GetRayCastHit();
                    if (raycastHit != null && raycastHit.Details.Object.tag != "GUI" && raycastHit.Details.Object.tag != "ButtonBar")
                    {
                        ShowModel(raycastHit.Details.Point, Quaternion.identity);
                    }
                    if (modelPrevPos != Vector3.zero && Vector3.Distance(CardChose.Skeleton.transform.position, modelPrevPos) <= 0.3f)
                    {
                        if (chooseModelPositionDuration <= 0f)
                        {
                            chooseModelPositionDuration = 3f;
                            modelPrevPos = Vector3.zero;
                            state = PlayerState.CHOOSE;
                            onCardSelect = false;
                            cardChose.Skeleton.GetComponent<TooltipCollider>().TurnOnTooltipColliders(true);

                            break;
                        }
                        else
                        {
                            chooseModelPositionDuration -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        chooseModelPositionDuration = 3f;
                    }
                    modelPrevPos = CardChose.Skeleton.transform.position;

                    break;

                };
        }
    }

    private IPointerResult GetRayCastHit()
    {
        foreach (var source in MixedRealityToolkit.InputSystem.DetectedInputSources)
        {
            // Ignore anything that is not a hand because we want articulated hands
            if (source.SourceType == Microsoft.MixedReality.Toolkit.Input.InputSourceType.Hand)
            {
                foreach (var p in source.Pointers)
                {
                    if (p is IMixedRealityNearPointer)
                    {
                        // Ignore near pointers, we only want the rays
                        continue;
                    }
                    if (p.Result != null)
                    {
                        var startPoint = p.Position;
                        var endPoint = p.Result.Details.Point;
                        var hitObject = p.Result.Details.Object;
                        if (hitObject)
                        {
                            return p.Result;
                        }
                    }

                }
            }
        }
        Debug.Log("HIT NULL");
        return null;
    }

    private void InitUI()
    {
        MenuObj.SetActive(true);
        if (GameManager.Instance.TrackedWithVuforia)
        {
            for (int i = 0; i < cardConfigs.Length; ++i)
            {
                GameObject cardObj = Instantiate(cardPrefab);
                cardObj.transform.SetParent(cardMenuSlots[i]);
                cardObj.transform.localPosition = Vector3.zero;
                cardObj.transform.localRotation = Quaternion.identity;
                Card card = cardObj.GetComponent<Card>();
                card.Config = cardConfigs[i];
                card.InitCardUI(cardMenuSlots[i].transform.GetChild(0).gameObject);
            }
        }
    }

    public void ShowModel(Vector3 position, Quaternion rotation)
    {
        if (!cardChose.Skeleton.activeSelf)
        {
            cardChose.Skeleton.SetActive(true);
        }
        cardChose.Skeleton.transform.position = position;
        cardChose.Skeleton.transform.rotation = Quaternion.identity;
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerDragged");
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        // state = PlayerState.CHOOSE;
        // cardChose = null;
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerClicked");
    }
}
