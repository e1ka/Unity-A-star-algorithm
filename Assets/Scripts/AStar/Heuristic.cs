using System;

public static class Heuristic
{
    public static int Manhattan((int x, int y) current, (int x, int y) target)
    {
        return Math.Abs(current.x - target.x) + Math.Abs(current.y - target.y);
    }
}
