using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "MCVideoQuestion", menuName = "Quiz/MC Video")]
public class MCVideoQuestion : QuestionData
{
    [Header("Question Video/Image")]
    public bool isQuestionUsingVideo;
    public VideoClip questionVideo;
    public Sprite questionVisual;
    [Header("Answer Option Button Image")]
    public Sprite[] options = new Sprite[3];
    [Header("Answer Image/Video/Empty")]
    public AnswerTypes answerType = AnswerTypes.Empty;
    public Sprite[] optionsImageType = new Sprite[3];
    public VideoClip[] optionVideosType = new VideoClip[3];

    public int correctIndex;

    public enum AnswerTypes
    {
        Empty,
        Image,
        Video
    }
    private void OnEnable()
    {
        questionType = QuestionType.MultipleChoiceVideo;
    }
    public void OnValidate()
    {
        
    }
}

