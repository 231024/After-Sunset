using System;
using UnityEngine;
using UnityEngine.UI;


public class ManagerLoginWindow : MonoBehaviour
{
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _createAccountButton;
    [SerializeField] private Button _signInBackButton;
    [SerializeField] private Button _createAccountBackButton;
    [SerializeField] private Button _quitButton;

    [SerializeField] private Canvas _enterInGameCanvas;
    [SerializeField] private Canvas _createAccountCanvas;
    [SerializeField] private Canvas _signInCanvas;

    private void Start()
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

    private void OnDestroy()
    {
        _signInButton.onClick.RemoveListener(OpenSignInWindow);
        _createAccountButton.onClick.RemoveListener(OpenCreateAccountWindow);
        _signInBackButton.onClick.RemoveListener(CloseSignInWindow);
        _createAccountBackButton.onClick.RemoveListener(CloseCreateAccountWindow);
        _quitButton.onClick.RemoveListener(Quit);
    }
}