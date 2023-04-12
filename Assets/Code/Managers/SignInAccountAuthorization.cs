using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

internal sealed class SignInAccountAuthorization : AccountDataWindowBase
{
    private Button _signInButton;
    
    public SignInAccountAuthorization(GeneralViews generalViews) : base(generalViews.ColorView)
    {
        _usernameField = generalViews.SignInAccountView.UsernameField;
        _passwordField = generalViews.SignInAccountView.PasswordField;
        _textStatus = generalViews.TextStatus;
        _signInButton = generalViews.SignInAccountView.SignInButton;
    }
    
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