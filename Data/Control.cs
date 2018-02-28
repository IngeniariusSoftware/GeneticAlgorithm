using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class Control
    {

        public static Bug[] Bugs = new Bug[64];

        public static int[,] Field = new int[96, 96];

        public static int Food;

        public static int Poison;

        public static int Barrier;

        public static int CurrentNumberBugs;

        static Random rnd = new Random();

        public static void FillField()
        {
            for (int indexY = 0; indexY < Field.GetLength(1); indexY++)
            {
                for (int indexX = 0; indexX < Field.GetLength(0); indexX++)
                {
                    if (Field[indexY, indexX] == 0 & Barrier < 32 & rnd.Next(0, 100) == 0)
                    {
                        Field[indexY, indexX] = 4;
                        Barrier++;
                    }
                    if (Field[indexY, indexX] == 0 & Food < 128 & rnd.Next(0, 40) == 0)
                    {
                        Field[indexY, indexX] = 1;
                        Food++;
                    }
                    if (Field[indexY, indexX] == 0 & Poison < 128 & rnd.Next(0, 40) == 0)
                    {
                        Field[indexY, indexX] = 2;
                        Poison++;
                    }
                }
            }
        }


        public static void FieldUpdate()
        {
            for (int indexY = 0; indexY < Field.GetLength(1); indexY++)
            {
                for (int indexX = 0; indexX < Field.GetLength(0); indexX++)
                {
                    if (Field[indexY, indexX] == 0 & Food < 128 & rnd.Next(0, 40) == 0)
                    {
                        Field[indexY, indexX] = 1;
                        Food++;
                    }
                    if (Field[indexY, indexX] == 0 & Poison < 128 & rnd.Next(0, 40) == 0)
                    {
                        Field[indexY, indexX] = 2;
                        Poison++;
                    }
                }
            }
        }

        public static void CreateCreatures()
        {
            int kol = 0;
            Bug[] Childs = new Bug[64];
            for (int indexFirstParent = 0; indexFirstParent < Bugs.Length; indexFirstParent++)
            {
                if (Bugs[indexFirstParent].PublicLife > 0)
                {
                    int[] ChildGenom = new int [64];
                    for (int indexSecondParent = 0; indexSecondParent < Bugs.Length; indexSecondParent++)
                    {
                        if (Bugs[indexSecondParent].PublicLife > 0)
                        {
                            for (int indexGenom = 0; indexGenom < 64; indexGenom++)
                            {
                                if (Bugs[indexFirstParent].PublicGenom[indexGenom] ==
                                    Bugs[indexSecondParent].PublicGenom[indexGenom])
                                {
                                    ChildGenom[indexGenom] = Bugs[indexFirstParent].PublicGenom[indexGenom];
                                }
                                else
                                {
                                    if (rnd.Next(0, 2) == 0)
                                    {
                                        ChildGenom[indexGenom] = Bugs[indexFirstParent].PublicGenom[indexGenom];
                                    }
                                    else
                                    {
                                        ChildGenom[indexGenom] = Bugs[indexSecondParent].PublicGenom[indexGenom];
                                    }
                                }

                            }
                            for (int indexY = 0; indexY < Field.GetLength(1); indexY++)
                            {
                                for (int indexX = 0; indexX < Field.GetLength(0); indexX++)
                                {
                                    if (Field[indexY, indexX] == 0)
                                    {
                                        Childs[kol] = new Bug(50, indexX, indexY, ChildGenom, rnd.Next(0, 7));
                                        kol++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (int index = 0; index < 64; index++)
            {
                Field[Bugs[index].PublicY, Bugs[index].PublicX] = 0;
            }
            Bugs = Childs;
            CurrentNumberBugs = kol;
        }

        public static void CreateNewCreatures()
        {
            int kol = 0;
            for (int indexY = 0; indexY < Field.GetLength(1); indexY++)
            {
                for (int indexX = 0; indexX < Field.GetLength(0); indexX++)
                {
                    if (Field[indexY, indexX] == 0 & kol < 64)
                    {
                        int[] genom = new int[64];
                        for (int index = 0; index < genom.Length; index++)
                        {
                            genom[index] = rnd.Next(0, 64);
                        }
                        Bugs[kol] = new Bug(50, indexX, indexY, genom, rnd.Next(0, 7));
                        Field[indexY, indexX] = 3;
                        kol++;
                    }
                }

            }
            CurrentNumberBugs = kol;
        }

        public static void Run()
        {
            for (int index = 0; index < Bugs.Length; index++)
            {
                if (Bugs[index].PublicLife > 0)
                {
                    int steps = 0;
                    bool end = false;
                    Bugs[index]--;
                    for (int gen = 0; steps < 128 & !end; gen++)
                    {
                        int BugX = Bugs[index].PublicX;
                        int BugY = Bugs[index].PublicY;
                        steps++;
                        if (gen < 8)
                        {
                            Bugs[index].Move();
                            end = true;
                        }
                        if (gen > 7 & gen < 16)
                        {
                            Bugs[index].Turn(gen / 2);
                        }
                        if (gen > 15 & gen < 24)
                        {
                            switch (gen)
                            {
                                case 16:
                                {
                                    if (BugY - 1 > 0)
                                    {

                                        if (Bugs[index].Eat(Field[BugY - 1, BugX]))
                                        {
                                            Field[BugY - 1, BugX] = 0;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 17:
                                {
                                    if (BugY - 1 > 0 & BugX + 1 < 96)
                                    {
                                        if (Bugs[index].Check(Field[BugY - 1, BugX + 1]))
                                        {
                                            Field[BugY - 1, BugX + 1] = 0;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 18:
                                {
                                    if (BugX + 1 < 96)
                                    {
                                        if (Bugs[index].Check(Field[BugY, BugX + 1]))
                                        {
                                            Field[BugY, BugX + 1] = 0;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 19:
                                {
                                    if (BugY + 1 < 96 & BugX + 1 < 96)
                                    {
                                        if (Bugs[index].Check(Field[BugY + 1, BugX + 1]))
                                        {
                                            Field[BugY + 1, BugX + 1] = 0;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 20:
                                {
                                    if (BugY + 1 < 96)
                                    {
                                        if (Bugs[index].Check(Field[BugY + 1, BugX]))
                                        {
                                            Field[BugY + 1, BugX] = 0;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 21:
                                {
                                    if (BugY + 1 < 96 & BugX - 1 > 0)
                                    {
                                        if (Bugs[index].Check(Field[BugY + 1, BugX - 1]))
                                        {
                                            Field[BugY + 1, BugX - 1] = 0;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 22:
                                {
                                    if (BugX - 1 > 0)
                                    {
                                        if (Bugs[index].Check(Field[BugY, BugX - 1]))
                                        {
                                            Field[BugY, BugX - 1] = 0;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 23:
                                {
                                    if (BugY - 1 > 0 & BugX - 1 > 0)
                                    {
                                        if (Bugs[index].Check(Field[BugY - 1, BugX - 1]))
                                        {
                                            Field[BugY - 1, BugX - 1] = 0;
                                            end = true;
                                        }
                                    }
                                    break;
                                }

                            }
                        }
                        if (gen > 23 & gen < 32)
                        {
                            switch (gen)
                            {
                                case 24:
                                {
                                    if (BugY - 1 > 0)
                                    {
                                        if (Bugs[index].Eat(Field[BugY - 1, BugX]))
                                        {
                                            Field[BugY - 1, BugX] = 1;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 25:
                                {
                                    if (BugY - 1 > 0 & BugX + 1 < 96)
                                    {
                                        if (Bugs[index].Eat(Field[BugY - 1, BugX + 1]))
                                        {
                                            Field[BugY - 1, BugX + 1] = 1;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 26:
                                {
                                    if (BugX + 1 < 96)
                                    {
                                        if (Bugs[index].Eat(Field[BugY, BugX + 1]))
                                        {
                                            Field[BugY, BugX + 1] = 1;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 27:
                                {
                                    if (BugY + 1 < 96 & BugX + 1 < 96)
                                    {
                                        if (Bugs[index].Eat(Field[BugY + 1, BugX + 1]))
                                        {
                                            Field[BugY + 1, BugX + 1] = 1;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 28:
                                {
                                    if (BugY + 1 < 96)
                                    {
                                        if (Bugs[index].Eat(Field[BugY + 1, BugX]))
                                        {
                                            Field[BugY + 1, BugX] = 1;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 29:
                                {
                                    if (BugY + 1 < 96 & BugX - 1 > 0)
                                    {
                                        if (Bugs[index].Eat(Field[BugY + 1, BugX - 1]))
                                        {
                                            Field[BugY + 1, BugX - 1] = 1;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 30:
                                {
                                    if (BugX - 1 > 0)
                                    {
                                        if (Bugs[index].Eat(Field[BugY, BugX - 1]))
                                        {
                                            Field[BugY, BugX - 1] = 1;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 31:
                                {
                                    if (BugY - 1 > 0 & BugX - 1 > 0)
                                    {
                                        if (Bugs[index].Eat(Field[BugY - 1, BugX - 1]))
                                        {
                                            Field[BugY - 1, BugX - 1] = 1;
                                            end = true;
                                        }
                                    }
                                    break;
                                }

                            }
                        }
                        if (gen > 31 & gen < 64)
                        {
                            gen = gen * 2;
                            if (gen > 63)
                            {
                                gen = gen - 64;
                            }
                        }
                    }

                }
            }
            CurrentNumberBugs
                = 0;
            for (int index = 0; index < Bugs.Length; index++)
            {
                if (Bugs[index].PublicLife > 0)
                {
                    CurrentNumberBugs++;
                }
            }
        }
    }
}
