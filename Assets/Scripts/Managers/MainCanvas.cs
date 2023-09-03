using System;
using System.Collections;
using Gameplay;
using UnityEngine;

namespace Managers
{
    public class MainCanvas : MonoBehaviour
    {
        public static MainCanvas instance;

        [SerializeField] private CraftArea craftArea;
        [SerializeField] private GameObject levelEndTextGo, materialWarningTextGo;

        private void Awake()
        {
            instance = this;
        }

        public void EnableLevelEndUI()
        {
            levelEndTextGo.SetActive(true);
            craftArea.Enable();
        }

        public void EnableWarningUI()
        {
            if(_warningTextRoutine != null) StopCoroutine(_warningTextRoutine);
            _warningTextRoutine = StartCoroutine(WarningTextRoutine());
        }

        public IEnumerator WarningTextRoutine()
        {
            materialWarningTextGo.SetActive(true);
            yield return new WaitForSeconds(1.8f);
            materialWarningTextGo.SetActive(false);
        }

        private Coroutine _warningTextRoutine;
    }
}