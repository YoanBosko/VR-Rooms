using UnityEngine;

[CreateAssetMenu(fileName = "MatchingQuestion", menuName = "Quiz/Matching")]
public class MatchingQuestion : QuestionData
{
    public Sprite[] items = new Sprite[5];
    public Sprite[] matches = new Sprite[5]; // Jawaban yang cocok (diacak saat tampil)
    public int[] correctPairs = new int[5]; // item[0] cocok dengan matches[correctPairs[0]], dst.

    private void OnEnable()
    {
        questionType = QuestionType.Matching;
    }
}
