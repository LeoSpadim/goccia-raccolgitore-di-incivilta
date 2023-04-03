using System.Collections.Generic;

public class Localisation {
  public enum Language {
    English,
    Italian
  }

  public static Language language = Language.English;

  private static Dictionary<string, string> localisedEN;
  private static Dictionary<string, string> localisedIT;

  public static bool isInit;

  public static void Init() {
    CSVLoader csvLoader = new CSVLoader();
    csvLoader.LoadCSV();

    localisedEN = csvLoader.GetDictionaryValues("en");
    localisedIT = csvLoader.GetDictionaryValues("it");

    isInit = true;
  }

  public static string GetLocalisedValue(string key) {
    if (!isInit) { Init(); }

    string value = key;
    switch (language){
      case Language.English:
        localisedEN.TryGetValue(key, out value);
        break;
      case Language.Italian:
        localisedIT.TryGetValue(key, out value);
        break;
    }

    return value;
  }
}
