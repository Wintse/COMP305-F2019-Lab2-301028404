using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Victoria Liu
/// 301028404
/// </summary>

[CreateAssetMenu(fileName = "SceneSettings", menuName = "Scene/Settings")]
[System.Serializable]
public class SceneSettings : ScriptableObject
{
    [Header("Scene Config")]
    public Scene scene;
    public SoundClip activeSoundClip;

    [Header("Scoreboard labels")]
    public bool scoreLabelEnabled;
    public bool livesLabelEnabled;
    public bool highScoreLabelEnabled;

    [Header("Scene Label ")]
    public bool startLabelSetActive;
    public bool endLabelSetActive;

    [Header("Scene Buttons")]
    public bool startButtonSetActive;
    public bool restartButtonSetActive;
}
