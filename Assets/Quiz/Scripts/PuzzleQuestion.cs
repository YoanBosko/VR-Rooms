using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleQuestion", menuName = "Quiz/Puzzle")]
public class PuzzleQuestion : QuestionData
{
    [Header("Question Image")]
    public Sprite[] questions = new Sprite[3]; // Target lokasi
    [Header("Slot option")]
    public Sprite[] targets = new Sprite[3]; // Target lokasi
    [Header("Answer option")]
    public Sprite[] options = new Sprite[3]; // Gambar yang akan ditempatkan
    [Tooltip("option[0] untuk target[correctPlacement[0]]")]
    public int[] correctPlacement = new int[3]; // option[0] untuk target[correctPlacement[0]], dst.

    private void OnEnable()
    {
        questionType = QuestionType.Puzzle;
    }
}

