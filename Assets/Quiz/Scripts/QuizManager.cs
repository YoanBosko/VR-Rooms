using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public QuestionData[] questions;
    [HideInInspector]public int currentIndex = 0;

    // Referensi ke panel UI untuk tiap jenis soal
    public GameObject mcImagePanel;
    public GameObject matchingPanel;
    public GameObject puzzlePanel;
    public GameObject mcVideoPanel;

    public Slider slider;

    public void Start()
    {
        slider.value = slider.minValue;
        Debug.Log("Slider terpanggil");
        LoadQuestion();
    }

    void LoadQuestion()
    {
        HideAllPanels();
        QuestionData q = questions[currentIndex];

        switch (q.questionType)
        {
            case QuestionType.MultipleChoiceImage:
                mcImagePanel.SetActive(true);
                // Kirim data ke UI controller panel ini
                mcImagePanel.GetComponent<MCImageController>().Setup((MCImageQuestion)q);
                break;

            case QuestionType.Matching:
                matchingPanel.SetActive(true);
                matchingPanel.GetComponent<MatchingController>().Setup((MatchingQuestion)q);
                break;

            case QuestionType.Puzzle:
                puzzlePanel.SetActive(true);
                puzzlePanel.GetComponent<PuzzleController>().Setup((PuzzleQuestion)q);
                break;

            case QuestionType.MultipleChoiceVideo:
                mcVideoPanel.SetActive(true);
                mcVideoPanel.GetComponent<MCVideoController>().Setup((MCVideoQuestion)q);
                break;
        }
    }

    void HideAllPanels()
    {
        mcImagePanel.SetActive(false);
        matchingPanel.SetActive(false);
        puzzlePanel.SetActive(false);
        mcVideoPanel.SetActive(false);
    }

    public void NextQuestion()
    {
        currentIndex++;
        if (currentIndex < questions.Length)
        {
            float value = (float)currentIndex / questions.Length * 100;
            // Debug.Log(value + " " + currentIndex + " " + questions.Length);
            slider.value = value;
            LoadQuestion();
        }
        else
            slider.value = slider.maxValue;
            Debug.Log("Selesai!");
    }
}

