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
    [SerializeField] private TMP_Text _buttonSignInText;
    [SerializeField] private TMP_Text _textStatus;
    
    [Header("Buttons")] 
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _deleteAccountButton;
    [SerializeField] private Button _quitButton;
    
    [Header("StatusColor")] 
    [SerializeField] private Color _colorLoading;
    [SerializeField] private Color _colorSuccess;
    [SerializeField] private Color _colorFailure;

    private string _username;

    private const string AUTH_KEY = "player-unique-id";

    private void Start()
    {
        _signInButton.onClick.AddListener(TryLogin);
        _deleteAccountButton.onClick.AddListener(DeleteAccount);
        _quitButton.onClick.AddListener(Quit);
        CheckAccount();
    }

    private void CheckAccount()
    {
        if (PlayerPrefs.HasKey(AUTH_KEY))
        {
            _buttonSignInText.text = "Sign in";
        }
        else
        {
            _buttonSignInText.text = "Register";
        }
    }

    public void UpdateUsername(string username)
    {
        _username = username;
    }

    private void TryLogin()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "2885B";
            Debug.Log("Successfully set the title ID.");
        }

        var needCreation = !PlayerPrefs.HasKey(AUTH_KEY);
        Debug.Log($"needCreation: {needCreation}");
        
        var id = PlayerPrefs.GetString(AUTH_KEY, Guid.NewGuid().ToString());
        Debug.Log($"id: {id}");
        
        var request = new LoginWithCustomIDRequest { CustomId = id, CreateAccount = needCreation };
        PlayFabClientAPI.LoginWithCustomID(request, result =>
        {
            var message = "PlayFab Success";
            _textStatus.text = message;
            _textStatus.color = _colorSuccess;
            PlayerPrefs.SetString(AUTH_KEY, id);
            Debug.Log(message);
            OnLoginSuccess(result);
            if (needCreation)
            {
                CreateInitialUsername();
            }
            else
            {
                SceneManager.LoadScene("MainProfile");
            }
        }, OnLoginFail);
        
        _textStatus.text = "Signing in...";
        _textStatus.color = _colorLoading;
    }

    private void CreateInitialUsername()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = $"Player {Random.Range(1000, 10000)}" },
            result =>
            {
                SceneManager.LoadScene("MainProfile");
            }, Debug.LogError);
    }

    private void DeleteAccount()
    {
        PlayerPrefs.DeleteKey(AUTH_KEY);
        CheckAccount();
    }

    private void OnLoginSuccess(LoginResult result)
    {
        var message = "Successfully logged in PlayFab.";
        _textStatus.text = message;
        _textStatus.color = _colorSuccess;
        TryGetData();
    }

    private void TryGetData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
            {
                Keys = new List<string>
                    { GameConstants.SCORE_ID, GameConstants.TOTAL_SCORE_ID, GameConstants.LEVEL_ID }
            }, GotUserData, Debug.LogError
        );
    }

    private void GotUserData(GetUserDataResult result)
    {
        var dataToWrite = new Dictionary<string, string>();
        var data = result.Data;

        if (!data.ContainsKey(GameConstants.SCORE_ID))
        {
            dataToWrite.Add(GameConstants.SCORE_ID, 0.ToString());
        }
        
        if (!data.ContainsKey(GameConstants.TOTAL_SCORE_ID))
        {
            dataToWrite.Add(GameConstants.TOTAL_SCORE_ID, 0.ToString());
        }
        
        if (!data.ContainsKey(GameConstants.LEVEL_ID))
        {
            dataToWrite.Add(GameConstants.LEVEL_ID, 0.ToString());
        }

        if (dataToWrite.Count > 0)
        {
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = dataToWrite
                }, dataResult =>
                {
                    Debug.Log($"Created initial user data");
                },
                Debug.LogError);
        }
    }

    private void OnLoginFail(PlayFabError error)
    {
        var message = "<color=red>Failed to log into PlayFab</color>!";
        _textStatus.text = message;
        _textStatus.color = _colorFailure;
        Debug.LogError($"{message} {error}");
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        _signInButton.onClick.RemoveListener(TryLogin);
        _deleteAccountButton.onClick.RemoveListener(DeleteAccount);
        _quitButton.onClick.RemoveListener(Quit);
    }
}