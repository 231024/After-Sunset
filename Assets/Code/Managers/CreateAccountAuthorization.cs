using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

internal sealed class CreateAccountAuthorization : AccountDataWindowBase
{
    private InputField _mailField;
    private Button _createAccountButton;
    
    private string _mail;
    
    public CreateAccountAuthorization(GeneralViews generalViews) : base(generalViews)
    {
        _usernameField = generalViews.CreateAccountView.UsernameField;
        _passwordField = generalViews.CreateAccountView.PasswordField;
        _mailField = generalViews.CreateAccountView.MailField;
        _textStatus = generalViews.TextStatus;
        _createAccountButton = generalViews.CreateAccountView.CreateInButton;
    }
    
    protected override void SubscriptionsElementsUi()
    {
        base.SubscriptionsElementsUi();
        
        _mailField.onValueChanged.AddListener(UpdateMail);
        _createAccountButton.onClick.AddListener(CreateAccount);
    }
    
    private void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _username,
            Email = _mail,
            Password = _password
        }, result =>
        {
            _textStatus.text = "PlayFab connection - Success";
            _textStatus.color = _colorSuccess;
            //PlayerPrefs.SetString(UNIQUE_AUTH_KEY, _id);
            OnLoginSuccess();
            SetPlayerUsername(_username);
            //EnterInGameScene(result.PlayFabId);
        }, OnLoginError);
    }
    
    private void UpdateMail(string mail)
    {
        _mail = mail;
    }
}