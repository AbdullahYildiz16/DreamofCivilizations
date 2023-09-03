using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        public PlayerMovement PlayerMovement;
        public PlayerInput PlayerInput;

        //[SerializeField] private float collectRadius;
        private CollectTrigger _collectTrigger;

        private List<CollectableData> _neededCollectables = new List<CollectableData>();
        [SerializeField] private CollectableSO collectableSO;
        [SerializeField] private Transform UICollectableParent;

        [Header("UI Prefabs")]
        [SerializeField] private GameObject woodUIPrefab;
        [SerializeField] private GameObject grassUIPrefab, rockUIPrefab, fiberUIPrefab, soilUIPrefab, waterUIPrefab;

        private void Awake()
        {
            _collectTrigger = GetComponentInChildren<CollectTrigger>();
            foreach (var type in collectableSO.NeededCollectables)
            {
                var go = type switch
                {
                    Enums.CollectableType.Wood => Instantiate(woodUIPrefab, Vector3.zero, Quaternion.identity, UICollectableParent),
                    Enums.CollectableType.Grass => Instantiate(grassUIPrefab, Vector3.zero, Quaternion.identity, UICollectableParent),
                    Enums.CollectableType.Rock => Instantiate(rockUIPrefab, Vector3.zero, Quaternion.identity, UICollectableParent),
                    Enums.CollectableType.Fiber => Instantiate(fiberUIPrefab, Vector3.zero, Quaternion.identity, UICollectableParent),
                    Enums.CollectableType.Soil => Instantiate(soilUIPrefab, Vector3.zero, Quaternion.identity, UICollectableParent),
                    Enums.CollectableType.Water => Instantiate(waterUIPrefab, Vector3.zero, Quaternion.identity, UICollectableParent),
                    _ => throw new ArgumentOutOfRangeException()
                };
                var collectableData = new CollectableData
                {
                    CollectableType = type,
                    UIObj = go
                };
                _neededCollectables.Add(collectableData);
            }
        }

        public void TryCollectObject()
        {
            // var collectableColliders = Physics.OverlapSphere(transform.position, 2f);
            // List<Collectable> collectables = new List<Collectable>();
            var collectables = _collectTrigger.CollectablesInTrigger.ToList();

            if(collectables.Count < 1) return;

            var minDis = float.MaxValue;
            Collectable closestCollectable = collectables.First();
            foreach (var collectable in collectables)
            {
                var dis = Vector3.Distance(transform.position, collectable.transform.position);
                if (dis < minDis) { closestCollectable = collectable; }
            }
            var camTrans = transform.GetChild(1).transform;
            var toOther = closestCollectable.transform.position - transform.position;
            if (Vector3.Dot(camTrans.forward, toOther.normalized) > .7f)
            {
                closestCollectable.Collect(_neededCollectables, this);
            }
        }

        public void RemoveCollectable(Enums.CollectableType collectableType)
        {
            //var a = _neededCollectables.Where(a => a.CollectableType == collectableType).ToList();
            CollectableData data = null;
            foreach (var collectableData in _neededCollectables)
            {
                if (collectableData.CollectableType == collectableType)
                {
                    data = collectableData;
                }
            }

            Destroy(data.UIObj);
            _neededCollectables.Remove(data);
            if (_neededCollectables.Count < 0)
            {
                Debug.Log("__");
            }
        }

        public void RemoveFromTrigger(Collectable collectable)
        {
            _collectTrigger.CollectablesInTrigger.Remove(collectable);
        }

        public class CollectableData
        {
            public GameObject UIObj;
            public Enums.CollectableType CollectableType;
        }
    }
}