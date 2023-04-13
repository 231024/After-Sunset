using UnityEngine;
using UnityEngine.UI;


public class ManagerLoginWindowView : MonoBehaviour
{
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _createAccountButton;
    [SerializeField] private Button _signInBackButton;
    [SerializeField] private Button _createAccountBackButton;
    [SerializeField] private Button _quitButton;

    [SerializeField] private Canvas _enterInGameCanvas;
    [SerializeField] private Canvas _createAccountCanvas;
    [SerializeField] private Canvas _signInCanvas;
    [SerializeField] private Canvas _authorizationCanvas;

    public Button SignInButton => _signInButton;

    public Button CreateAccountButton => _createAccountButton;

    public Button SignInBackButton => _signInBackButton;

    public Button CreateAccountBackButton => _createAccountBackButton;

    public Button QuitButton => _quitButton;

    public Canvas EnterInGameCanvas => _enterInGameCanvas;

    public Canvas CreateAccountCanvas => _createAccountCanvas;

    public Canvas SignInCanvas => _signInCanvas;

    public Canvas AuthorizationCanvas => _authorizationCanvas;
}