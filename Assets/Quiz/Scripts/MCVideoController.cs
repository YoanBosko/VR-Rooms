using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class MCVideoController : MonoBehaviour
{
    [Header("Question")]
    public Image questionImage;
    public VideoPlayer questionVideo;

    [Header("Answer")]
    public Button[] optionButtons = new Button[3];
    public Image[] answerImages;
    public VideoPlayer[] answerVideos;

    public Action onIncorrectMatch; // event jika jawaban salah (akan kamu isi sendiri)

    public void Setup(MCVideoQuestion data)
    {
        if (data.isQuestionUsingVideo == false)
        {
            questionImage.gameObject.SetActive(true);
            questionVideo.gameObject.SetActive(false);
            questionImage.sprite = data.questionVisual;
        }
        else
        {
            questionImage.gameObject.SetActive(false);
            questionVideo.gameObject.SetActive(true);
            questionVideo.clip = data.questionVideo;
        }


        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].image.sprite = data.options[i];

            if (data.answerType == MCVideoQuestion.AnswerTypes.Image)
            {
                answerImages[i].gameObject.SetActive(true);
                answerVideos[i].gameObject.SetActive(false);
                answerImages[i].sprite = data.optionsImageType[i];
            }
            else if (data.answerType == MCVideoQuestion.AnswerTypes.Video)
            {
                answerImages[i].gameObject.SetActive(false);
                answerVideos[i].gameObject.SetActive(true);
                answerVideos[i].clip = data.optionVideosType[i];
            }
            else
            {
                answerImages[i].gameObject.SetActive(false);
                answerVideos[i].gameObject.SetActive(false);
            }


            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => {
                bool isCorrect = index == data.correctIndex;
                if (isCorrect) FindObjectOfType<QuizManager>().NextQuestion();
                else onIncorrectMatch?.Invoke();
                // Debug.Log(isCorrect ? "Benar!" : "Salah!");
            });
        }
    }
}
