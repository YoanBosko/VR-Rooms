using UnityEngine;

public enum QuestionType
{
    MultipleChoiceImage,
    Matching,
    Puzzle,
    MultipleChoiceVideo
}

public abstract class QuestionData : ScriptableObject
{
    public QuestionType questionType;
}
