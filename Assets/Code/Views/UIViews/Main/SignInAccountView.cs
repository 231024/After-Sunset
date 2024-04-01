using UnityEngine;
using UnityEngine.UI;


public sealed class SignInAccountView : MonoBehaviour
{
    [SerializeField] private InputField _usernameField;
    [SerializeField] private InputField _passwordField;
    [SerializeField] private Button _signInButton;

    public Button SignInButton => _signInButton;

    public InputField UsernameField => _usernameField;

    public InputField PasswordField => _passwordField;
}