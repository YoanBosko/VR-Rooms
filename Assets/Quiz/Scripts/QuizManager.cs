using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    public Image[] healthIcon = new Image[3];
    public Sprite brokenHealthIcon;
    public Sprite normalHealthIcon;
    public UnityEvent onQuizClear;
    private int wrongCounter;

    public void Start()
    {
        slider.value = slider.minValue;
        Debug.Log("Slider terpanggil");
        currentIndex = 0;
        wrongCounter = 0;
        foreach (Image image in healthIcon)
        {
            image.sprite = normalHealthIcon;
        }
        LoadQuestion();
    }
    public void Update()
    {
        if (wrongCounter == 1)
        {
            healthIcon[0].sprite = brokenHealthIcon;
        }
        else if (wrongCounter == 2)
        {
            healthIcon[1].sprite = brokenHealthIcon;
        }
        else if (wrongCounter == 3)
        {
            Start();
        }

        if (slider.value == slider.maxValue)
        {
            onQuizClear?.Invoke();
        }
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
                mcImagePanel.GetComponent<MCImageController>().onIncorrectMatch = () =>
                {
                    wrongCounter++;
                };
                break;

            case QuestionType.Matching:
                matchingPanel.SetActive(true);
                matchingPanel.GetComponent<MatchingController>().Setup((MatchingQuestion)q);

                matchingPanel.GetComponent<MatchingController>().onIncorrectMatch = (qIndex, aIndex) =>
                {
                    // Debug.Log($"[QuizManager] Jawaban salah: Soal {qIndex}, Jawaban {aIndex}");
                    // Tambahkan efek suara, penalti, atau feedback visual di sini
                    wrongCounter++;
                };
                break;

            case QuestionType.Puzzle:
                puzzlePanel.SetActive(true);
                puzzlePanel.GetComponent<PuzzleController>().Setup((PuzzleQuestion)q);
                puzzlePanel.GetComponent<PuzzleController>().onIncorrectMatch = () =>
                {
                    wrongCounter++;
                };
                break;

            case QuestionType.MultipleChoiceVideo:
                mcVideoPanel.SetActive(true);
                mcVideoPanel.GetComponent<MCVideoController>().Setup((MCVideoQuestion)q);

                mcVideoPanel.GetComponent<MCVideoController>().onIncorrectMatch = () =>
                {
                    wrongCounter++;
                };
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
        {
            slider.value = slider.maxValue;
            HideAllPanels();
            Debug.Log("Selesai!");
        }
            
    }
}

