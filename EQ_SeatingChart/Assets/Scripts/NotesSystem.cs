using UnityEngine;

public class NotesSystem : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform deskArea; // Parent canvas area for notes

    public void SpawnNote(string message)
    {
        GameObject note = Instantiate(notePrefab, deskArea);
        RectTransform noteRect = note.GetComponent<RectTransform>();

        // Randomize spawn position + rotation for “thrown” effect
        noteRect.anchoredPosition = new Vector2(Random.Range(-200, 200), Random.Range(-100, 100));
        noteRect.localRotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));

        // Set text
        note.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = message;
    }
}