using UnityEngine;

public static class Stats 
{
    public static int Level { get; private set; } = 1;

    private static int _score = 0; 

    public static int Score
    {
        get { return _score; }
        set
        {
            _score = value; 
            if(_score > 30 * Level)
            {
                Level++;
            }
        }
    }

    public static void ResetAllStats()
    {
        if (!PlayerPrefs.HasKey("Score"))
            PlayerPrefs.SetInt("Score", 0);
        else if (PlayerPrefs.GetInt("Score") < _score)
            PlayerPrefs.SetInt("Score", _score); 
        Level = 1;
        _score = 0; 

    }
}
