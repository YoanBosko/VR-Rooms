using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "MCVideoQuestion", menuName = "Quiz/MC Video")]
public class MCVideoQuestion : QuestionData
{
    public VideoClip questionVideo;
    public VideoClip[] optionVideos = new VideoClip[3];
    public int correctIndex;

    private void OnEnable()
    {
        questionType = QuestionType.MultipleChoiceVideo;
    }
}

