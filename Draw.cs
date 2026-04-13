namespace RunningMan;

public static class Draw
{
    public const int SCREEN_SIZE_X = 110;
    public const int SCREEN_SIZE_Y = 24;
    
    public static void DrawToConsole(List<Obstacle> obstacles)
    {
        Console.Clear();

        DrawScreenBorders();
        DrawObstacles(obstacles);
        DrawMan();
    }

    private static void DrawScreenBorders()
    {
        for (int i = 0; i < SCREEN_SIZE_Y; i++)
        {
            Console.SetCursorPosition(0, i);
            
            if (i == 0)
            {
                Console.Write(new string('=', SCREEN_SIZE_X));
                Console.Write($" Health: {Man.Health}");
            }
            else if (i == SCREEN_SIZE_Y - 1)
            {
                Console.Write(new string('=', SCREEN_SIZE_X));
                Console.Write($" Score: {Man.Score}");
            }
            else
            {
                Console.Write("||" + new string(' ', SCREEN_SIZE_X - 4) + "||");
            }
        }
    }

    private static void DrawObstacles(List<Obstacle> obstacles)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            string[] obstacleBody = obstacles[i].GetBody();
            for (int j = 0; j < obstacleBody.Length; j++)
            {
                if (obstacles[i].X < 0 || obstacles[i].Y < 0)
                {
                    //Console.Write("Error! Obstacles position is to low to draw!");
                    continue;
                }
                Console.SetCursorPosition(obstacles[i].X, obstacles[i].Y + j);
                Console.Write(obstacleBody[j]);
            }
        }
    }

    private static void DrawMan()
    {
        string[] body = Man.GetBody();
        for (int i = 0; i < body.Length; i++)
        {
            Console.SetCursorPosition(Man.X, Man.Y + i);
            Console.Write(body[i]);
        }
    }
}