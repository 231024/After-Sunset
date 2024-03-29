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

    [Inject] public MainGeneralViews MainGeneralViews;

    public ManagerLoginWindow(MainGeneralViews mainGeneralViews)
    {
        MainGeneralViews = mainGeneralViews;
        
    }
    
    public void Start()
    {
        _signInButton = MainGeneralViews.ManagerLoginWindowView.SignInButton;
        _createAccountButton = MainGeneralViews.ManagerLoginWindowView.CreateAccountButton;
        _signInBackButton = MainGeneralViews.ManagerLoginWindowView.SignInBackButton;
        _createAccountBackButton = MainGeneralViews.ManagerLoginWindowView.CreateAccountBackButton;
        _quitButton = MainGeneralViews.ManagerLoginWindowView.QuitButton;
        _enterInGameCanvas = MainGeneralViews.ManagerLoginWindowView.EnterInGameCanvas;
        _createAccountCanvas = MainGeneralViews.ManagerLoginWindowView.CreateAccountCanvas;
        _signInCanvas = MainGeneralViews.ManagerLoginWindowView.SignInCanvas;
        
        
        _signInButton.onClick.AddListener(OpenSignInWindow);
        _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
        _signInBackButton.onClick.AddListener(CloseSignInWindow);
        _createAccountBackButton.onClick.AddListener(CloseCreateAccountWindow);
        _quitButton.onClick.AddListener(Quit);
    }

    // public void Initialization()
    // {
    //     _signInButton.onClick.AddListener(OpenSignInWindow);
    //     _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
    //     _signInBackButton.onClick.AddListener(CloseSignInWindow);
    //     _createAccountBackButton.onClick.AddListener(CloseCreateAccountWindow);
    //     _quitButton.onClick.AddListener(Quit);
    // }

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