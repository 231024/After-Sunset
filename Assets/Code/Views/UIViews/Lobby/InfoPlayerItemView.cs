using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPlayerItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _labelPlayerName;

    public TMP_Text LabelPlayerName => _labelPlayerName;
}
