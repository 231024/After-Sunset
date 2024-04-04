using System;
using System.Collections.Generic;
using MessagePipe;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

internal class AccountDataWindowBase : IStartable, IDisposable
{
    protected InputField _usernameField;
    protected InputField _passwordField;
    
    private Canvas _enterInGameCanvas;
    private Canvas _createAccountCanvas;
    private Canvas _signInCanvas;
    private Canvas _authorizationCanvas;

    protected Color _colorLoading;
    protected Color _colorSuccess;
    protected Color _colorFailure;
    
    protected TMP_Text _textStatus;

    protected const string UNIQUE_AUTH_KEY = "player-unique-id";
    protected const string IS_NOT_REGISTRED_TEXT = "Register";
    protected const string IS_REGISTRED_TEXT = "Play without logining";

    protected string _id;
    protected string _username;
    protected string _password;
    protected string _connecting = "Connecting...";

    protected bool _creationAccount;

    [Inject] protected MainGeneralViews _mainGeneralViews;
    [Inject] protected PhotonController _photonController;

    public AccountDataWindowBase(MainGeneralViews mainGeneralViews, PhotonController photonController)
    {
        _mainGeneralViews = mainGeneralViews;
        _photonController = photonController;
        
        _textStatus = _mainGeneralViews.TextStatus;
        SetColor(_mainGeneralViews.ColorView);
        _enterInGameCanvas = _mainGeneralViews.ManagerLoginWindowView.EnterInGameCanvas;
        _createAccountCanvas = _mainGeneralViews.ManagerLoginWindowView.CreateAccountCanvas;
        _signInCanvas = _mainGeneralViews.ManagerLoginWindowView.SignInCanvas;
        _authorizationCanvas = _mainGeneralViews.ManagerLoginWindowView.AuthorizationCanvas;
    }

    public void Start()
    {
        SubscriptionsElementsUi();
        BeginningAuthorized();
        //_photonController.Connect();
    }

    protected virtual void SubscriptionsElementsUi()
    {
        _usernameField.onValueChanged.AddListener(UpdateUsername);
        _passwordField.onValueChanged.AddListener(UpdatePassword);
    }
    
    protected virtual void UnSubscriptionsElementsUi()
    {
        _usernameField.onValueChanged.RemoveListener(UpdateUsername);
        _passwordField.onValueChanged.RemoveListener(UpdatePassword);
    }

    protected void SetColor(ColorView colorView)
    {
        _colorLoading = colorView.ColorLoading;
        _colorSuccess = colorView.ColorSuccess;
        _colorFailure = colorView.ColorFailure;
    }

    protected virtual void CheckAccount()
    {
        _creationAccount = !PlayerPrefs.HasKey(UNIQUE_AUTH_KEY);
        _id = PlayerPrefs.GetString(UNIQUE_AUTH_KEY, Guid.NewGuid().ToString());
    }
    
    protected void SetPlayerUsername(String displayName)
    {
        _username = displayName;
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = displayName
            },
            result =>
            { 
                _photonController.Connect();
            }, Debug.LogError);
    }
    
    protected void OnLoginSuccess()
    {
        _textStatus.text = "Successfully logged in PlayFab.";
        _textStatus.color = _colorSuccess;
        TryGetData();
    }
    
    protected void TryGetData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            {
                Keys = new List<string>
                {
                    GameConstants.SCORE_ID, 
                    GameConstants.TOTAL_SCORE_ID, 
                    GameConstants.LEVEL_ID
                }
            }, GotUserData, Debug.LogError
        );
    }
    
    protected void GotUserData(GetUserDataResult result)
    {
        var updatedData = new Dictionary<string, string>();
        var data = result.Data;

        if (!data.ContainsKey(GameConstants.SCORE_ID))
        {
            updatedData.Add(GameConstants.SCORE_ID, 0.ToString());
        }
        
        if (!data.ContainsKey(GameConstants.TOTAL_SCORE_ID))
        {
            updatedData.Add(GameConstants.TOTAL_SCORE_ID, 0.ToString());
        }
        
        if (!data.ContainsKey(GameConstants.LEVEL_ID))
        {
            updatedData.Add(GameConstants.LEVEL_ID, 0.ToString());
        }

        if (updatedData.Count > 0)
        {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = updatedData
                }, dataResult =>
                {
                    Debug.Log($"Created initial user data");
                },
                Debug.LogError);
        }
    }
    
    protected void OnLoginError(PlayFabError error)
    {
        var message = "<color=red>Failed to log into PlayFab</color>!";
        _textStatus.text = message;
        _textStatus.color = _colorFailure;
        Debug.LogError($"{message} {error}");
    }

    private void UpdateUsername(string username)
    {
        _username = username;
    }

    private void UpdatePassword(string password)
    {
        _password = password;
    }

    public void BeginningAuthorized()
    {
        _authorizationCanvas.enabled = true;
        _enterInGameCanvas.enabled = true;
    }

    protected void DisabledAllCanvas()
    {
        _enterInGameCanvas.enabled = false;
        _createAccountCanvas.enabled = false;
        _signInCanvas.enabled = false;
        _authorizationCanvas.enabled = false;
    }

    public void Dispose()
    {
        UnSubscriptionsElementsUi();
    }
}
