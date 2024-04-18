using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class InfoRoomItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _labelRoomName;
    [SerializeField] private TMP_Text _labelAmountPlayers;
    [SerializeField] private TMP_Text _labelMapName;

    [SerializeField] private Button _infoRoomItemButton;

    public TMP_Text LabelRoomName => _labelRoomName;
    public TMP_Text LabelAmountPlayers => _labelAmountPlayers;
    public TMP_Text LabelMapName => _labelMapName;

    public Button InfoRoomItemButton => _infoRoomItemButton;
}
