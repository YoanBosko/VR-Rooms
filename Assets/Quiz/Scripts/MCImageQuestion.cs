using UnityEngine;

[CreateAssetMenu(fileName = "MCImageQuestion", menuName = "Quiz/MC Image")]
public class MCImageQuestion : QuestionData
{
    public Sprite questionVisual; // Bisa gambar/video thumbnail
    public Sprite[] options = new Sprite[3];
    public int correctIndex;

    private void OnEnable()
    {
        questionType = QuestionType.MultipleChoiceImage;
    }
}
