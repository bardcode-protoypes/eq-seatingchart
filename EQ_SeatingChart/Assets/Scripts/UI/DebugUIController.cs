using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;

public class DebugUIController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private NotesSystem notesSystem;
    [SerializeField] private LocalizationManager localizationManager;
    private void OnEnable()
    {
        // Debug Note spawn
        var addNoteButton = this.uiDocument.rootVisualElement.Q<Button>("addNoteButton");
        if (addNoteButton != null)
        {
            addNoteButton.clicked += () =>
            {
                this.notesSystem.CreateNote("debug_note");
                Debug.Log("note spawned");
            };
        }
        // Language dropdown
        var languageDropdown = this.uiDocument.rootVisualElement.Q<DropdownField>("languageSelection");
        if (languageDropdown != null && this.localizationManager != null)
        {
            languageDropdown.choices.Clear();
            languageDropdown.choices = new System.Collections.Generic.List<string>(localizationManager.GetAvailableLocaleNames());
            languageDropdown.RegisterValueChangedCallback(evt =>
            {
                int selectedIndex = languageDropdown.index;
                if (selectedIndex >= 0 && selectedIndex < localizationManager.GetAvailableLocaleNames().Length)
                {
                    string selectedLocaleName = localizationManager.GetAvailableLocaleNames()[selectedIndex];
                    localizationManager.SetLocaleByName(selectedLocaleName);
                }
            });
        }
    }

}
