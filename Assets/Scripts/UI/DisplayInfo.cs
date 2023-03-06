using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class DisplayInfo : IView
{
    //internal static InformationObjects _textLines;
    //private List<Text> _texts;
    private readonly string _firstKeyText;
    private readonly string _secondKeyText;
    private readonly string _thirdKeyText;
    private readonly string _firstText;
    private readonly string _secondText;
    private readonly string _thirdKText;
    private int _health;
    private int _bullets = 50;
    private int _mines = 5;

    public string FirstKeyText { get; }
    public string FirstText { get; }

    private Dictionary<string, Text> TextObjects { get; }

    public DisplayInfo(IHealth playerHealth)
    {
        _firstKeyText = "Health";
        _secondKeyText = "Bullets";
        _thirdKeyText = "Mines";
        _firstText = "Health: ";
        _secondText = "Bullets: ";
        _thirdKText = "Mines: ";
        
        
        FirstKeyText = _firstKeyText;
        FirstText = _firstText;
        _health = playerHealth.PlayerHealth;
            
        var texts = Object.FindObjectsOfType<Text>().ToList();
        TextObjects = new Dictionary<string, Text>();
        TextObjects.Add(_firstKeyText, texts[0]);
        TextObjects.Add(_secondKeyText, texts[1]);
        TextObjects.Add(_thirdKeyText, texts[2]);

        TextObjects[_firstKeyText].text = _firstText + _health;
        TextObjects[_secondKeyText].text = _secondText + _bullets;
        TextObjects[_thirdKeyText].text = _thirdKText + _mines;
            
        //_texts = Object.FindObjectsOfType<Text>().ToDictionary(k => k.name, v => v.GetComponent<Text>());
    }

    public void Display(string nameOfKey, int numbers, string message)
    {
        var textMassage = ($"{message} {numbers}");
        TextObjects[nameOfKey].text = textMassage;
    }
}