using UnityEngine;

namespace Gameplay
{
    public class CraftArea : MonoBehaviour
    {
        public bool IsActive = false;

        [SerializeField] private GameObject textUIGo;
        [SerializeField] private GameObject lightGo;
        public GameObject CraftedObj;

        public void Enable()
        {
            IsActive = true;
        }

        public void EnableLight()
        {
            lightGo.SetActive(true);
        }

        public void DisableLight()
        {
            lightGo.SetActive(false);
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