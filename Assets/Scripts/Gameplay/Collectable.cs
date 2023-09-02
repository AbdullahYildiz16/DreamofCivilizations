using System.Collections.Generic;
using Interface;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class Collectable : MonoBehaviour, ICollectible
    {
        public Enums.CollectableType CollectableType;

        public void Collect(List<Enums.CollectableType> neededCollectable, Player player)
        {
            foreach (var collectable in neededCollectable)
            {
                if (collectable == CollectableType)
                {
                    player.RemoveCollectable(CollectableType);
                    Destroy(gameObject);
                    return;
                }
            }

            Debug.Log("Do not Collect");
        }
    }
}