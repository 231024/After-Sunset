using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class PlayFabAuthorization  : MonoBehaviour
{
    [Header("Text")] 
    [SerializeField] private TMP_Text _textButtonSignInAnonimous;
    [SerializeField] private TMP_Text _textStatus;
    
    [Header("Buttons")] 
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _deleteAccountButton;
    
    [Header("StatusColor")] 
    [SerializeField] private Color _colorLoading;
    [SerializeField] private Color _colorSuccess;
    [SerializeField] private Color _colorFailure;

    private string _username;

    private const string UNIQUE_AUTH_KEY = "player-unique-id";
    private const string IS_NOT_REGISTRED_TEXT = "Register";
    private const string IS_REGISTRED_TEXT = "Sign in";
    private const string LOADING_LOBBY_SCENE = "Lobby";

    private void Start()
    {
        _signInButton.onClick.AddListener(CheckIdAndLogin);
        _deleteAccountButton.onClick.AddListener(DeleteAccount);
        CheckAccount();
    }

    private void CheckAccount()
    {
        if (PlayerPrefs.HasKey(UNIQUE_AUTH_KEY))
        {
            _textButtonSignInAnonimous.text = IS_REGISTRED_TEXT;
        }
        else
        {
            _textButtonSignInAnonimous.text = IS_NOT_REGISTRED_TEXT;
        }
    }

    public void UpdateUsername(string username)
    {
        _username = username;
    }

    private void CheckIdAndLogin()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "2885B";
        }
        
        var creationAccount = !PlayerPrefs.HasKey(UNIQUE_AUTH_KEY);
        var id = PlayerPrefs.GetString(UNIQUE_AUTH_KEY, Guid.NewGuid().ToString());
        
        var loginWithCustomIDRequest = new LoginWithCustomIDRequest 
            { 
                CustomId = id, 
                CreateAccount = creationAccount 
            };
        
        PlayFabClientAPI.LoginWithCustomID(loginWithCustomIDRequest, 
            result =>
        {
            _textStatus.text = "PlayFab connection - Success";
            _textStatus.color = _colorSuccess;
            PlayerPrefs.SetString(UNIQUE_AUTH_KEY, id);
            OnLoginSuccess(result);
            if (creationAccount)
            {
                SetPlayerUsername();
            }
            else
            {
                SceneManager.LoadScene(LOADING_LOBBY_SCENE);
            }
        }, OnLoginError);
        
        _textStatus.text = "Signing in...";
        _textStatus.color = _colorLoading;
    }

    private void SetPlayerUsername()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = $"Player {Random.Range(1000, 9999)}"
            },
            result =>
            {
                SceneManager.LoadScene(LOADING_LOBBY_SCENE);
            }, Debug.LogError);
    }

    private void DeleteAccount()
    {
        PlayerPrefs.DeleteKey(UNIQUE_AUTH_KEY);
        CheckAccount();
    }

    private void OnLoginSuccess(LoginResult result)
    {
        _textStatus.text = "Successfully logged in PlayFab.";
        _textStatus.color = _colorSuccess;
        TryGetData();
    }

    private void TryGetData()
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

    private void GotUserData(GetUserDataResult result)
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

    private void OnLoginError(PlayFabError error)
    {
        var message = "<color=red>Failed to log into PlayFab</color>!";
        _textStatus.text = message;
        _textStatus.color = _colorFailure;
        Debug.LogError($"{message} {error}");
    }

    private void OnDestroy()
    {
        _signInButton.onClick.RemoveListener(CheckIdAndLogin);
        _deleteAccountButton.onClick.RemoveListener(DeleteAccount);
    }
}