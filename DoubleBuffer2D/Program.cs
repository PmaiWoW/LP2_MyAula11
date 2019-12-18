using System;

namespace DoubleBuffer2D
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            DoubleBuffer2D<char> db =
                new DoubleBuffer2D<char>(5, 5);

            db[1, 0] = 'x';
            db[2, 0] = 'x';
            db[3, 0] = 'x';
            db[4, 0] = 'x';
            db[1, 1] = 'x';
            db[1, 2] = 'x';
            db[2, 2] = 'x';
            db[3, 2] = 'x';
            db[3, 3] = 'x';
            db[0, 4] = 'x';
            db[1, 4] = 'x';
            db[2, 4] = 'x';
            db[3, 4] = 'x';
            
            db.Swap();
            db.Render();
        }
    }
}
