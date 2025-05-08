using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class MainMenuEvent : MonoBehaviour
{

    private UIDocument _document;


    private VisualElement _rootMenu;
    private VisualElement _creditMenu;
    private VisualElement _highScoreMenu;

    private Label _highScoreDisplay;

    private Button _button;
    private Button _creditButton;
    private Button _creditBackButton;
    private Button _quitButton;

    private Button _highScoreButton;
    private Button _highScoreResetButton;
    private Button _highScoreBackButton;

    private List<Button> _menuButton = new List<Button>();

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _song01;


    [SerializeField] private string _startLevelName;

    private void Awake()
    {

        _audioSource = GetComponent<AudioSource>();


        MusicManager.Instance.Play(_song01, .1f);

        _document = GetComponent<UIDocument>();

        _rootMenu = _document.rootVisualElement.Q("RootMenu");
        _creditMenu = _document.rootVisualElement.Q("CreditMenu");
        _highScoreMenu = _document.rootVisualElement.Q("HighScoreMenu");


        _highScoreDisplay = _document.rootVisualElement.Q<Label>("HighScoreDisplay");


        _button = _document.rootVisualElement.Q("StartGameButton") as Button;
        
        _button.RegisterCallback<ClickEvent>(OnPlayGameClick);


        _creditButton = _document.rootVisualElement.Q("CreditButton") as Button;

        _creditButton.RegisterCallback<ClickEvent>(OnCreditButtonClick);

        
        _creditBackButton = _document.rootVisualElement.Q("CreditBackButton") as Button;

        _creditBackButton.RegisterCallback<ClickEvent>(OnCreditBackButtonClick);


        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;

        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);



        _highScoreButton = _document.rootVisualElement.Q("HighScoreButton") as Button;

        _highScoreButton.RegisterCallback<ClickEvent>(OnHighScoreButtonClick);


        _highScoreResetButton = _document.rootVisualElement.Q("HighScoreResetButton") as Button;

        _highScoreResetButton.RegisterCallback<ClickEvent>(OnHighScoreResetButtonClick);


        _highScoreBackButton = _document.rootVisualElement.Q("HighScoreBackButton") as Button;

        _highScoreBackButton.RegisterCallback<ClickEvent>(OnHighScoreBackButtonClick);


        _menuButton = _document.rootVisualElement.Query<Button>().ToList();
        for(int i = 0; i < _menuButton.Count; i++)
        {
            _menuButton[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void OnDisable()
    {
        _creditButton.UnregisterCallback<ClickEvent>(OnCreditButtonClick);
        
        _creditBackButton.UnregisterCallback<ClickEvent>(OnCreditBackButtonClick);

        _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);

        _button.UnregisterCallback<ClickEvent>(OnPlayGameClick);

        _highScoreButton.UnregisterCallback<ClickEvent>(OnHighScoreButtonClick);

        _highScoreResetButton.UnregisterCallback<ClickEvent>(OnHighScoreResetButtonClick);

        _highScoreBackButton.UnregisterCallback<ClickEvent>(OnHighScoreBackButtonClick);

        for (int i = 0; i < _menuButton.Count; i++)
        {
            _menuButton[i].UnregisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        SceneManager.LoadScene(_startLevelName);

        MusicManager.Instance.Stop(2);
    }

    private void OnAllButtonClick(ClickEvent evt)
    {
        _audioSource.Play();
    }

    private void OnCreditButtonClick(ClickEvent evt)
    {
        Debug.Log("Credit Button Click");

        _rootMenu.style.display = DisplayStyle.None;
        _creditMenu.style.display = DisplayStyle.Flex;
    }

    private void OnCreditBackButtonClick(ClickEvent evt)
    {
        Debug.Log("Credit Back Button Click");

        _rootMenu.style.display = DisplayStyle.Flex;
        _creditMenu.style.display = DisplayStyle.None;
    }

    private void OnHighScoreButtonClick(ClickEvent evt)
    {
        

        _rootMenu.style.display = DisplayStyle.None;
        _highScoreMenu.style.display = DisplayStyle.Flex;
    }

    private void OnHighScoreResetButtonClick(ClickEvent evt)
    {
      

        _rootMenu.style.display = DisplayStyle.None;
        _highScoreMenu.style.display = DisplayStyle.Flex;
    }


    private void OnHighScoreBackButtonClick(ClickEvent evt)
    {
        

        _rootMenu.style.display = DisplayStyle.Flex;
        _highScoreMenu.style.display = DisplayStyle.None;
    }




    private void OnQuitButtonClick(ClickEvent evt)
    {
        Debug.Log("Quit Button Click");

        Application.Quit();
    }

    public void HighScoreDisplay()
    {
        _highScoreDisplay.text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";

    }
    
}
