using UnityEngine;

namespace Utilities
{
    public static class GlobalPlayerPrefs
    {
        public static int LevelIdx
        {
            get
            {
                var lvl = PlayerPrefs.GetInt("LevelIdx", 1);
                if (lvl > 4)
                {
                    lvl = 1;
                    PlayerPrefs.SetInt("LevelIdx", lvl);
                }
                return lvl;
            } 
            set => PlayerPrefs.SetInt("LevelIdx", value);
        }   // 0:menu, 1:night, 2:day, 3:night2, 4:day2
    }
}