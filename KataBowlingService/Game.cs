namespace KataBowlingService;
public class Game
{
    private List<int> _Rolls = new List<int>();

    /// <summary>
    /// called each time the player rolls a ball.  
    /// The argument is the number of pins knocked down.
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public int Roll(int pinsKnockedDown)
    {
        if(pinsKnockedDown > 10)
        {
            throw new Exception();
        }

        this._Rolls.Add(pinsKnockedDown);    

        return pinsKnockedDown;
    }

    /// <summary>
    /// called only at the very end of the game.  
    /// It returns the total score for that game
    /// </summary>
    /// <returns></returns>
    public int Score()
    {
        int score = 0;
        int rolls = 0;
        bool anySpareOrStrike = false;

        for(int frame = 0; frame < 10; frame++)
        {
            if(this.IsSpare(rolls))
            {
                score += 10 + this._Rolls[rolls + 2];
                rolls += 2;
                anySpareOrStrike = true;
            }
            else if(this.IsStrike(rolls))
            {
                score += 10 + this._Rolls[rolls + 1] + this._Rolls[rolls + 1];
                rolls++;
                anySpareOrStrike = true;
            }
            else
            {
                score += this._Rolls[rolls] + this._Rolls[rolls + 1];
                rolls += 2;
            }
        }

        if (anySpareOrStrike)
        {
            score += this._Rolls.LastOrDefault();
        }

        return score;
    }

    private bool IsSpare(int roll)
    {
        return this._Rolls[roll] + this._Rolls[roll + 1] == 10;
    }

    private bool IsStrike(int roll)
    {
        return this._Rolls[roll] == 10;
    }
}
