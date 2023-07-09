using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NamePicker 
{
  private static string[] _names;
  private static Dictionary<string, bool> _pickedNames;

  [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSceneLoad )]
  private static void Initialize()
  {
    TextAsset namesText = (TextAsset) Resources.Load("names");
    _names = namesText.text.Split("\n");
    _pickedNames = new Dictionary<string, bool>(100);
  }
  
  public static string PickName()
  {
    int randomValue = (int)(Random.value * _names.Length - 1);
    while (_pickedNames.ContainsKey(_names[randomValue]))
    {
      randomValue = (int)(Random.value * _names.Length - 1);
    }
    
    _pickedNames.Add(_names[randomValue], true);
    return _names[randomValue];
  }
  
}
