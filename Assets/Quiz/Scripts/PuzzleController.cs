using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Video;

public class PuzzleController : MonoBehaviour
{
    public QuizManager QuizManager;

    [Header("Elemen Pertanyaan (Atas)")]
    public Image[] questionImages;      // Array untuk gambar pertanyaan
    public VideoPlayer[] questionVideos;// Array untuk video pertanyaan

    [Header("Elemen Jawaban")]
    public Image[] puzzleSlots;         // Array untuk slot jawaban (tengah)
    public Button[] answerButtons;       // Array untuk tombol opsi jawaban (bawah)

    private int[] currentAnswers;       // Jawaban saat ini di slot, ukurannya akan diatur secara dinamis
    private int selectedAnswerIndex = -1;

    private PuzzleQuestion currentQuestion;
    public Action onIncorrectMatch;

    /// <summary>
    /// Awake dipanggil saat skrip diinisialisasi.
    /// Digunakan untuk mengatur ukuran array 'currentAnswers' berdasarkan jumlah slot puzzle.
    /// </summary>
    void Awake()
    {
        // PERUBAHAN: Ukuran array 'currentAnswers' sekarang dinamis,
        // disesuaikan dengan jumlah 'puzzleSlots' yang Anda atur di Inspector.
        currentAnswers = new int[puzzleSlots.Length];
    }

    public void Setup(PuzzleQuestion data)
    {
        currentQuestion = data;

        // PERUBAHAN: Perulangan sekarang menggunakan 'puzzleSlots.Length'.
        // Pastikan jumlah elemen di 'questionImages', 'questionVideos', dan 'data.questions'
        // sama dengan jumlah 'puzzleSlots'.
        for (int i = 0; i < puzzleSlots.Length; i++)
        {
            // Tampilkan gambar atau video pertanyaan
            if (data.isQuestionUsingVideo == false)
            {
                questionImages[i].gameObject.SetActive(true);
                questionVideos[i].gameObject.SetActive(false);
                questionImages[i].sprite = data.questions[i];
            }
            else
            {
                questionImages[i].gameObject.SetActive(false);
                questionVideos[i].gameObject.SetActive(true);
                questionVideos[i].clip = data.questionVideo[i];
            }

            // Reset slot jawaban
            puzzleSlots[i].sprite = null;
            currentAnswers[i] = -1;

            // Atur listener untuk slot
            int slotIdx = i;
            Button btn = puzzleSlots[i].GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => OnSlotClicked(slotIdx));
            }
        }

        // Tampilkan opsi jawaban. Loop ini sudah dinamis menggunakan answerButtons.Length.
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int idx = i;
            answerButtons[i].image.sprite = data.options[i];
            answerButtons[i].interactable = true;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerClicked(idx));
        }

        selectedAnswerIndex = -1;
    }

    void OnAnswerClicked(int index)
    {
        selectedAnswerIndex = index;
        Debug.Log($"Jawaban {index + 1} dipilih. Klik slot untuk menempatkan.");
    }

    void OnSlotClicked(int slotIndex)
    {
        if (selectedAnswerIndex != -1)
        {
            // Tempatkan jawaban
            puzzleSlots[slotIndex].sprite = answerButtons[selectedAnswerIndex].image.sprite;
            currentAnswers[slotIndex] = selectedAnswerIndex;
            selectedAnswerIndex = -1;
        }
        else if (currentAnswers[slotIndex] != -1)
        {
            // Kembalikan jawaban ke opsi jika slot diklik lagi
            Debug.Log($"Jawaban di slot {slotIndex + 1} dikembalikan.");
            currentAnswers[slotIndex] = -1;
            puzzleSlots[slotIndex].sprite = null; // Kosongkan gambar di slot
        }

        // Cek apakah semua slot sudah terisi
        bool allFilled = true;
        foreach (int ans in currentAnswers)
        {
            if (ans == -1)
            {
                allFilled = false;
                break; // Keluar dari loop jika satu slot saja masih kosong
            }
        }

        if (allFilled)
            CheckAnswers();
    }

    void CheckAnswers()
    {
        int correct = 0;
        // PERUBAHAN: Perulangan untuk memeriksa jawaban sekarang menggunakan 'puzzleSlots.Length'.
        for (int i = 0; i < puzzleSlots.Length; i++)
        {
            if (currentAnswers[i] == currentQuestion.correctPlacement[i])
                correct++;
        }

        // PERUBAHAN: Kondisi kemenangan dan pesan log sekarang dinamis.
        if (correct == puzzleSlots.Length)
        {
            Debug.Log("Semua jawaban benar!");
            QuizManager.NextQuestion();
        }
        else
        {
            onIncorrectMatch?.Invoke();
            Debug.Log($"Jawaban salah. Benar {correct} dari {puzzleSlots.Length}. Reset ulang.");
            Setup(currentQuestion); // Reset soal
        }
    }
}
