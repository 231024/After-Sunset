using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;
using Random = UnityEngine.Random;


internal sealed class AnonymousAuthorization  : AccountDataWindowBase
{
    private TMP_Text _textButtonSignInAnonimous;
    private Button _signInButton;
    
    // public AnonymousAuthorization()
    // {
    //     _textStatus = _generalViews.TextStatus;
    //     _signInButton = _generalViews.AnonymousLoginView.SignInButton;
    //     _textButtonSignInAnonimous = _signInButton.GetComponentInChildren<TMP_Text>();
    // }

    public AnonymousAuthorization(MainGeneralViews mainGeneralViews, PhotonController photonController) : base(mainGeneralViews, photonController)
    {
        _signInButton = _mainGeneralViews.AnonymousLoginView.SignInButton;
        _textButtonSignInAnonimous = _signInButton.GetComponentInChildren<TMP_Text>();
    }

    // public override void Start()
    // {
    //     base.Start();
    //     _textStatus = _generalViews.TextStatus;
    //     _signInButton = _generalViews.AnonymousLoginView.SignInButton;
    //     _textButtonSignInAnonimous = _signInButton.GetComponentInChildren<TMP_Text>();
    // }

    protected override void SubscriptionsElementsUi()
    {
        _signInButton.onClick.AddListener(Login);
        CheckAccount();
    }

    protected override void UnSubscriptionsElementsUi()
    {
        _signInButton.onClick.RemoveListener(Login);
    }

    protected override void CheckAccount()
    {
        base.CheckAccount();
        _username = $"Player {Random.Range(1000, 9999)}";
        
        if (PlayerPrefs.HasKey(UNIQUE_AUTH_KEY))
        {
            _textButtonSignInAnonimous.text = IS_REGISTRED_TEXT;
        }
        else
        {
            _textButtonSignInAnonimous.text = IS_NOT_REGISTRED_TEXT;
        }
    }

    private void Login()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "2885B";
            Debug.Log("Successfully set the title ID.");
        }
        
        var loginWithCustomIDRequest = new LoginWithCustomIDRequest 
        { 
            CustomId = _id, 
            CreateAccount = _creationAccount 
        };
        
        PlayFabClientAPI.LoginWithCustomID(loginWithCustomIDRequest, 
            result =>
            {
                _textStatus.text = "PlayFab connection - Success";
                _textStatus.color = _colorSuccess;
                PlayerPrefs.SetString(UNIQUE_AUTH_KEY, _id);
                OnLoginSuccess();
                if (_creationAccount)
                {
                    SetPlayerUsername(_username);
                }
                else
                {
                    _photonController.Connect();
                }
            }, OnLoginError);
        
        _textStatus.text = "Signing in...";
        _textStatus.color = _colorLoading;
    }
}