using System.Collections.Generic;
using Interface;
using Managers;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class Collectable : MonoBehaviour, ICollectible
    {
        public Enums.CollectableType CollectableType;

        [SerializeField] private GameObject uiObj;
        public bool UIActive => uiObj.activeSelf;

        public void Collect(List<Player.CollectableData> collectableData, Player player)
        {
            List<Enums.CollectableType> neededCollectable = new List<Enums.CollectableType>();
            foreach (var data in collectableData) { neededCollectable.Add(data.CollectableType); }

            foreach (var collectable in neededCollectable)
            {
                if (collectable == CollectableType)
                {
                    player.RemoveCollectable(CollectableType);
                    player.RemoveFromTrigger(this);
                    Destroy(gameObject);
                    return;
                }
            }

            Debug.Log("Do not Collect");
        }

        public void OpenUI()
        {
            uiObj.SetActive(true);
        }

        public void CloseUI()
        {
            uiObj.SetActive(false);
        }
    }
}