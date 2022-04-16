using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Polar
{
    public class SelectLocale : MonoBehaviour
    {
      [SerializeField]
      private Locale locale;

      public void SetLocale(){
          LocalizationSettings.SelectedLocale = locale;
          // tallenna player prefeihin
      }
    }
}
