using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UserStudy
{
    public class UserStudyModule : MonoBehaviour
    {
        private GameObject _hitObject = null;
        [SerializeField] private Sphere _chosenSphere = null;
        public GameObject GetRayCastHit()
        {
            foreach (var source in CoreServices.InputSystem.DetectedInputSources)
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
                                Debug.Log("HitObject: " + hitObject.name + " type: " + hitObject.tag);
                                if(hitObject != _hitObject)
                                {
                                    _hitObject = hitObject;
                                    _chosenSphere?.OnNormal();
                                    _chosenSphere = null;

                                    if (hitObject.TryGetComponent<Sphere>(out Sphere component))
                                    {
                                        _chosenSphere?.OnNormal();
                                        _chosenSphere = component;
                                        _chosenSphere.OnHovered();
                                    }
                                }
                                return hitObject;
                            }
                            else
                            {
                                _hitObject = null;
                                _chosenSphere?.OnNormal();
                                _chosenSphere = null;
                            }
                        }

                    }
                }
            }
            return null;
        }


        private void Update()
        {
            if (GameManager.Instance.isAudience) return;
            GetRayCastHit();
        }
    }

}
