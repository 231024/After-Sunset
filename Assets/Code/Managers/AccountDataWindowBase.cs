﻿using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class AccountDataWindowBase : IInitialization, ICleanup
{
    private InputField _usernameField;
    private InputField _passwordField;

    protected Color _colorLoading;
    protected Color _colorSuccess;
    protected Color _colorFailure;
    
    protected TMP_Text _textStatus;

    protected const string UNIQUE_AUTH_KEY = "player-unique-id";
    protected const string IS_NOT_REGISTRED_TEXT = "Register";
    protected const string IS_REGISTRED_TEXT = "Play without logining";
    protected const string LOADING_LOBBY_SCENE = "Lobby";

    protected string _id;
    protected string _username;
    protected string _password;
    protected string _connecting = "Connecting...";

    protected bool _creationAccount;

    public AccountDataWindowBase(ColorView colorView)
    {
        SetColor(colorView);
    }

    public void Initialization()
    {
        SubscriptionsElementsUi();
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
    
    protected void Login()
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
                OnLoginSuccess(result);
                if (_creationAccount)
                {
                    SetPlayerUsername(_username);
                }
                else
                {
                    
                    //SceneManager.LoadScene(LOADING_LOBBY_SCENE);
                }
            }, OnLoginError);
        
        _textStatus.text = "Signing in...";
        _textStatus.color = _colorLoading;
    }
    
    protected void SetPlayerUsername(String displayName)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = displayName
            },
            result =>
            {
               //SceneManager.LoadScene(LOADING_LOBBY_SCENE);
            }, Debug.LogError);
    }
    
    protected void OnLoginSuccess(LoginResult result)
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

    protected void EnterInGameScene(string playFabId)
    {
        //SetUserData(playFabId);
        //MakePurchase();
        //GetInventory();
    }

    public void Cleanup()
    {
        UnSubscriptionsElementsUi();
    }
}
