using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Video;

public class PuzzleController : MonoBehaviour
{
    public QuizManager QuizManager;

    public Image[] questionImages;     // Gambar pertanyaan (atas)
    public VideoPlayer[] questionVideos;// Video pertanyaan (atas)
    public Image[] puzzleSlots;        // Slot jawaban (tengah)
    public Button[] answerButtons;     // Opsi jawaban (bawah)

    private int[] currentAnswers = new int[3]; // Jawaban saat ini di slot
    private int selectedAnswerIndex = -1;

    private PuzzleQuestion currentQuestion;
    public Action onIncorrectMatch;

    public void Setup(PuzzleQuestion data)
    {
        currentQuestion = data;

        // Tampilkan gambar pertanyaan
        for (int i = 0; i < 3; i++)
        {
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
        }

        // Reset slot kosong
        for (int i = 0; i < 3; i++)
        {
            puzzleSlots[i].sprite = null;
            currentAnswers[i] = -1;

            int slotIdx = i;
            Button btn = puzzleSlots[i].GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => OnSlotClicked(slotIdx));
            }
        }

        // Tampilkan opsi jawaban
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
            // Kembalikan jawaban ke opsi
            Debug.Log($"Jawaban di slot {slotIndex + 1} dikembalikan.");
            currentAnswers[slotIndex] = -1;
            puzzleSlots[slotIndex].sprite = null;
        }

        // Cek apakah semua slot sudah terisi
        bool allFilled = true;
        foreach (int ans in currentAnswers)
            if (ans == -1)
                allFilled = false;

        if (allFilled)
            CheckAnswers();
    }

    void CheckAnswers()
    {
        int correct = 0;
        for (int i = 0; i < 3; i++)
        {
            if (currentAnswers[i] == currentQuestion.correctPlacement[i])
                correct++;
        }

        if (correct == 3)
        {
            Debug.Log("Semua jawaban benar!");
            QuizManager.NextQuestion();
        }
        else
        {
            onIncorrectMatch?.Invoke();
            Debug.Log($"Jawaban salah. Benar {correct} dari 3. Reset ulang.");
            Setup(currentQuestion); // Reset soal
        }
    }
}
