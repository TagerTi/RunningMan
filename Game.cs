using Timer = System.Timers.Timer;

namespace RunningMan;

public static class Game
{
    private const int MIN_X = 5;
    private const int MAX_X = Draw.SCREEN_SIZE_X - 5;
    private const int MIN_Y = 3;
    private const int MAX_Y = Draw.SCREEN_SIZE_Y - 5;
    private static List<Obstacle> obstacles = [];
    private static Timer timer = new (200);
    private static Timer hardnessTimer = new(2000);

    private static float max_obstacles = 1.8f;


    public static void Setup()
    {
        Console.CursorVisible = false;
        
        SpawnNextObstacle();
        
        timer.Start();
        timer.Elapsed += OnTimerTimeout;
        
        hardnessTimer.Start();
        hardnessTimer.Elapsed += GetHarderTimeout;

        Man.Setup(MIN_X, MAX_Y, MAX_X, MIN_X);
    }

    public static void GetHarderTimeout(object? sender, System.Timers.ElapsedEventArgs e)
    {
        timer.Interval *= 0.95f;
        max_obstacles += 0.1f;
    }

    public static void OnTimerTimeout(object? sender, System.Timers.ElapsedEventArgs e)
    {
        for (int i = 0; i < obstacles.Count; i++)  
        {
            obstacles[i].Move(MIN_X, MAX_X, MIN_Y, MAX_Y);

            if (obstacles[i].X >= Man.X && obstacles[i].Y + 1 > Man.Y && 
                obstacles[i].X < Man.X + 3 && obstacles[i].Y < Man.Y + 3)
            {
                obstacles[i].die = true;
                Man.Damage();
            }
            
            if (obstacles[i].X > Draw.SCREEN_SIZE_X || obstacles[i].X < 0 || 
                obstacles[i].Y > Draw.SCREEN_SIZE_Y || obstacles[i].Y < 0)
            {
                obstacles[i].die = true;
            }
        }
        
        Man.LoopCycle();
        Draw.DrawToConsole(obstacles);

        var dyingObstacles = obstacles.Where(obstacle => obstacle.die).ToArray();

        foreach (var obstacle in dyingObstacles)
        {
            obstacles.Remove(obstacle);
            Man.Score++;
        }

        SpawnNextObstacle();
    }

    private static void SpawnNextObstacle()
    {
        if (obstacles.Count < Math.Floor(max_obstacles))
        {
            Random dice = new();
            if (dice.Next(0,2) > 0)
                obstacles.Add(new Obstacle(MAX_X, MAX_Y));
            else
                obstacles.Add(new Obstacle(dice.Next(MIN_X,MAX_X), MIN_Y, dice.Next(-1,2), 1));
        }
    }

    public static void Loop()
    {
        while (true)
        {
            if (Console.KeyAvailable)
            {
                Man.HandleInput();
            }
            Thread.Sleep(10);
        }
    }
}