using MessagePipe;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

internal sealed class SignInAccountAuthorization : AccountDataWindowBase
{
    private Button _signInButton;

    public SignInAccountAuthorization(MainGeneralViews mainGeneralViews, 
        PhotonController photonController, 
        ISubscriber<string, string> subscriber) : 
        base(mainGeneralViews, photonController, subscriber)
    {
        _usernameField = _mainGeneralViews.SignInAccountView.UsernameField;
        _passwordField = _mainGeneralViews.SignInAccountView.PasswordField;
        _signInButton = _mainGeneralViews.SignInAccountView.SignInButton;
    }

    // public override void Start()
    // {
    //     base.Start();
    //     _usernameField = _generalViews.SignInAccountView.UsernameField;
    //     _passwordField = _generalViews.SignInAccountView.PasswordField;
    //     _textStatus = _generalViews.TextStatus;
    //     _signInButton = _generalViews.SignInAccountView.SignInButton;
    // }

    protected override void SubscriptionsElementsUi()
    {
        base.SubscriptionsElementsUi();
        
        _signInButton.onClick.AddListener(SignIn);
    }

    private void SignIn()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "2885B";
            Debug.Log("Successfully set the title ID.");
        }
        
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _username,
            Password = _password
        }, result =>
        {
            _textStatus.text = "PlayFab connection - Success";
            _textStatus.color = _colorSuccess;
            //PlayerPrefs.SetString(UNIQUE_AUTH_KEY, _id);
            OnLoginSuccess();
            SetPlayerUsername(_username);
        }, OnLoginError);
        
        _textStatus.text = "Signing in...";
        _textStatus.color = _colorLoading;
    }
}