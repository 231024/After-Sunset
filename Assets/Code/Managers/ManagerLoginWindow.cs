using UnityEngine;
using UnityEngine.UI;


internal sealed class ManagerLoginWindow : IInitialization, ICleanup
{
    private Button _signInButton;
    private Button _createAccountButton;
    private Button _signInBackButton;
    private Button _createAccountBackButton;
    private Button _quitButton;

    private Canvas _enterInGameCanvas;
    private Canvas _createAccountCanvas;
    private Canvas _signInCanvas;

    public ManagerLoginWindow(ManagerLoginWindowView view)
    {
        _signInButton = view.SignInButton;
        _createAccountButton = view.CreateAccountButton;
        _signInBackButton = view.SignInBackButton;
        _createAccountBackButton = view.CreateAccountBackButton;
        _quitButton = view.QuitButton;
        _enterInGameCanvas = view.EnterInGameCanvas;
        _createAccountCanvas = view.CreateAccountCanvas;
        _signInCanvas = view.SignInCanvas;
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

    public void Cleanup()
    {
        _signInButton.onClick.RemoveListener(OpenSignInWindow);
        _createAccountButton.onClick.RemoveListener(OpenCreateAccountWindow);
        _signInBackButton.onClick.RemoveListener(CloseSignInWindow);
        _createAccountBackButton.onClick.RemoveListener(CloseCreateAccountWindow);
        _quitButton.onClick.RemoveListener(Quit);
    }
}