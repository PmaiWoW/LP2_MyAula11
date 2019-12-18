using System;
using System.Threading;
using System.Collections.Concurrent;

namespace GameLoop
{
    public class Program
    {
        private enum Dir { Up, Left, Down, Right, None };
        private BlockingCollection<ConsoleKey> input;
        private Thread inputThread;
        private int x, y, xOld, yOld;
        private readonly int maxX, maxY;
        private Dir? dir;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.GameLoop();
        }

        private void ReadInput()
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey().Key;
                input.Add(key);
            } while (key != ConsoleKey.Escape);
        }

        private Program()
        {
            input = new BlockingCollection<ConsoleKey>();
            inputThread = new Thread(ReadInput);
            maxX = 10;
            maxY = 10;
            x = 5;
            y = 5;
            dir = Dir.None;
            inputThread.Start();
        }

        private void GameLoop()
        {
            while (true)
            {
                long start = DateTime.Now.Ticks;
                ProcessInput();
                Update();
                Render();
                Thread.Sleep((int)(start / 10000) + 50 - 
                    (int)(DateTime.Now.Ticks));
            }
        }

        private void ProcessInput()
        {
            ConsoleKey key;
            if(input.TryTake(out key))
            {
                switch (key)
                {
                    case ConsoleKey.W:
                        dir = Dir.Up;
                        break;
                    case ConsoleKey.A:
                        dir = Dir.Left;
                        break;
                    case ConsoleKey.S:
                        dir = Dir.Down;
                        break;
                    case ConsoleKey.D:
                        dir = Dir.Right;
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void Update()
        {
            if(dir != Dir.None)
            {
                switch (dir)
                {
                    case Dir.Up:
                        y = Math.Max(0, y - 1);
                        break;
                    case Dir.Left:
                        x = Math.Max(0, x - 1);
                        break;
                    case Dir.Down:
                        y = Math.Min(maxY, y + 1);
                        break;
                    case Dir.Right:
                        x = Math.Max(maxX, x + 1);
                        break;
                }

                dir = Dir.None;
            }
        }
        
        private void Render()
        {
            Console.SetCursorPosition(x, y);
            Console.Write("X ");
            foreach (ConsoleKey k in input) Console.Write(k);
        }
    }
}
