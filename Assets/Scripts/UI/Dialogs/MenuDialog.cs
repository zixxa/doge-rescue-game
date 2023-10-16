using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UI;
public class MenuDialog : Dialog{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    protected void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        exitButton.onClick.AddListener(OnExitClick);
    }
    private void OnPlayButtonClick() => SceneManager.LoadScene(ConstantValues.MAIN_SCENE_NAME);
    private void OnExitClick() => Application.Quit();
}