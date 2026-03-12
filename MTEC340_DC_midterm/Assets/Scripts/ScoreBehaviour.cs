using UnityEngine;
using TMPro;

public class ScoreBehaviour : MonoBehaviour
{
    private int _score;

    public int Score  
    {
        get => _score;
        set
        {
            _score = value;
            _scoreUI.text =  Score.ToString(); 
        }
    }

    [SerializeField] private TMP_Text _scoreUI; //tell unity where we need to update the score VALUE
}

