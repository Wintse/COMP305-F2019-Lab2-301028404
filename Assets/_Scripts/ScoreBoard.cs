using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Victoria Liu
/// 301028404
/// </summary>

[CreateAssetMenu(fileName = "ScoreBoard", menuName = "Game/ScoreBoard")]
[System.Serializable]
public class ScoreBoard : ScriptableObject
{
    public int highScore;
    public int lives;
    public int score;
}
