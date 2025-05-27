using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "MCImageQuestion", menuName = "Quiz/MC Image")]
public class MCImageQuestion : QuestionData
{
    [Header("Question Image/Video")]
    public Sprite questionVisual; // Bisa gambar/video thumbnail
    public VideoClip questionVideo;
    [Header("Answer Option")]
    public Sprite[] options = new Sprite[3];
    public int correctIndex;

    private void OnEnable()
    {
        questionType = QuestionType.MultipleChoiceImage;
    }
}
