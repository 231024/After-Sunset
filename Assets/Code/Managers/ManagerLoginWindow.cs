using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;


internal sealed class ManagerLoginWindow : IStartable, IDisposable
{
    private Button _signInButton;
    private Button _createAccountButton;
    private Button _signInBackButton;
    private Button _createAccountBackButton;
    private Button _quitButton;

    private Canvas _enterInGameCanvas;
    private Canvas _createAccountCanvas;
    private Canvas _signInCanvas;

    [Inject] public GeneralViews _generalViews;

    public ManagerLoginWindow(GeneralViews generalViews)
    {
        _generalViews = generalViews;
    }
    
    public void Start()
    {
        _signInButton = _generalViews.ManagerLoginWindowView.SignInButton;
        _createAccountButton = _generalViews.ManagerLoginWindowView.CreateAccountButton;
        _signInBackButton = _generalViews.ManagerLoginWindowView.SignInBackButton;
        _createAccountBackButton = _generalViews.ManagerLoginWindowView.CreateAccountBackButton;
        _quitButton = _generalViews.ManagerLoginWindowView.QuitButton;
        _enterInGameCanvas = _generalViews.ManagerLoginWindowView.EnterInGameCanvas;
        _createAccountCanvas = _generalViews.ManagerLoginWindowView.CreateAccountCanvas;
        _signInCanvas = _generalViews.ManagerLoginWindowView.SignInCanvas;
    }

    public void Initialization()
    {
        _signInButton.onClick.AddListener(OpenSignInWindow);
        _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
        _signInBackButton.onClick.AddListener(CloseSignInWindow);
        _createAccountBackButton.onClick.AddListener(CloseCreateAccountWindow);
        _quitButton.onClick.AddListener(Quit);
    }

    private void OpenSignInWindow()
    {
        _signInCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }

    private void OpenCreateAccountWindow()
    {
        _createAccountCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }

    private void CloseSignInWindow()
    {
        _enterInGameCanvas.enabled = true;
        _signInCanvas.enabled = false;
    }

    private void CloseCreateAccountWindow()
    {
        _enterInGameCanvas.enabled = true;
        _createAccountCanvas.enabled = false;
    }
    
    private void Quit()
    {
        Application.Quit();
    }

    public void Dispose()
    {
        _signInButton.onClick.RemoveListener(OpenSignInWindow);
        _createAccountButton.onClick.RemoveListener(OpenCreateAccountWindow);
        _signInBackButton.onClick.RemoveListener(CloseSignInWindow);
        _createAccountBackButton.onClick.RemoveListener(CloseCreateAccountWindow);
        _quitButton.onClick.RemoveListener(Quit);
    }
}