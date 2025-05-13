using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCImageController : MonoBehaviour
{
    public Image questionImage;
    public Button[] optionButtons;

    public void Setup(MCImageQuestion data)
    {
        questionImage.sprite = data.questionVisual;
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].image.sprite = data.options[i];
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => {
                bool isCorrect = index == data.correctIndex;
                Debug.Log(isCorrect ? "Benar!" : "Salah!");
                FindObjectOfType<QuizManager>().NextQuestion();
            });
        }
    }
}

