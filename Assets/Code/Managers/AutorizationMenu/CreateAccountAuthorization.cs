using MessagePipe;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

internal sealed class CreateAccountAuthorization : AccountDataWindowBase
{
    private InputField _mailField;
    private Button _createAccountButton;

    private string _mail;

    public CreateAccountAuthorization(MainGeneralViews mainGeneralViews, 
        PhotonController photonController, 
        ISubscriber<string, string> subscriber) : 
        base(mainGeneralViews, photonController, subscriber)
    {
        _usernameField = _mainGeneralViews.CreateAccountView.UsernameField;
        _passwordField = _mainGeneralViews.CreateAccountView.PasswordField;
        _mailField = _mainGeneralViews.CreateAccountView.MailField;
        _createAccountButton = _mainGeneralViews.CreateAccountView.CreateInButton;
    }

    // public override void Start()
    // {
    //     base.Start();
    //     _usernameField = _generalViews.CreateAccountView.UsernameField;
    //     _passwordField = _generalViews.CreateAccountView.PasswordField;
    //     _mailField = _generalViews.CreateAccountView.MailField;
    //     _textStatus = _generalViews.TextStatus;
    //     _createAccountButton = _generalViews.CreateAccountView.CreateInButton;
    // }

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