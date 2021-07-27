[System.Serializable]
public class GameDataVO
{
    public GameDataVO(int topScore = 0, int topSpeed = 0)
    {
        this.highScore = topScore;
        this.highSpeed = topSpeed;
    }

    public int highScore = 0;
    public int highSpeed = 0;
}
