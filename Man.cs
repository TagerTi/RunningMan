

namespace RunningMan;

public static class Man
{
    public static int X;
    public static int Y;

    public static int Health = 2;
    
    private static int baseY;
    private static int maxX;
    private static int minX;

    public static int Score;

    private static int jumping = 0;

    private static int frame = 0;

    private static string[] legFrames = [
        "| |", "| \\", "/ |"
    ];

    private static Dictionary<String, bool> keyboardKeys = new()
    {
        {"Up", false},
        {"Left", false},
        {"Right", false},
    };

    public static void Setup(int x, int baseY, int maxX, int minX)
    {
        Man.baseY = baseY;
        Man.maxX = maxX;
        Man.minX = minX;
        Man.X = x;
        Man.Y = baseY;

        Man.Score = 0;
    }

    public static void Move(int dirX, bool jump = false)
    {
        Man.X = Math.Min(Math.Max(Man.X + dirX, minX), maxX);
        
        if (Man.jumping == 0 && jump)
        {
            Man.jumping = 10;
        }
    }

    public static void LoopCycle()
    {
        var movingDirX = (keyboardKeys["Right"] ? 1 : 0) - (keyboardKeys["Left"] ? 1 : 0);
        Move(movingDirX, keyboardKeys["Up"]);

        if (Man.jumping > 0)
        {
            Man.Y--;
            if (Man.jumping == Man.baseY - Man.Y) Man.jumping = -Man.jumping;
        } 
        else if (Man.jumping < 0)
        {
            Man.Y++;
            if (Man.Y == baseY) Man.jumping = 0;
        }

        keyboardKeys["Right"] = false;
        keyboardKeys["Left"] = false;
        keyboardKeys["Up"] = false;
    }

    public static string[] GetBody()
    {
        string[] body = [
            " O ",
            "/|\\",
            legFrames[frame]
        ];
        frame = (frame + 1) % 3;

        return body;
    }
    
    public static void Damage()
    {
        Man.Health--;

        if (Man.Health <= 0)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("HAHAHAHAHAHAHAHAHAH");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("YOU DIED");
            Environment.Exit(0);
        }
    }

    public static void HandleInput()
    {
        var key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.D) keyboardKeys["Right"] = true;
        if (key.Key == ConsoleKey.A) keyboardKeys["Left"] = true;
        if (key.Key is ConsoleKey.W or ConsoleKey.Spacebar) keyboardKeys["Up"] = true;
    }
}