using UnityEngine;
using UnityEngine.UI;


internal sealed class AnonymousLoginView : MonoBehaviour
{
    [SerializeField] private Button _signInButton;

    public Button SignInButton => _signInButton;
}