using UnityEngine;
using UnityEngine.UI;
using UI;
using UnityEngine.SceneManagement;
public class WinDialog : Dialog{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button exitButton;
    protected void Awake()
    {
        newGameButton.onClick.AddListener(OnNewGameButtonClick);
        exitButton.onClick.AddListener(OnExitClick);
    }

    private void OnNewGameButtonClick()
    {
        PlayerPrefs.SetInt(ConstantValues.CURRENT_LEVEL_NAME, 0);
        SceneManager.LoadScene(ConstantValues.MENU_SCENE_NAME);
    }

    private void OnExitClick() => Application.Quit();
}
