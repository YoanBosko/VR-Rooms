using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MatchingController : MonoBehaviour
{
    public Button[] questionButtons;
    public Button[] answerButtons;

    private int selectedQuestionIndex = -1;
    private bool[] isMatched = new bool[5];

    public Action<int, int> onIncorrectMatch; // event jika jawaban salah (akan kamu isi sendiri)

    private MatchingQuestion currentData;

    public void Setup(MatchingQuestion data)
    {
        currentData = data;
        selectedQuestionIndex = -1;

        for (int i = 0; i < 5; i++)
        {
            isMatched[i] = false;

            int idx = i;
            questionButtons[i].gameObject.SetActive(true);
            answerButtons[i].gameObject.SetActive(true);

            questionButtons[i].image.sprite = data.items[i];
            questionButtons[i].onClick.RemoveAllListeners();
            questionButtons[i].onClick.AddListener(() => OnQuestionSelected(idx));

            answerButtons[i].image.sprite = data.matches[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(idx));
        }
    }

    void OnQuestionSelected(int index)
    {
        if (isMatched[index]) return; // sudah terpasangkan
        selectedQuestionIndex = index;
        Debug.Log($"Soal ke-{index + 1} dipilih.");
    }

    void OnAnswerSelected(int answerIndex)
    {
        if (selectedQuestionIndex == -1)
        {
            Debug.Log("Pilih soal terlebih dahulu!");
            return;
        }

        int correctAnswerIndex = currentData.correctPairs[selectedQuestionIndex];

        if (answerIndex == correctAnswerIndex)
        {
            Debug.Log("Jawaban BENAR!");

            // Sembunyikan pasangan
            questionButtons[selectedQuestionIndex].gameObject.SetActive(false);
            answerButtons[answerIndex].gameObject.SetActive(false);
            isMatched[selectedQuestionIndex] = true;

            // Cek apakah semua pasangan sudah terjawab
            bool allDone = true;
            foreach (bool b in isMatched)
                if (!b) allDone = false;

            if (allDone)
            {
                Debug.Log("Semua pasangan benar! Lanjut ke soal berikutnya.");
                FindObjectOfType<QuizManager>().NextQuestion();
            }
        }
        else
        {
            Debug.Log("Jawaban SALAH!");
            onIncorrectMatch?.Invoke(selectedQuestionIndex, answerIndex);
        }

        selectedQuestionIndex = -1;
    }
}
