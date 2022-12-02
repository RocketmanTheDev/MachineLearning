using System;

namespace toto
{
    class Program
    {
        class Toto
        {
            private int[] array;
            int cnt, from; private int sh = 10000;

            public Toto(int cnt, int from){this.cnt = cnt; this.from = from;}

            private void FillNumbers()
            {
                array = new int[from];
                for (int i = 0; i < from; i++) { array[i] = i; }
            }

            private void Shuffle()
            {
                Random r = new Random();
                for(int i=0; i < sh;i++)
                {
                    int x = r.Next(); x = x % from;
                    int y = r.Next(); y = y % from;
                    int temp = array[x]; array[x] = array[y]; array[y] = temp;
                }
            }

            public void Draw()
            {
                FillNumbers(); Shuffle();
                for (int i = 0; i < cnt; i++) { Console.WriteLine(array[i]); }
            }
        }
        static void Main(string[] args)
        {
            Toto toto = new Toto(6, 49); toto.Draw();
        }
    }
}
