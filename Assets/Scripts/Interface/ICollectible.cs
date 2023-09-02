using System.Collections.Generic;
using Gameplay;
using Utilities;

namespace Interface
{
    public interface ICollectible
    {
        public void Collect(List<Enums.CollectableType> collectableType, Player player);
    }
}
