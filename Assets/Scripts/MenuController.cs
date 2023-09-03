using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class MenuController : MonoBehaviour
{
    private bool isLevelLoaded;
    public void OnPlayBtnClicked()
    {
        if (isLevelLoaded) return;
        SceneManager.LoadScene(GlobalPlayerPrefs.LevelIdx);
        isLevelLoaded = true;

    }

    public void OnExitBtnClicked()
    {
        Application.Quit();
    }
}
