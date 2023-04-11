using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


internal sealed class AnonymousAuthorization  : AccountDataWindowBase
{
    private TMP_Text _textButtonSignInAnonimous;
    private Button _signInButton;

    public AnonymousAuthorization(GeneralViews generalViews) : base(generalViews.ColorView)
    {
        _textStatus = generalViews.TextStatus;
        _signInButton = generalViews.AnonymousLoginView.SignInButton;
        _textButtonSignInAnonimous = _signInButton.GetComponentInChildren<TMP_Text>();
    }
    
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
}