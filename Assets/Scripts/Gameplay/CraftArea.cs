using UnityEngine;

namespace Gameplay
{
    public class CraftArea : MonoBehaviour
    {
        public bool IsActive = false;

        [SerializeField] private GameObject textUIGo;

        public void Enable()
        {
            IsActive = true;
        }

        public void OpenUI()
        {
            textUIGo.SetActive(true);
        }
        
        public void CloseUI()
        {
            textUIGo.SetActive(false);
        }
    }
}