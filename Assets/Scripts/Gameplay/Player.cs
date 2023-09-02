using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ScriptableObjects;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        public PlayerMovement PlayerMovement;
        public PlayerInput PlayerInput;

        [SerializeField] private float collectRadius;

        private List<Enums.CollectableType> _neededCollectables;
        [SerializeField] private CollectableSO collectableSO;

        private void Awake()
        {
            _neededCollectables = collectableSO.NeededCollectables;
        }

        public void TryCollectObject()
        {
            var collectableColliders = Physics.OverlapSphere(transform.position, collectRadius);
            List<Collectable> collectables = new List<Collectable>();
            foreach (var collectableCollider in collectableColliders)
            { if(collectableCollider.TryGetComponent(out Collectable collectable)) { collectables.Add(collectable); } }

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
            _neededCollectables.Remove(collectableType);
            if (_neededCollectables < 0)
            {
                
            }
        }
    }
}