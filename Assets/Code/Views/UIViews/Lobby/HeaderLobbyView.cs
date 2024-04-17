using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeaderLobbyView : MonoBehaviour
{
    
    [SerializeField] private TMP_Text _textUsername;
    [SerializeField] private TMP_Text _textProgress;
    [SerializeField] private TMP_Text _textLevel;
    
    public TMP_Text TextUsername => _textUsername;
    public TMP_Text TextProgress => _textProgress;
    public TMP_Text TextLevel => _textLevel;
}
