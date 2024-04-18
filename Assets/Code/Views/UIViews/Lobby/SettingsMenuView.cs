
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class SettingsMenuView : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private Button _changeUsername;
    [SerializeField] private Button _quitGame;
    [SerializeField] private Button _closeSettingMenu;
    [SerializeField] private Slider _volumeMusic;
    [SerializeField] private Slider _volumeSounds;
    [SerializeField] private Transform _changeUsernamePanel;

    [Header("Menu Change Username")] 
    [SerializeField] private TMP_InputField _newUsername;
    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonConfirm;

    public Button ChangeUsername => _changeUsername;

    public Button QuitGame => _quitGame;

    public Button CloseSettingMenu => _closeSettingMenu;

    public Slider VolumeMusic => _volumeMusic;

    public Slider VolumeSounds => _volumeSounds;

    public Transform ChangeUsernamePanel => _changeUsernamePanel;

    public TMP_InputField NewUsername => _newUsername;

    public Button ButtonBack => _buttonBack;

    public Button ButtonConfirm => _buttonConfirm;
}
