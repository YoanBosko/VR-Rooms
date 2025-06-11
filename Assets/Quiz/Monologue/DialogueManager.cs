using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Gunakan namespace ini jika Anda memakai TextMeshPro (sangat direkomendasikan)
// using UnityEngine.UI; // Gunakan ini jika Anda masih memakai komponen Text lama

/// <summary>
/// Kelas ini berfungsi untuk menyimpan satu baris dialog beserta durasinya.
/// [System.Serializable] membuatnya dapat terlihat dan diatur di Inspector Unity.
/// </summary>
[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 10)]
    public string text;

    [Tooltip("Durasi teks akan ditampilkan di layar (dalam detik)")]
    public float duration;
}

/// <summary>
/// Komponen utama yang mengelola alur dialog.
/// Pasang skrip ini pada sebuah GameObject di dalam Scene Anda.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [Header("Komponen UI")]
    [Tooltip("Objek TextMeshPro untuk menampilkan teks dialog.")]
    public Text dialogueTextObject; // Ganti ke public Text jika memakai Text lama
    
    [Tooltip("GameObject Canvas yang menjadi induk dari semua elemen UI dialog.")]
    public GameObject dialogueCanvasObject;

    [Header("Konten Dialog")]
    [Tooltip("Daftar urutan teks dialog yang akan ditampilkan.")]
    public List<DialogueLine> dialogueSequence;

    private int currentLineIndex = 0;

    // Method ini akan dipanggil secara otomatis saat game dimulai
    void Start()
    {
        // Validasi untuk memastikan semua variabel telah diatur di Inspector
        if (dialogueTextObject == null || dialogueCanvasObject == null)
        {
            Debug.LogError("Error: Komponen UI (Text atau Canvas) belum diatur di Dialogue Manager. Silakan hubungkan melalui Inspector.");
            return; // Hentikan eksekusi jika ada yang belum diatur
        }

        if (dialogueSequence == null || dialogueSequence.Count == 0)
        {
            Debug.LogWarning("Warning: Tidak ada dialog yang diatur di Dialogue Manager.");
            dialogueCanvasObject.SetActive(false); // Nonaktifkan canvas jika tidak ada dialog
            return;
        }

        // Memulai Coroutine untuk menjalankan alur dialog
        StartCoroutine(PlayDialogueSequence());
    }

    /// <summary>
    /// Coroutine untuk menjalankan alur dialog satu per satu.
    /// </summary>
    private IEnumerator PlayDialogueSequence()
    {
        // Pastikan canvas aktif saat dialog dimulai
        dialogueCanvasObject.SetActive(true);
        
        // Loop melalui setiap baris dialog dalam daftar
        foreach (var line in dialogueSequence)
        {
            // Update teks pada UI
            dialogueTextObject.text = line.text;
            
            // Tunggu sesuai durasi yang telah ditentukan
            yield return new WaitForSeconds(line.duration);
        }
        
        // Setelah semua dialog selesai, nonaktifkan GameObject Canvas
        Debug.Log("Dialog selesai.");
        dialogueCanvasObject.SetActive(false);
    }
}
