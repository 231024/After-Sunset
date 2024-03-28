using TMPro;
using UnityEngine;


internal sealed class GeneralViews : MonoBehaviour
{
    [Header("TextLebels")] 
    [SerializeField] private TMP_Text _textStatus;

    [Header("Views")] 
    [SerializeField] private ColorView _colorView;
    [SerializeField] private AnonymousLoginView _anonymousLoginView;
    [SerializeField] private SignInAccountView _signInAccountView;
    [SerializeField] private CreateAccountView _createAccountView;
    [SerializeField] private ManagerLoginWindowView _managerLoginWindowView;

    public TMP_Text TextStatus => _textStatus;
    public ColorView ColorView => _colorView;
    public AnonymousLoginView AnonymousLoginView => _anonymousLoginView;
    public SignInAccountView SignInAccountView => _signInAccountView;
    public CreateAccountView CreateAccountView => _createAccountView;
    public ManagerLoginWindowView ManagerLoginWindowView => _managerLoginWindowView;
}