using UnityEditor;
using UnityEngine;

public class NamePicker 
{
  private static string[] _names;

  [RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.BeforeSceneLoad )]
  private static void Initialize()
  {
    TextAsset namesText = (TextAsset) Resources.Load("names");
    _names = namesText.text.Split("\n");
  }
  
  public static string PickName()
  {
    return _names[(int)(Random.value*_names.Length-1)];
  }
  
}
