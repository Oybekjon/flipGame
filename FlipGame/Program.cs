using System.IO;

namespace FlipGame
{
    internal class Program
    {
        private static int _ans = int.MaxValue;
        private static int[,] _array = new int[4, 4];

        private static bool Judge()
        {
            var first = _array[0, 0];
            for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                if (_array[i, j] != first)
                    return false;
            return true;
        }

        private static void Change(int x, int y)
        {
            _array[x, y] ^= 1;
            if (x - 1 >= 0) _array[x - 1, y] ^= 1;
            if (y - 1 >= 0) _array[x, y - 1] ^= 1;
            if (x + 1 < 4) _array[x + 1, y] ^= 1;
            if (y + 1 < 4) _array[x, y + 1] ^= 1;
        }

        private static void Smth(int x, int y, int times)
        {
            if (Judge())
            {
                if (_ans > times) _ans = times;
                return;
            }

            if (x >= 4 || y >= 4) return;
            int aX, aY;
            aX = (x + 1) % 4;
            aY = y + (x + 1) / 4;
            Smth(aX, aY, times);
            Change(x, y);
            Smth(aX, aY, times + 1);
            Change(x, y);
        }

        public static void Main(string[] args)
        {
            var input = File.ReadAllText(@"D:\techcells\input.txt");
            using StreamWriter output = new(@"D:\techcells\output.txt");
            for (var i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (input[j] == 'b') _array[i, j] = 1;
                    else _array[i, j] = 0;
                }
            }
            Smth(0, 0, 0);
            if (_ans == int.MaxValue) output.WriteLine("Impossible");
            else output.WriteLine(_ans);
        }
    }
}