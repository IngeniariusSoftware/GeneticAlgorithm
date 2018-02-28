using System;

namespace Data
{
    class Control
    {
        public static int Generation = 1;

        public static int SizeMapX = 96;

        public static int SizeMapY = 64;

        public static Bug[] Bugs = new Bug[64];

        public static int[,] Field = new int[SizeMapY, SizeMapX];

        public static int Food;

        public static int Poison;

        public static int Barrier;

        public static int CurrentNumberBugs;

        static Random rnd = new Random();

        public static void FillField()
        {
            for (int indexY = 0; indexY < Field.GetLength(0); indexY++)
            {
                for (int indexX = 0; indexX < Field.GetLength(1); indexX++)
                {
                    if (Field[indexY, indexX] == 0 & Barrier < 32 & rnd.Next(0, 150) == 0)
                    {
                        Field[indexY, indexX] = 4;
                        Barrier++;
                    }
                    if (Field[indexY, indexX] == 0 & Food < 128 & rnd.Next(0, 150) == 0)
                    {
                        Field[indexY, indexX] = 1;
                        Food++;
                    }
                    if (Field[indexY, indexX] == 0 & Poison < 128 & rnd.Next(0, 150) == 0)
                    {
                        Field[indexY, indexX] = 2;
                        Poison++;
                    }
                }
            }
        }


        public static void FieldUpdate()
        {
            for (int indexY = 0; indexY < Field.GetLength(0); indexY++)
            {
                for (int indexX = 0; indexX < Field.GetLength(1); indexX++)
                {
                    if (Field[indexY, indexX] == 0 & Food < 128 & rnd.Next(0, 200) == 0)
                    {
                        Field[indexY, indexX] = 1;
                        Food++;
                    }
                    if (Field[indexY, indexX] == 0 & Poison < 128 & rnd.Next(0, 200) == 0)
                    {
                        Field[indexY, indexX] = 2;
                        Poison++;
                    }
                }
            }
        }

        public static void CreateCreatures()
        {
            Bug[] Childs = new Bug[64];
            int kol = 0;
            if (CurrentNumberBugs < 8)
            {
                for (int indexY = 0; indexY < SizeMapY; indexY++)
                {
                    for (int indexX = 0; indexX < SizeMapX; indexX++)
                    {
                        if (rnd.Next(0, 50) == 0 & Field[indexY, indexX] == 0 & kol < 64)
                        {
                            int[] ChildGenom = new int[64];
                            for (int index = 0; index < ChildGenom.Length; index++)
                            {
                                ChildGenom[index] = rnd.Next(0, 64);
                            }
                            Childs[kol] = new Bug(50, indexX, indexY, ChildGenom, rnd.Next(0, 8));
                            Field[indexY, indexX] = 3;
                            kol++;
                        }
                    }
                }
            }
            else
            {
                for (int indexFirstParent = 0; indexFirstParent < 64; indexFirstParent++)
                {
                    if (Bugs[indexFirstParent].PublicLife > 0)
                    {
                        int[] ChildGenom = new int [64];
                        for (int indexSecondParent = 0; indexSecondParent < 64; indexSecondParent++)
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
                                bool create = false;
                                for (int indexY = 0; indexY < SizeMapY; indexY++)
                                {
                                    for (int indexX = 0; indexX < SizeMapX; indexX++)
                                    {
                                        if (rnd.Next(0, 50) == 0 & Field[indexY, indexX] == 0 & !create & kol < 64)
                                        {
                                            Childs[kol] = new Bug(50, indexX, indexY, ChildGenom, rnd.Next(0, 8));
                                            Field[indexY, indexX] = 3;
                                            kol++;
                                            create = true;
                                        }
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
            Generation++;
        }

        public static void CreateNewCreatures()
        {
            int kol = 0;
            for (int indexY = 0; indexY < Field.GetLength(0); indexY++)
            {
                for (int indexX = 0; indexX < Field.GetLength(1); indexX++)
                {
                    if (Field[indexY, indexX] == 0 & kol < 64 & (rnd.Next(0, 50) == 0))
                    {
                        int[] genom = new int[64];
                        for (int index = 0; index < genom.Length; index++)
                        {
                            genom[index] = rnd.Next(0, 64);
                        }
                        Bugs[kol] = new Bug(50, indexX, indexY, genom, rnd.Next(0, 8));
                        Field[indexY, indexX] = 3;
                        kol++;
                    }
                }

            }
            CurrentNumberBugs = kol;
        }

        public static void Run()
        {
            for (int index = 0; index < 64; index++)
            {
                if (Bugs[index].PublicLife > 0)
                {
                    int steps = 0;
                    bool end = false;
                    Bugs[index]--;
                    for (int gen = 0; steps < 100 & !end; gen++)
                    {
                        if (gen > 63)
                        {
                            gen = gen - 64;
                        }
                        int BugX = Bugs[index].PublicX;
                        int BugY = Bugs[index].PublicY;
                        steps++;
                        if (Bugs[index].PublicGenom[gen] < 8)
                        {
                            switch (Bugs[index].Direction)
                            {
                                case 0:
                                {
                                    if (BugY > 0)
                                    {
                                        if (Field[BugY - 1, BugX] == 0)
                                        {
                                            Field[BugY, BugX] = 0;
                                            Bugs[index].Move();
                                            Field[Bugs[index].PublicY, Bugs[index].PublicX] = 3;
                                        }
                                    }
                                    break;
                                }
                                case 1:
                                {
                                    if (BugY > 0 & BugX + 1 < SizeMapX)
                                    {
                                        if (Field[BugY - 1, BugX + 1] == 0)
                                        {
                                            Field[BugY, BugX] = 0;
                                            Bugs[index].Move();
                                            Field[Bugs[index].PublicY, Bugs[index].PublicX] = 3;
                                        }

                                    }
                                    break;
                                }
                                case 2:
                                {
                                    if (BugX + 1 < SizeMapX)
                                    {
                                        if (Field[BugY, BugX + 1] == 0)
                                        {
                                            Field[BugY, BugX] = 0;
                                            Bugs[index].Move();
                                            Field[Bugs[index].PublicY, Bugs[index].PublicX] = 3;
                                        }

                                    }
                                    break;
                                }
                                case 3:
                                {
                                    if (BugX + 1 < SizeMapX & BugY + 1 < SizeMapY)
                                    {
                                        if (Field[BugY + 1, BugX + 1] == 0)
                                        {
                                            Field[BugY, BugX] = 0;
                                            Bugs[index].Move();
                                            Field[Bugs[index].PublicY, Bugs[index].PublicX] = 3;
                                        }

                                    }
                                    break;
                                }
                                case 4:
                                {
                                    if (BugY + 1 < SizeMapY)
                                    {
                                        if (Field[BugY + 1, BugX] == 0)
                                        {
                                            Field[BugY, BugX] = 0;
                                            Bugs[index].Move();
                                            Field[Bugs[index].PublicY, Bugs[index].PublicX] = 3;
                                        }

                                    }
                                    break;
                                }
                                case 5:
                                {
                                    if (BugY + 1 < SizeMapY & BugX > 0)
                                    {
                                        if (Field[BugY + 1, BugX - 1] == 0)
                                        {
                                            Field[BugY, BugX] = 0;
                                            Bugs[index].Move();
                                            Field[Bugs[index].PublicY, Bugs[index].PublicX] = 3;
                                        }

                                    }
                                    break;
                                }
                                case 6:
                                {
                                    if (BugX > 0)
                                    {
                                        if (Field[BugY, BugX - 1] == 0)
                                        {
                                            Field[BugY, BugX] = 0;
                                            Bugs[index].Move();
                                            Field[Bugs[index].PublicY, Bugs[index].PublicX] = 3;
                                        }

                                    }
                                    break;
                                }
                                case 7:
                                {
                                    if (BugX > 0 & BugY > 0)
                                    {
                                        if (Field[BugY - 1, BugX - 1] == 0)
                                        {
                                            Field[BugY, BugX] = 0;
                                            Bugs[index].Move();
                                            Field[Bugs[index].PublicY, Bugs[index].PublicX] = 3;
                                        }

                                    }
                                    break;
                                }
                            }
                            end = true;
                        }
                        if (Bugs[index].PublicGenom[gen] > 7 & Bugs[index].PublicGenom[gen] < 16)
                        {
                            Bugs[index].Turn(Bugs[index].PublicGenom[gen] - 8);
                        }
                        if (Bugs[index].PublicGenom[gen] > 15 & Bugs[index].PublicGenom[gen] < 24)
                        {
                            switch (Bugs[index].PublicGenom[gen])
                            {
                                case 16:
                                {
                                    if (BugY > 0)
                                    {

                                        if (Bugs[index].Check(Field[BugY - 1, BugX]))
                                        {
                                            Field[BugY - 1, BugX] = 1;
                                            end = true;
                                            Poison--;
                                        }
                                    }
                                    break;
                                }
                                case 17:
                                {
                                    if (BugY > 0 & BugX + 1 < 96)
                                    {
                                        if (Bugs[index].Check(Field[BugY - 1, BugX + 1]))
                                        {
                                            Field[BugY - 1, BugX + 1] = 1;
                                            end = true;
                                            Poison--;
                                        }
                                    }
                                    break;
                                }
                                case 18:
                                {
                                    if (BugX + 1 < SizeMapX)
                                    {
                                        if (Bugs[index].Check(Field[BugY, BugX + 1]))
                                        {
                                            Field[BugY, BugX + 1] = 1;
                                            end = true;
                                            Poison--;
                                        }
                                    }
                                    break;
                                }
                                case 19:
                                {
                                    if (BugY + 1 < SizeMapY & BugX + 1 < SizeMapX)
                                    {
                                        if (Bugs[index].Check(Field[BugY + 1, BugX + 1]))
                                        {
                                            Field[BugY + 1, BugX + 1] = 1;
                                            end = true;
                                            Poison--;
                                        }
                                    }
                                    break;
                                }
                                case 20:
                                {
                                    if (BugY + 1 < SizeMapY)
                                    {
                                        if (Bugs[index].Check(Field[BugY + 1, BugX]))
                                        {
                                            Field[BugY + 1, BugX] = 1;
                                            end = true;
                                            Poison--;
                                        }
                                    }
                                    break;
                                }
                                case 21:
                                {
                                    if (BugY + 1 < SizeMapY & BugX > 0)
                                    {
                                        if (Bugs[index].Check(Field[BugY + 1, BugX - 1]))
                                        {
                                            Field[BugY + 1, BugX - 1] = 1;
                                            end = true;
                                            Poison--;
                                        }
                                    }
                                    break;
                                }
                                case 22:
                                {
                                    if (BugX > 0)
                                    {
                                        if (Bugs[index].Check(Field[BugY, BugX - 1]))
                                        {
                                            Field[BugY, BugX - 1] = 1;
                                            end = true;
                                            Poison--;
                                        }
                                    }
                                    break;
                                }
                                case 23:
                                {
                                    if (BugY > 0 & BugX > 0)
                                    {
                                        if (Bugs[index].Check(Field[BugY - 1, BugX - 1]))
                                        {
                                            Field[BugY - 1, BugX - 1] = 1;
                                            end = true;
                                            Poison--;
                                        }
                                    }
                                    break;
                                }

                            }
                        }
                        if (Bugs[index].PublicGenom[gen] > 23 & Bugs[index].PublicGenom[gen] < 32)
                        {
                            switch (Bugs[index].PublicGenom[gen])
                            {
                                case 24:
                                {
                                    if (BugY > 0)
                                    {
                                        if (Bugs[index].Eat(Field[BugY - 1, BugX]))
                                        {
                                            if (Field[BugY - 1, BugX] == 1)
                                            {
                                                Food--;
                                            }
                                            else
                                            {
                                                Poison--;
                                            }
                                            end = true;
                                            Field[BugY - 1, BugX] = 0;

                                        }
                                    }
                                    break;
                                }
                                case 25:
                                {
                                    if (BugY > 0 & BugX + 1 < SizeMapX)
                                    {
                                        if (Bugs[index].Eat(Field[BugY - 1, BugX + 1]))
                                        {
                                            if (Field[BugY - 1, BugX + 1] == 1)
                                            {
                                                Food--;
                                            }
                                            else
                                            {
                                                Poison--;
                                            }
                                            end = true;
                                            Field[BugY - 1, BugX + 1] = 0;
                                        }
                                    }
                                    break;
                                }
                                case 26:
                                {
                                    if (BugX + 1 < SizeMapX)
                                    {
                                        if (Bugs[index].Eat(Field[BugY, BugX + 1]))
                                        {
                                            if (Field[BugY, BugX + 1] == 1)
                                            {
                                                Food--;
                                            }
                                            else
                                            {
                                                Poison--;
                                            }
                                            end = true;
                                            Field[BugY, BugX + 1] = 0;
                                        }
                                    }
                                    break;
                                }
                                case 27:
                                {
                                    if (BugY + 1 < SizeMapY & BugX + 1 < SizeMapX)
                                    {
                                        if (Bugs[index].Eat(Field[BugY + 1, BugX + 1]))
                                        {
                                            if (Field[BugY + 1, BugX + 1] == 1)
                                            {
                                                Food--;
                                            }
                                            else
                                            {
                                                Poison--;
                                            }
                                            Field[BugY + 1, BugX + 1] = 0;
                                            end = true;
                                        }
                                    }
                                    break;
                                }
                                case 28:
                                {
                                    if (BugY + 1 < SizeMapY)
                                    {
                                        if (Bugs[index].Eat(Field[BugY + 1, BugX]))
                                        {
                                            if (Field[BugY + 1, BugX] == 1)
                                            {
                                                Food--;
                                            }
                                            else
                                            {
                                                Poison--;
                                            }
                                            end = true;
                                            Field[BugY + 1, BugX] = 0;

                                        }
                                    }
                                    break;
                                }
                                case 29:
                                {
                                    if (BugY + 1 < SizeMapY & BugX > 0)
                                    {
                                        if (Bugs[index].Eat(Field[BugY + 1, BugX - 1]))
                                        {
                                            if (Field[BugY + 1, BugX - 1] == 1)
                                            {
                                                Food--;
                                            }
                                            else
                                            {
                                                Poison--;
                                            }
                                            end = true;
                                            Field[BugY + 1, BugX - 1] = 0;

                                        }
                                    }
                                    break;
                                }
                                case 30:
                                {
                                    if (BugX > 0)
                                    {
                                        if (Bugs[index].Eat(Field[BugY, BugX - 1]))
                                        {
                                            if (Field[BugY, BugX - 1] == 1)
                                            {
                                                Food--;
                                            }
                                            else
                                            {
                                                Poison--;
                                            }
                                            end = true;
                                            Field[BugY, BugX - 1] = 0;


                                        }
                                    }
                                    break;
                                }
                                case 31:
                                {
                                    if (BugY > 0 & BugX > 0)
                                    {
                                        if (Bugs[index].Eat(Field[BugY - 1, BugX - 1]))
                                        {
                                            if (Field[BugY - 1, BugX - 1] == 1)
                                            {
                                                Food--;
                                            }
                                            else
                                            {
                                                Poison--;
                                            }
                                            end = true;
                                            Field[BugY - 1, BugX - 1] = 0;


                                        }
                                    }
                                    break;
                                }

                            }
                        }
                        if (Bugs[index].PublicGenom[gen] > 31 & Bugs[index].PublicGenom[gen] < 64)
                        {
                            gen = gen * 2;
                            if (gen > 63)
                            {
                                gen = gen - 64;
                            }
                        }
                    }
                    if (steps > 99)
                    {
                        Bugs[index].PublicLife = 0;
                    }

                }
            }
            CurrentNumberBugs
                = 0;
            for (int index = 0; index < 64; index++)
            {
                if (Bugs[index].PublicLife > 0)
                {
                    CurrentNumberBugs++;
                }
            }
        }
    }
}
