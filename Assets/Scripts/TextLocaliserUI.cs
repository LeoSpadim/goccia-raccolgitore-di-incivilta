using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliserUI : MonoBehaviour {
  TextMeshProUGUI textField;
  public string key;

  private void Update(){
    textField = GetComponent<TextMeshProUGUI>();
    string value = Localisation.GetLocalisedValue(key);
    textField.text = value;
  }
}
