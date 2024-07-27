using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

namespace UserStudy
{
    public class RingSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _spherePrefab;
        [SerializeField] private int _totalRings;
        [SerializeField] private int _spheresPerRing;
        [SerializeField] private float _ringRadius;
        [SerializeField] private float _ringHeight;


        [SerializeField] private GameObject _playerObj;

        private void OnEnable()
        {
            if (_playerObj == null)
            {
                _playerObj = transform.parent.gameObject;
                transform.SetParent(null);
            }
            RingSpawn();
        }

        private void Update()
        {
            if (_playerObj)
            {
                //transform.SetPositionAndRotation(_playerObj.transform.position, Quaternion.identity);
                transform.SetPositionAndRotation(Vector3.up * _playerObj.transform.position.y, Quaternion.identity);

            }
        }

        private void RingSpawn()
        {
            for (int i = 0; i < _totalRings; i++)
            {
                for (int j = 0; j < _spheresPerRing; j++)
                {
                    var sphere = PhotonNetwork.Instantiate("Prefabs/" + _spherePrefab.name, Vector3.zero, Quaternion.identity);
                    sphere.SetActive(true);
                    sphere.transform.SetParent(transform);
                    sphere.transform.SetLocalPositionAndRotation(new(_ringRadius * Mathf.Cos(360/_spheresPerRing * j * Mathf.Deg2Rad),
                                                                    (i * _ringHeight - 1.5f),
                                                                    _ringRadius * Mathf.Sin(360 / _spheresPerRing * j * Mathf.Deg2Rad)),
                                                                Quaternion.identity);
                    sphere.transform.localScale = Vector3.one;
                }
            }
        }
    }
}
