using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    public Image[] puzzleSlots;     // Slot pertanyaan
    public Button[] answerButtons;  // 3 opsi jawaban

    private int[] currentAnswers = new int[3]; // indeks jawaban yang ditempatkan
    private int selectedAnswerIndex = -1;

    public void Setup(PuzzleQuestion data)
    {
        for (int i = 0; i < 3; i++)
        {
            puzzleSlots[i].sprite = data.targets[i];
            currentAnswers[i] = -1;
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int idx = i;
            answerButtons[i].image.sprite = data.options[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerClicked(idx));
        }

        // Tambahkan listener ke slot juga jika mau (misalnya user klik slot untuk mengisi)
    }

    void OnAnswerClicked(int index)
    {
        selectedAnswerIndex = index;
        Debug.Log($"Jawaban {index + 1} dipilih, pilih slot tujuan.");

        for (int i = 0; i < puzzleSlots.Length; i++)
        {
            int slotIdx = i;
            Button btn = puzzleSlots[i].GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => PlaceAnswer(slotIdx));
            }
        }
    }

    void PlaceAnswer(int slotIndex)
    {
        if (selectedAnswerIndex == -1) return;

        puzzleSlots[slotIndex].sprite = answerButtons[selectedAnswerIndex].image.sprite;
        currentAnswers[slotIndex] = selectedAnswerIndex;
        selectedAnswerIndex = -1;

        Debug.Log($"Jawaban ditempatkan di slot {slotIndex + 1}");

        // Cek apakah semua slot sudah terisi
        bool allFilled = true;
        foreach (int a in currentAnswers)
            if (a == -1)
                allFilled = false;

        if (allFilled)
            CheckAnswers();
    }

    void CheckAnswers()
    {
        PuzzleQuestion data = (PuzzleQuestion)FindObjectOfType<QuizManager>().questions[
            FindObjectOfType<QuizManager>().currentIndex];

        int score = 0;
        for (int i = 0; i < 3; i++)
            if (currentAnswers[i] == data.correctPlacement[i])
                score++;

        Debug.Log($"Benar {score} dari 3 slot.");
        FindObjectOfType<QuizManager>().NextQuestion();
    }
}
