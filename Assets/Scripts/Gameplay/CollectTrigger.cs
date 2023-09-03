using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

namespace Gameplay
{
    public class CollectTrigger : MonoBehaviour
    {
        private Player _player;

        public List<Collectable> CollectablesInTrigger = new List<Collectable>();

        private CraftArea _craftArea;

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
        }

        private void Update()
        {
            if (TryInteractCraft(false)) return;

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
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent(out Collectable collectable))
            {
                CollectablesInTrigger.Add(collectable);
            }

            if (other.transform.TryGetComponent(out CraftArea craftArea))
            {
                _craftArea = craftArea;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.TryGetComponent(out Collectable collectable))
            {
                if(collectable.UIActive) collectable.CloseUI();
                CollectablesInTrigger.Remove(collectable);
            }

            if (other.transform.TryGetComponent(out CraftArea craftArea))
            {
                _craftArea.CloseUI();
                _craftArea = null;
            }
        }

        public bool TryInteractCraft(bool clickedE)
        {
            if (_craftArea != null)
            {
                var camTransform = _player.transform.GetChild(1).transform;
                var toOtherTransform = _craftArea.transform.position - transform.position;
                if (Vector3.Dot(camTransform.forward, toOtherTransform.normalized) > .7f)
                {
                    _craftArea.OpenUI();
                    if (clickedE)
                    {
                        if (_craftArea.IsActive)
                        {
                            _craftArea.CloseUI();
                            _craftArea.IsActive = false;
                            _player.PlayerMovement.enabled = false;
                            _player.PlayerInput.enabled = false;
                            Debug.Log("Level End");
                        }
                        else
                        {
                            MainCanvas.instance.EnableWarningUI();
                            Debug.Log("Not Enough Materials");
                        }
                    }
                    return true;
                }
            }

            return false;
        } 
    }
}