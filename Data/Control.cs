using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Data
{
    class Control
    {
        public static int MaxNumberFood = 256;

        public static int MaxNumberPoison = 256;

        public static int MaxNumberBugs = 64;

        public static int MaxNumberAnts = 0;

        public static int MaxNumberSpiderEggs = 8;

        public static int MaxNumberSpiderWeb = 16;

        public static int MaxNumberSpiders = 36;

        public static int MaxNumberRocks = 128;

        public static int SizeMapX = 80;

        public static int SizeMapY = 43;

        public static Bug[] Bugs = new Bug[MaxNumberBugs];

        public static Ant[] Ants = new Ant[MaxNumberAnts];

        public static Spider[] Spiders = new Spider[MaxNumberSpiders];

        public static Cell[,] Field = new Cell[SizeMapY, SizeMapX];

        public static int CurrentNumberFood;

        public static int CurrentNumberPoison;

        public static int CurrentNumberRock;

        public static int CurrentNumberBugs;

        public static int CurrentNumberSpiders;

        public static int CurrentNumberAnts;

        public static Random Rnd = new Random();

        public static void Check()
        {



        }

        public static void FillField()
        {
            for (int indexY = 0; indexY < SizeMapY; indexY++)
            {
                for (int indexX = 0; indexX < SizeMapX; indexX++)
                {
                    if (indexX == 0 | indexY == 0 | indexX == SizeMapX - 1 | indexY == SizeMapY - 1)
                    {
                        Field[indexY, indexX] = new Cell(3);
                    }
                    else
                    {
                        Field[indexY, indexX] = new Cell();
                    }
                }
            }
            int cellRndX = 0;
            int cellRndY = 0;
            while (CurrentNumberRock < MaxNumberRocks)
            {
                while (Field[cellRndY, cellRndX].PublicContent != 0)
                {
                    cellRndY = Rnd.Next(0, SizeMapY);
                    cellRndX = Rnd.Next(0, SizeMapX);
                }
                Field[cellRndY, cellRndX].PublicContent = 4;
                CurrentNumberRock++;
            }
            while (CurrentNumberFood < MaxNumberFood)
            {
                while (Field[cellRndY, cellRndX].PublicContent != 0)
                {
                    cellRndY = Rnd.Next(0, SizeMapY);
                    cellRndX = Rnd.Next(0, SizeMapX);
                }
                Field[cellRndY, cellRndX].PublicContent = 1;
                CurrentNumberFood++;
            }
            while (CurrentNumberPoison < MaxNumberPoison)
            {
                while (Field[cellRndY, cellRndX].PublicContent != 0)
                {
                    cellRndY = Rnd.Next(0, SizeMapY);
                    cellRndX = Rnd.Next(0, SizeMapX);
                }
                Field[cellRndY, cellRndX].PublicContent = 2;
                CurrentNumberPoison++;
            }
        }

        public static void FieldUpdate()
        {
            int cellRndX = 0;
            int cellRndY = 0;
            while (CurrentNumberFood < MaxNumberFood)
            {
                while (Field[cellRndY, cellRndX].PublicContent != 0)
                {
                    cellRndY = Rnd.Next(0, SizeMapY);
                    cellRndX = Rnd.Next(0, SizeMapX);
                }
                Field[cellRndY, cellRndX].PublicContent = 1;
                CurrentNumberFood++;
            }
            while (CurrentNumberPoison < MaxNumberPoison)
            {
                while (Field[cellRndY, cellRndX].PublicContent != 0)
                {
                    cellRndY = Rnd.Next(0, SizeMapY);
                    cellRndX = Rnd.Next(0, SizeMapX);
                }
                Field[cellRndY, cellRndX].PublicContent = 2;
                CurrentNumberPoison++;
            }
        }

        public static void SortBugs()
        {
            bool change = true;
            for (int indexMain = 0; indexMain < Bugs.Length & change; indexMain++)
            {
                change = false;
                for (int indexAdditional = 0; indexAdditional < Bugs.Length - 1 - indexMain; indexAdditional++)
                {
                    if (Bugs[indexAdditional].PublicLifeTime > Bugs[indexAdditional + 1].PublicLifeTime)
                    {
                        Bug shelfBug = Bugs[indexAdditional];
                        Bugs[indexAdditional] = Bugs[indexAdditional + 1];
                        Bugs[indexAdditional + 1] = shelfBug;
                        change = true;
                    }
                }
            }
            //Проверка
            //string listLife = "";
            //foreach (var bug in Bugs)
            //{
            //    listLife = listLife + bug.PublicLifeTime.ToString() + " ";
            //}
            //MessageBox.Show(listLife);
            //Конец проверки
        }

        public static void CreateChildsBugs()
        {
            Bug[] childs = new Bug[MaxNumberBugs];
            CurrentNumberBugs = 0;
            int cellRndX = 0;
            int cellRndY = 0;
            SortBugs();
            for (int indexParent = 0; indexParent < 8; indexParent++)
            {
                for (int childIndex = 0; childIndex < 8; childIndex++)
                {
                    int[] childGenom = new int [Bug.PublicLengthGenom];
                    for (int indexGenom = 0; indexGenom < Bug.PublicLengthGenom; indexGenom++)
                    {
                        childGenom[indexGenom] = Bugs[MaxNumberBugs - indexParent - 1].Genom[indexGenom];
                    }
                    if (6 > childIndex & childIndex > 3)
                    {
                        int rndKol = Rnd.Next(0, 8);
                        for (int i = 0; i < rndKol; i++)
                        {
                            childGenom[Rnd.Next(0, Bug.PublicLengthGenom)] = Rnd.Next(0, Bug.PublicLengthGenom);
                        }
                    }
                    if (childIndex > 5)
                    {
                        int rndParent = Rnd.Next(0, 8);
                        for (int indexGenom = 0; indexGenom < Bug.PublicLengthGenom; indexGenom++)
                        {
                            if (Bugs[MaxNumberBugs - indexParent - 1].Genom[indexGenom] ==
                                Bugs[MaxNumberBugs - rndParent - 1].Genom[indexGenom])
                            {
                                childGenom[indexGenom] = Bugs[MaxNumberBugs - indexParent - 1].Genom[indexGenom];
                            }
                            else
                            {
                                if (Rnd.Next(0, 2) == 0)
                                {
                                    childGenom[indexGenom] = Bugs[MaxNumberBugs - rndParent - 1].Genom[indexGenom];
                                }
                                else
                                {
                                    childGenom[indexGenom] = Bugs[MaxNumberBugs - indexParent - 1].Genom[indexGenom];
                                }
                            }
                        }
                        int rndKol = Rnd.Next(0, 3);
                        for (int i = 0; i < rndKol; i++)
                        {
                            childGenom[Rnd.Next(0, Bug.PublicLengthGenom)] = Rnd.Next(0, Bug.PublicLengthGenom);
                        }
                    }
                    while (Field[cellRndY, cellRndX].PublicContent != 0)
                    {
                        cellRndY = Rnd.Next(0, SizeMapY);
                        cellRndX = Rnd.Next(0, SizeMapX);
                    }
                    childs[CurrentNumberBugs] = new Bug(cellRndX, cellRndY, childGenom, Rnd.Next(0, 8));
                    Field[cellRndY, cellRndX].PublicContent = Bug.PublicTypeCell;
                    CurrentNumberBugs++;
                }
            }
            //Проверка
            //string listChildGenom = "";
            //int indexParents = 63;
            //int indexAdd = 0;

            //for (int indexs=0;indexs<64;indexs++)
            //{
            //    int numberMutation = 0;
            //    for (int indexY = 0; indexY < LengthGenomY; indexY++)
            //    {
            //        for (int indexX = 0; indexX < LengthGenomX; indexX++)
            //        {
            //            if (childs[indexs].Genom[indexY, indexX] != Bugs[indexParents].Genom[indexY, indexX])
            //            {
            //                numberMutation++;
            //            }
            //        }
            //    }
            //    listChildGenom = listChildGenom + " " + numberMutation;
            //    indexAdd++;
            //    if (indexAdd % 8 == 0)
            //        indexParents--;
            //}
            //MessageBox.Show(listChildGenom);
            //Конец проверки
            //for (int index = 0; index < Bugs.Length; index++)
            //{
            //    if (Field[Bugs[index].PublicY, Bugs[index].PublicX].PublicContent == 3)
            //    {
            //        Field[Bugs[index].PublicY, Bugs[index].PublicX].PublicContent = 0;
            //    }
            //}
            Bugs = childs;
            Bug.Generation++;
            FileStream bugsFile = new FileStream("Bugs.bin", FileMode.OpenOrCreate);
            BinaryFormatter binForm = new BinaryFormatter();
            binForm.Serialize(bugsFile, childs);
            bugsFile.Close();
        }

        public static void SortAnts()
        {
            bool change = true;
            for (int indexMain = 0; indexMain < Ants.Length & change; indexMain++)
            {
                change = false;
                for (int indexAdditional = 0; indexAdditional < Ants.Length - 1 - indexMain; indexAdditional++)
                {
                    if (Ants[indexAdditional].PublicLifeTime > Ants[indexAdditional + 1].PublicLifeTime)
                    {
                        Ant shelfAnt = Ants[indexAdditional];
                        Ants[indexAdditional] = Ants[indexAdditional + 1];
                        Ants[indexAdditional + 1] = shelfAnt;
                        change = true;
                    }
                }
            }
        }

        public static void CreateChildsAnts()
        {
            Ant[] childs = new Ant[MaxNumberAnts];
            CurrentNumberAnts = 0;
            int cellRndX = 0;
            int cellRndY = 0;
            SortAnts();
            for (int indexParent = 0; indexParent < 8; indexParent++)
            {
                for (int childIndex = 0; childIndex < 8; childIndex++)
                {
                    int[] childGenom = new int[Ant.PublicLengthGenom];
                    for (int indexGenom = 0; indexGenom < Ant.PublicLengthGenom; indexGenom++)
                    {
                        childGenom[indexGenom] = Ants[MaxNumberAnts - indexParent - 1].Genom[indexGenom];
                    }
                    if (6 > childIndex & childIndex > 3)
                    {
                        int rndKol = Rnd.Next(0, 8);
                        for (int i = 0; i < rndKol; i++)
                        {
                            childGenom[Rnd.Next(0, Ant.PublicLengthGenom)] = Rnd.Next(0, Ant.PublicLengthGenom);
                        }
                    }
                    if (childIndex > 5)
                    {
                        int rndParent = Rnd.Next(0, 8);
                        for (int indexGenom = 0; indexGenom < Ant.PublicLengthGenom; indexGenom++)
                        {
                            if (Ants[MaxNumberAnts - indexParent - 1].Genom[indexGenom] ==
                                Ants[MaxNumberAnts - rndParent - 1].Genom[indexGenom])
                            {
                                childGenom[indexGenom] = Ants[MaxNumberAnts - indexParent - 1].Genom[indexGenom];
                            }
                            else
                            {
                                if (Rnd.Next(0, 2) == 0)
                                {
                                    childGenom[indexGenom] = Ants[MaxNumberAnts - rndParent - 1].Genom[indexGenom];
                                }
                                else
                                {
                                    childGenom[indexGenom] = Ants[MaxNumberAnts - indexParent - 1].Genom[indexGenom];
                                }
                            }
                        }
                        int rndKol = Rnd.Next(0, 3);
                        for (int i = 0; i < rndKol; i++)
                        {
                            childGenom[Rnd.Next(0, Ant.PublicLengthGenom)] = Rnd.Next(0, Ant.PublicLengthGenom);
                        }
                    }
                    while (Field[cellRndY, cellRndX].PublicContent != 0)
                    {
                        cellRndY = Rnd.Next(0, SizeMapY);
                        cellRndX = Rnd.Next(0, SizeMapX);
                    }
                    childs[CurrentNumberAnts] = new Ant(cellRndX, cellRndY, childGenom, Rnd.Next(0, 8));
                    Field[cellRndY, cellRndX].PublicContent = Ant.PublicTypeCell;
                    CurrentNumberAnts++;
                }
            }
            Ants = childs;
            Ant.Generation++;
            FileStream antsFile = new FileStream("Ants.bin", FileMode.OpenOrCreate);
            BinaryFormatter binForm = new BinaryFormatter();
            binForm.Serialize(antsFile, childs);
            antsFile.Close();
        }

        public static void CreateCreatures()
        {
            int cellRndX = 0;
            int cellRndY = 0;
            CurrentNumberBugs = 0;
            FileStream bugsFile = new FileStream("Bugs.bin", FileMode.OpenOrCreate);
            BinaryFormatter binForm = new BinaryFormatter();
            if (bugsFile.Length > 0)
            {
                Bugs = (Bug[]) binForm.Deserialize(bugsFile);
                foreach (var bug in Bugs)
                {
                    while (Field[cellRndY, cellRndX].PublicContent != 0)
                    {
                        cellRndY = Rnd.Next(0, SizeMapY);
                        cellRndX = Rnd.Next(0, SizeMapX);
                    }
                    bug.PublicX = cellRndX;
                    bug.PublicY = cellRndY;
                    Field[cellRndY, cellRndX].PublicContent = Bug.PublicTypeCell;
                }
                CurrentNumberBugs = Bugs.Length;
            }
            else
            {
                while (CurrentNumberBugs < MaxNumberBugs)
                {
                    while (Field[cellRndY, cellRndX].PublicContent != 0)
                    {
                        cellRndY = Rnd.Next(0, SizeMapY);
                        cellRndX = Rnd.Next(0, SizeMapX);
                    }
                    int[] genom = new int[Bug.PublicLengthGenom];
                    for (int index = 0; index < Bug.PublicLengthGenom; index++)
                    {
                        genom[index] = Rnd.Next(0, Bug.PublicLengthGenom);
                    }
                    Bugs[CurrentNumberBugs] = new Bug(cellRndX, cellRndY, genom, Rnd.Next(0, 8));
                    Field[cellRndY, cellRndX].PublicContent = Bug.PublicTypeCell;
                    CurrentNumberBugs++;
                }
            }
            bugsFile.Close();
            CurrentNumberAnts = 0;
            FileStream antsFile = new FileStream("Ants.bin", FileMode.OpenOrCreate);
            binForm = new BinaryFormatter();
            if (antsFile.Length > 0)
            {
                Ants = (Ant[]) binForm.Deserialize(antsFile);
                foreach (var bug in Ants)
                {
                    while (Field[cellRndY, cellRndX].PublicContent != 0)
                    {
                        cellRndY = Rnd.Next(0, SizeMapY);
                        cellRndX = Rnd.Next(0, SizeMapX);
                    }
                    bug.PublicX = cellRndX;
                    bug.PublicY = cellRndY;
                    Field[cellRndY, cellRndX].PublicContent = Ant.PublicTypeCell;
                }
                CurrentNumberAnts = Ants.Length;
            }
            else
            {
                while (CurrentNumberAnts < MaxNumberAnts)
                {
                    while (Field[cellRndY, cellRndX].PublicContent != 0)
                    {
                        cellRndY = Rnd.Next(0, SizeMapY);
                        cellRndX = Rnd.Next(0, SizeMapX);
                    }
                    int[] genom = new int[Ant.PublicLengthGenom];
                    for (int index = 0; index < Ant.PublicLengthGenom; index++)
                    {
                        genom[index] = Rnd.Next(0, Ant.PublicLengthGenom);
                    }
                    Ants[CurrentNumberAnts] = new Ant(cellRndX, cellRndY, genom, Rnd.Next(0, 8));
                    Field[cellRndY, cellRndX].PublicContent = Ant.PublicTypeCell;
                    CurrentNumberAnts++;
                }
            }
            antsFile.Close();
        }

        public static void Run()
        {
            for (int index = 0; index < MaxNumberBugs; index++)
            {
                if (Bugs[index].PublicLife > 0)
                {
                    int maxSteps = 256;
                    int steps = 0;
                    bool end = false;
                    Bugs[index]--;
                    Bugs[index].PublicLifeTime++;
                    while (steps < maxSteps & !end)
                    {
                        steps++;
                        int bugX = Bugs[index].PublicX;
                        int bugY = Bugs[index].PublicY;
                        if (Bugs[index].PublicGenomSelected > Bug.PublicLengthGenom - 1)
                        {
                            Bugs[index].PublicGenomSelected = Bugs[index].PublicGenomSelected - Bug.PublicLengthGenom;
                        }
                        int cellBugY = bugY + (int) Math.Round(
                                           Math.Cos((Bugs[index].Genom[Bugs[index].PublicGenomSelected] * 45 +
                                                     Bugs[index].PublicDirection * 45) / 180.0 * Math.PI),
                                           MidpointRounding.AwayFromZero);
                        int cellBugX = bugX + (int) Math.Round(
                                           Math.Sin((Bugs[index].Genom[Bugs[index].PublicGenomSelected] * 45 +
                                                     Bugs[index].PublicDirection * 45) / 180.0 * Math.PI),
                                           MidpointRounding.AwayFromZero);
                        if (Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 8)
                        {
                            Bugs[index].Move(Field[cellBugY, cellBugX].PublicContent, cellBugY, cellBugX);
                            Bugs[index].See(Field[cellBugY, cellBugX].PublicContent);
                            switch (Field[cellBugY, cellBugX].PublicContent)
                            {
                                case 1:
                                {
                                    CurrentNumberFood--;
                                    break;
                                }
                            }
                            Field[bugY, bugX].PublicContent = 0;
                            Field[Bugs[index].PublicY, Bugs[index].PublicX].PublicContent = Bug.PublicTypeCell;
                            end = true;
                        }
                        else
                        {
                            if (Bugs[index].Genom[Bugs[index].PublicGenomSelected] > 7 &
                                Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 16)
                            {
                                Bugs[index].Check(Field[cellBugY, cellBugX].PublicContent);
                                Bugs[index].See(Field[cellBugY, cellBugX].PublicContent);
                                switch (Field[cellBugY, cellBugX].PublicContent)
                                {
                                    case 1:
                                    {
                                        Field[cellBugY, cellBugX].PublicContent = 0;
                                        CurrentNumberFood--;
                                        break;
                                    }
                                    case 2:
                                    {
                                        Field[cellBugY, cellBugX].PublicContent = 1;
                                        CurrentNumberFood++;
                                        CurrentNumberPoison--;
                                        break;
                                    }
                                }
                                end = true;
                            }
                            else
                            {
                                if (Bugs[index].Genom[Bugs[index].PublicGenomSelected] > 15 &
                                    Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 24)
                                {
                                    Bugs[index].See(Field[cellBugY, cellBugX].PublicContent);
                                }
                                else
                                {
                                    if (Bugs[index].Genom[Bugs[index].PublicGenomSelected] > 23 &
                                        Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 32)
                                    {
                                        if (Bugs[index].PublicDirection +
                                            Bugs[index].Genom[Bugs[index].PublicGenomSelected] -
                                            24 > 7)
                                        {
                                            Bugs[index].PublicDirection =
                                                Bugs[index].PublicDirection +
                                                Bugs[index].Genom[Bugs[index].PublicGenomSelected] -
                                                32;
                                        }
                                        else
                                        {
                                            Bugs[index].PublicDirection =
                                                Bugs[index].PublicDirection +
                                                Bugs[index].Genom[Bugs[index].PublicGenomSelected] -
                                                24;
                                        }
                                        Bugs[index].PublicGenomSelected++;
                                    }
                                    else
                                    {
                                        if (Bugs[index].Genom[Bugs[index].PublicGenomSelected] > 31 &
                                            Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 40)
                                        {
                                            if (Field[cellBugY, cellBugX].PublicContent == Ant.PublicTypeCell)
                                            {
                                                bool find = false;
                                                for (int indexAnt = 0; indexAnt < MaxNumberAnts & !find; indexAnt++)
                                                {
                                                    if (Ants[indexAnt].PublicLife > 0 &
                                                        Ants[indexAnt].PublicX == cellBugX &
                                                        Ants[indexAnt].PublicY == cellBugY)
                                                    {
                                                        Ants[indexAnt].PublicLife -= Bug.PublicAttack;
                                                        find = true;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (Field[cellBugY, cellBugX].PublicContent == Spider.PublicTypeCell)
                                                {
                                                    bool find = false;
                                                    for (int indexSpider = 0;
                                                        indexSpider < MaxNumberSpiders & !find;
                                                        indexSpider++)
                                                    {
                                                        if (Spiders[indexSpider].PublicLife > 0 &
                                                            Spiders[indexSpider].PublicX == cellBugX &
                                                            Spiders[indexSpider].PublicY == cellBugY)
                                                        {
                                                            Spiders[indexSpider].PublicLife -= Bug.PublicAttack;
                                                            find = true;
                                                        }
                                                    }
                                                }
                                            }
                                            Bugs[index].PublicGenomSelected++;
                                            end = true;
                                        }
                                        else
                                        {
                                            if (Bugs[index].Genom[Bugs[index].PublicGenomSelected] > 39 &
                                                Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 80)
                                            {
                                                Bugs[index].PublicGenomSelected +=
                                                    Bugs[index].Genom[Bugs[index].PublicGenomSelected];
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    CurrentNumberBugs
                        = 0;
                    for (int indexBugs = 0; indexBugs < MaxNumberBugs; indexBugs++)
                    {
                        if (Bugs[indexBugs].PublicLife > 0)
                        {
                            CurrentNumberBugs++;
                        }
                        else
                        {
                            if (!Bugs[indexBugs].PublicIsDead)
                            {
                                Field[Bugs[indexBugs].PublicY, Bugs[indexBugs].PublicX].PublicContent = 1;
                                Bugs[indexBugs].PublicIsDead = true;
                                CurrentNumberFood++;
                            }
                        }
                    }
                    if (Bugs[index].PublicLife > 90 & MaxNumberBugs - CurrentNumberBugs > 1)
                    {
                        int bugX = Bugs[index].PublicX;
                        int bugY = Bugs[index].PublicY;
                        Bugs[index].PublicLife = 50;
                        int[] childGenom = new int[Bug.PublicLengthGenom];
                        for (int indexGenom = 0; indexGenom < Bug.PublicLengthGenom; indexGenom++)
                        {
                            childGenom[indexGenom] = Bugs[MaxNumberBugs - index - 1].Genom[indexGenom];
                        }
                        bool find = false;
                        for (int i = 0; i < 8 & !find; i++)
                        {
                            int placeY = bugY + (int)Math.Round(Math.Cos(i * 45 / 180.0 * Math.PI),
                                             MidpointRounding.AwayFromZero);
                            int placeX = bugX + (int)Math.Round(Math.Sin(i * 45 / 180.0 * Math.PI),
                                             MidpointRounding.AwayFromZero);
                            if (Field[placeY, placeX].PublicContent == 0)
                            {
                                bool create = false;
                                for (int indexPlaceBug = 0;
                                    indexPlaceBug < Bugs.Length & !create;
                                    indexPlaceBug++)
                                {
                                    if (Bugs[indexPlaceBug].PublicLife == 0)
                                    {
                                        find = true;
                                        create = true;
                                        Bugs[indexPlaceBug] = new Bug(placeX, placeY, childGenom, Rnd.Next(0, 8));
                                        Field[placeY, placeX].PublicContent = Bug.PublicTypeCell;
                                    }
                                }
                            }
                        }
                        int rndKol = Rnd.Next(0, 3);
                        for (int i = 0; i < rndKol; i++)
                        {
                            childGenom[Rnd.Next(0, Bug.PublicLengthGenom)] = Rnd.Next(0, Bug.PublicLengthGenom);
                        }
                        find = false;
                        for (int i = 0; i < 8 & !find; i++)
                        {
                            int placeY = bugY + (int)Math.Round(Math.Cos(i * 45 / 180.0 * Math.PI),
                                             MidpointRounding.AwayFromZero);
                            int placeX = bugX + (int)Math.Round(Math.Sin(i * 45 / 180.0 * Math.PI),
                                             MidpointRounding.AwayFromZero);
                            if (Field[placeY, placeX].PublicContent == 0)
                            {
                                bool create = false;
                                for (int indexPlaceBug = 0;
                                    indexPlaceBug < Bugs.Length & !create;
                                    indexPlaceBug++)
                                {
                                    if (Bugs[indexPlaceBug].PublicLife == 0)
                                    {
                                        find = true;
                                        create = true;
                                        Bugs[indexPlaceBug] = new Bug(placeX, placeY, childGenom, Rnd.Next(0, 8));
                                        Field[placeY, placeX].PublicContent = Bug.PublicTypeCell;
                                    }
                                }
                            }
                        }
                        if (CurrentNumberBugs == MaxNumberBugs)
                        {
                            FileStream bugsFile = new FileStream("Bugs.bin", FileMode.OpenOrCreate);
                            BinaryFormatter binForm = new BinaryFormatter();
                            binForm.Serialize(bugsFile, Bugs);
                            bugsFile.Close();
                        }
                    }
                }
            }
            //for (int index = 0; index < MaxNumberAnts; index++)
            //{
            //    if (Ants[index].PublicLife > 0)
            //    {
            //        int maxSteps = 256;
            //        int steps = 0;
            //        bool end = false;
            //        Ants[index]--;
            //        Ants[index].PublicLifeTime++;
            //        while (steps < maxSteps & !end)
            //        {
            //            steps++;
            //            int antX = Ants[index].PublicX;
            //            int antY = Ants[index].PublicY;
            //            if (Ants[index].PublicGenomSelected > Ant.PublicLengthGenom - 1)
            //            {
            //                Ants[index].PublicGenomSelected = Ants[index].PublicGenomSelected - Ant.PublicLengthGenom;
            //            }
            //            int cellAntY = antY + (int) Math.Round(
            //                               Math.Cos((Ants[index].Genom[Ants[index].PublicGenomSelected] * 45 +
            //                                         Ants[index].PublicDirection * 45) / 180.0 * Math.PI),
            //                               MidpointRounding.AwayFromZero);
            //            int cellAntX = antX + (int) Math.Round(
            //                               Math.Sin((Ants[index].Genom[Ants[index].PublicGenomSelected] * 45 +
            //                                         Ants[index].PublicDirection * 45) / 180.0 * Math.PI),
            //                               MidpointRounding.AwayFromZero);
            //            if (Ants[index].Genom[Ants[index].PublicGenomSelected] < 8)
            //            {
            //                Ants[index].Move(Field[cellAntY, cellAntX].PublicContent, cellAntY, cellAntX);
            //                Ants[index].See(Field[cellAntY, cellAntX].PublicContent);
            //                switch (Field[cellAntY, cellAntX].PublicContent)
            //                {
            //                    case 1:
            //                    {
            //                        CurrentNumberFood--;
            //                        break;
            //                    }
            //                }
            //                Field[antY, antX].PublicContent = 0;
            //                Field[Ants[index].PublicY, Ants[index].PublicX].PublicContent = Ant.PublicTypeCell;
            //                end = true;
            //            }
            //            else
            //            {
            //                if (Ants[index].Genom[Ants[index].PublicGenomSelected] > 7 &
            //                    Ants[index].Genom[Ants[index].PublicGenomSelected] < 16)
            //                {
            //                    Ants[index].Check(Field[cellAntY, cellAntX].PublicContent);
            //                    Ants[index].See(Field[cellAntY, cellAntX].PublicContent);
            //                    switch (Field[cellAntY, cellAntX].PublicContent)
            //                    {
            //                        case 1:
            //                        {
            //                            Field[cellAntY, cellAntX].PublicContent = 0;
            //                            CurrentNumberFood--;
            //                            break;
            //                        }
            //                    }
            //                    end = true;
            //                }
            //                else
            //                {
            //                    if (Ants[index].Genom[Ants[index].PublicGenomSelected] > 15 &
            //                        Ants[index].Genom[Ants[index].PublicGenomSelected] < 24)
            //                    {
            //                        Ants[index].See(Field[cellAntY, cellAntX].PublicContent);
            //                    }
            //                    else
            //                    {
            //                        if (Ants[index].Genom[Ants[index].PublicGenomSelected] > 23 &
            //                            Ants[index].Genom[Ants[index].PublicGenomSelected] < 32)
            //                        {
            //                            if (Ants[index].PublicDirection +
            //                                Ants[index].Genom[Ants[index].PublicGenomSelected] -
            //                                24 > 7)
            //                            {
            //                                Ants[index].PublicDirection =
            //                                    Ants[index].PublicDirection +
            //                                    Ants[index].Genom[Ants[index].PublicGenomSelected] -
            //                                    32;
            //                            }
            //                            else
            //                            {
            //                                Ants[index].PublicDirection =
            //                                    Ants[index].PublicDirection +
            //                                    Ants[index].Genom[Ants[index].PublicGenomSelected] -
            //                                    24;
            //                            }
            //                            Ants[index].PublicGenomSelected++;
            //                        }
            //                        else
            //                        {
            //                            if (Ants[index].Genom[Ants[index].PublicGenomSelected] > 31 &
            //                                Ants[index].Genom[Ants[index].PublicGenomSelected] < 40)
            //                            {
            //                                if (Field[cellAntY, cellAntX].PublicContent == Bug.PublicTypeCell)
            //                                {
            //                                    bool find = false;
            //                                    for (int indexBug = 0; indexBug < MaxNumberBugs & !find; indexBug++)
            //                                    {
            //                                        if (Bugs[indexBug].PublicLife > 0 &
            //                                            Bugs[indexBug].PublicX == cellAntX &
            //                                            Bugs[indexBug].PublicY == cellAntY)
            //                                        {
            //                                            Bugs[indexBug].PublicLife -= Ant.PublicAttack;
            //                                            find = true;
            //                                        }
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    if (Field[cellAntY, cellAntX].PublicContent == Spider.PublicTypeCell)
            //                                    {
            //                                        bool find = false;
            //                                        for (int indexSpider = 0;
            //                                            indexSpider < MaxNumberSpiders & !find;
            //                                            indexSpider++)
            //                                        {
            //                                            if (Spiders[indexSpider].PublicLife > 0 &
            //                                                Spiders[indexSpider].PublicX == cellAntX &
            //                                                Spiders[indexSpider].PublicY == cellAntY)
            //                                            {
            //                                                Spiders[indexSpider].PublicLife -= Ant.PublicAttack;
            //                                                find = true;
            //                                            }
            //                                        }
            //                                    }
            //                                }
            //                                Ants[index].PublicGenomSelected++;
            //                                end = true;
            //                            }
            //                            else
            //                            {
            //                                if (Ants[index].Genom[Ants[index].PublicGenomSelected] > 39 &
            //                                    Ants[index].Genom[Ants[index].PublicGenomSelected] < 80)
            //                                {
            //                                    Ants[index].PublicGenomSelected +=
            //                                        Ants[index].Genom[Ants[index].PublicGenomSelected];
            //                                }
            //                            }

            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //CurrentNumberAnts
            //    = 0;
            //for (int indexAnts = 0; indexAnts < MaxNumberAnts; indexAnts++)
            //{
            //    if (Ants[indexAnts].PublicLife > 0)
            //    {
            //        CurrentNumberAnts++;
            //    }
            //    else
            //    {
            //        if (!Ants[indexAnts].PublicIsDead)
            //        {
            //            Field[Ants[indexAnts].PublicY, Ants[indexAnts].PublicX].PublicContent = 1;
            //            Ants[indexAnts].PublicIsDead = true;
            //            CurrentNumberFood++;
            //        }
            //    }
            //}
        }
    }
}
