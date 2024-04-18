using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public sealed class CreateAccountView : MonoBehaviour
{
    [SerializeField] private InputField _usernameField;
    [SerializeField] private InputField _passwordField;
    [SerializeField] private InputField _mailField;
    [SerializeField] private Button _createInButton;

    public InputField UsernameField => _usernameField;
    public InputField PasswordField => _passwordField;
    public InputField MailField => _mailField;
    public Button CreateInButton => _createInButton;
}