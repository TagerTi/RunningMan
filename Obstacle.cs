namespace RunningMan;

public class Obstacle
{
    public int X;
    public int Y;
    public int movingDirX;
    public int movingDirY;
    
    
    public bool die = false;
    
    public Obstacle(int x, int height, int movingDirX = -1, int movingDirY = 0)
    {
        this.X = x;
        this.Y = height;
        this.movingDirX = movingDirX;
        this.movingDirY = movingDirY;
    }

    public void Move()
    {
        this.X += movingDirX;
        this.Y += movingDirY;
    }

    public static string[] GetBody()
    {
        return ["#", "#"];
    }
}