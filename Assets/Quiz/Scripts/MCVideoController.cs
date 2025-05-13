using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MCVideoController : MonoBehaviour
{
    public VideoPlayer questionVideo; // opsional jika ada visual pertanyaan
    public VideoPlayer[] optionPlayers; // 3 video player
    public Button[] optionButtons;

    public void Setup(MCVideoQuestion data)
    {
        if (questionVideo != null && data.questionVideo != null)
            questionVideo.clip = data.questionVideo;

        for (int i = 0; i < optionPlayers.Length; i++)
        {
            int index = i;
            optionPlayers[i].clip = data.optionVideos[i];
            optionPlayers[i].Play();

            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() =>
            {
                bool isCorrect = index == data.correctIndex;
                Debug.Log(isCorrect ? "Benar!" : "Salah!");
                FindObjectOfType<QuizManager>().NextQuestion();
            });
        }
    }
}
