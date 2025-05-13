using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleQuestion", menuName = "Quiz/Puzzle")]
public class PuzzleQuestion : QuestionData
{
    public Sprite[] targets = new Sprite[3]; // Target lokasi
    public Sprite[] options = new Sprite[3]; // Gambar yang akan ditempatkan
    public int[] correctPlacement = new int[3]; // option[0] untuk target[correctPlacement[0]], dst.

    private void OnEnable()
    {
        questionType = QuestionType.Puzzle;
    }
}

