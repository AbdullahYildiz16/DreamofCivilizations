using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CollectableSO", menuName = "CollectableSO", order = 0)]
    public class CollectableSO : ScriptableObject
    {
        public List<Enums.CollectableType> NeededCollectables;
    }
}