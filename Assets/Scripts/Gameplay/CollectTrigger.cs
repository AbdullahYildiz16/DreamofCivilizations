using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class CollectTrigger : MonoBehaviour
    {
        private Player _player;

        public List<Collectable> CollectablesInTrigger = new List<Collectable>();

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
        }

        private void Update()
        {
            if(CollectablesInTrigger.Count < 1) return;

            var minDis = float.MaxValue;
            Collectable closestCollectable = CollectablesInTrigger.First();
            foreach (var collectable in CollectablesInTrigger)
            {
                var dis = Vector3.Distance(transform.position, collectable.transform.position);
                if (dis < minDis) { closestCollectable = collectable; }
            }
            var camTrans = _player.transform.GetChild(1).transform;
            var toOther = closestCollectable.transform.position - transform.position;
            if (Vector3.Dot(camTrans.forward, toOther.normalized) > .7f)
            {
                closestCollectable.OpenUI();
            }
            else
            {
                closestCollectable.CloseUI();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out Collectable collectable))
            {
                CollectablesInTrigger.Add(collectable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.TryGetComponent(out Collectable collectable))
            {
                if(collectable.UIActive) collectable.CloseUI();
                CollectablesInTrigger.Remove(collectable);
            }
        }
    }
}