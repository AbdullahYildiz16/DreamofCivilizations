using System.Collections;
using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace Managers
{
    public class MainCanvas : MonoBehaviour
    {
        public static MainCanvas instance;

        [SerializeField] private GameObject tapToStartBtn;
        [SerializeField] private CraftArea craftArea;
        [SerializeField] private GameObject levelEndTextGo, materialWarningTextGo;
        [SerializeField] private Button successBtn;

        private void Awake()
        {
            successBtn.onClick.AddListener(() =>
            {
                GlobalPlayerPrefs.LevelIdx++;
                SceneManager.LoadScene(GlobalPlayerPrefs.LevelIdx);
            });
            instance = this;
        }

        private IEnumerator Start()
        {
            if (GlobalPlayerPrefs.LevelIdx % 2 == 0) yield break;
            tapToStartBtn.SetActive(true);

        }

        private bool onceLoad = false;
        private void EnableNextLevelButton()
        {
            if (!onceLoad) onceLoad = true;
            else return;
            GlobalPlayerPrefs.LevelIdx++;
            SceneManager.LoadScene(GlobalPlayerPrefs.LevelIdx);
        }

        public void EnableLevelEndUI()
        {
            levelEndTextGo.SetActive(true);
            craftArea.Enable();
        }

        public void DisableLevelEndUI()
        {
            levelEndTextGo.SetActive(false);
        }

        public void EnableWarningUI()
        {
            if(_warningTextRoutine != null) StopCoroutine(_warningTextRoutine);
            _warningTextRoutine = StartCoroutine(WarningTextRoutine());
        }

        public void OnTapToStartBtnClicked()
        {
            EnableNextLevelButton();
        }

        private bool levelSucceed = false;
        public void LevelSuccess()
        {
            if(levelSucceed) return;
            levelSucceed = true;
            StartCoroutine(SuccessRoutine());
        }

        private IEnumerator SuccessRoutine()
        {
            yield return new WaitForSeconds(2.4f);
            EnableSuccesUI();
        }

        private void EnableSuccesUI()
        {

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