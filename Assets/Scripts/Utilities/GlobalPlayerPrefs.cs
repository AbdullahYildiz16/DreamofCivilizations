using UnityEngine;

namespace Utilities
{
    public static class GlobalPlayerPrefs
    {
        public static int LevelIdx
        {
            get => PlayerPrefs.GetInt("LevelIdx", 0); 
            set => PlayerPrefs.SetInt("LevelIdx", value);
        }   // 0:menu, 1:night, 2:day, 3:night2, 4:day2
    }
}