using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class GameMenuController : MonoBehaviour
{

    private VisualElement _gameplayMenuVisualTree;
    private VisualElement _pauseMenuVisualTree;

    private Button _pauseButton;
    private Button _quitButton;
    private Button _resumeButton;

    private List<Button> _buttons = new List<Button>();

    private AudioSource _audioSource;

    private void Awake()
    {

        _audioSource = GetComponent<AudioSource>();


        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        _gameplayMenuVisualTree = root.Q("GameplayMenuVisualTree");
        _pauseMenuVisualTree = root.Q("PauseMenuVisualTree");

        _pauseButton = root.Q("PauseButton") as Button;
        _pauseButton.RegisterCallback<ClickEvent>(OnPauseButtonClick);

        _quitButton = root.Q("QuitButton") as Button;
        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);

        _resumeButton = root.Q("ResumeButton") as Button;
        _resumeButton.RegisterCallback<ClickEvent>(OnResumeButtonClick);

        _buttons = root.Query<Button>().ToList();
        foreach(Button button in _buttons)
        {
            button.RegisterCallback<ClickEvent>(OnAnyButtonClick);
        }

        _gameplayMenuVisualTree.style.display = DisplayStyle.Flex;
        _pauseMenuVisualTree.style.display = DisplayStyle.None;
    }

    private void OnPauseButtonClick(ClickEvent evt)
    {
        Debug.Log("Activate Pause Menu");

        _gameplayMenuVisualTree.style.display = DisplayStyle.None;
        _pauseMenuVisualTree.style.display = DisplayStyle.Flex;
    }

    private void OnQuitButtonClick(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Quit!");
    }

    private void OnResumeButtonClick(ClickEvent evt)
    {
        _gameplayMenuVisualTree.style.display = DisplayStyle.Flex;
        _pauseMenuVisualTree.style.display = DisplayStyle.None;
    }

    private void OnAnyButtonClick(ClickEvent evt)
    {
        Debug.Log("Any Button");

        _audioSource.Play();
    }

    private void OnDisable()
    {
        _pauseButton.UnregisterCallback<ClickEvent>(OnPauseButtonClick);
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
        _resumeButton.UnregisterCallback<ClickEvent>(OnResumeButtonClick);

        foreach (Button button in _buttons)
        {
            button.UnregisterCallback<ClickEvent>(OnAnyButtonClick);
        }
    }
}
