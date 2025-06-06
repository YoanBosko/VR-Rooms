using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Video;

public class MCImageController : MonoBehaviour
{
    public QuizManager QuizManager;
    public Image questionImage;
    public VideoPlayer questionVideo;
    public Button[] optionButtons;
    public Action onIncorrectMatch; // event jika jawaban salah (akan kamu isi sendiri)

    public void Setup(MCImageQuestion data)
    {
        if (data.questionVisual != null)
        {
            questionImage.gameObject.SetActive(true);
            questionVideo.gameObject.SetActive(false);
            questionImage.sprite = data.questionVisual;
        }
        else if (data.questionVideo != null)
        {
            questionImage.gameObject.SetActive(false);
            questionVideo.gameObject.SetActive(true);
            questionVideo.clip = data.questionVideo;
        }
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].image.sprite = data.options[i];
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => {
                bool isCorrect = index == data.correctIndex;
                if (isCorrect) QuizManager.NextQuestion();
                else onIncorrectMatch?.Invoke();
                // Debug.Log(isCorrect ? "Benar!" : "Salah!");
            });
        }
    }
}

