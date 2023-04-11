using TMPro;
using UnityEngine;


internal sealed class GeneralViews : MonoBehaviour
{
    [Header("TextLebels")] 
    [SerializeField] private TMP_Text _textStatus;

    [Header("Views")] 
    [SerializeField] private ColorView _colorView;
    [SerializeField] private AnonymousLoginView _anonymousLoginView;

    public TMP_Text TextStatus => _textStatus;
    public ColorView ColorView => _colorView;
    public AnonymousLoginView AnonymousLoginView => _anonymousLoginView;
}