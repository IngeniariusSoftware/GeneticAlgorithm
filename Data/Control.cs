using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Data
{
    class Control
    {
        public static double MaxPercentFood = 0.04;

        public static double MaxPercentPoison = 0.04;

        public static int MaxNumberBugs = 64;

        public static double MaxPercentWalls = 0.1;

        public static int SizeMapX = 60;

        public static int SizeMapY = 30;

        public static Bug[] Bugs = new Bug[MaxNumberBugs];

        public static Cell[,] Field = new Cell[SizeMapY, SizeMapX];

        public static int CurrentNumberFood;

        public static int CurrentNumberPoison;

        public static int CurrentNumberWalls;

        public static int CurrentNumberBugs;

        public static Random Rnd = new Random();

        /// <summary>
        /// Заполнение случайной клетки карты указанным значением
        /// </summary>
        private static void FillEmptyCell(int cellValue)
        {
            int cellRndY = 0;
            int cellRndX = 0;
            while (Field[cellRndY, cellRndX].PublicContent != 0)
            {
                cellRndY = Rnd.Next(0, SizeMapY);
                cellRndX = Rnd.Next(0, SizeMapX);
            }
            Field[cellRndY, cellRndX].PublicContent = cellValue;
        }

        /// <summary>
        /// Генерация пустых клеток карты и её стен
        /// </summary>
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
        }
        /// <summary>
        /// Поддерживаем уровень еды, яда и т.д. на карте
        /// </summary>
        public static void FieldUpdate()
        {
            while (CurrentNumberFood < SizeMapX * SizeMapY * MaxPercentFood)
            {
                FillEmptyCell(1);
                CurrentNumberFood++;
            }
            while (CurrentNumberPoison < SizeMapX * SizeMapY * MaxPercentPoison)
            {
                FillEmptyCell(2);
                CurrentNumberPoison++;
            }
            while (CurrentNumberWalls < SizeMapX * SizeMapY * MaxPercentWalls)
            {
                FillEmptyCell(3);
                CurrentNumberWalls++;
            }
        }

        /// <summary>
        /// Сортировка жуков для дальнейшего отбора
        /// </summary>
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
        }

        public static void PrintInFile()
        {
            FileStream fs = new FileStream("Graphs\\lifedrain.txt",FileMode.Append);
            StreamWriter Sw = new StreamWriter(fs);
            Sw.WriteLine(Bugs[63].PublicLifeTime);
            Sw.Close();
            fs.Close();

            FileStream fs1 = new FileStream("Graphs\\generationnumber.txt", FileMode.Append);
            StreamWriter Sw1 = new StreamWriter(fs1);
            Sw1.WriteLine(Bug.Generation);
            Sw1.Close();
            fs1.Close();
         

        }

        /// <summary>
        /// Создание детей в случае гибели всех жуков
        /// </summary>
        public static void CreateChildsBugs()
        {
            SortBugs();
            PrintInFile();
            Bug[] child = new Bug[MaxNumberBugs];
            CurrentNumberBugs = 0;
            int cellRndX = 0;
            int cellRndY = 0;
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
                    child[CurrentNumberBugs] = new Bug(cellRndX, cellRndY, childGenom, Rnd.Next(0, 8));
                    Field[cellRndY, cellRndX].PublicContent = Bug.PublicTypeCell;
                    CurrentNumberBugs++;
                }
            }
            Bugs = child;
            Bug.Generation++;
            FileStream bugsFile = new FileStream("SaveGeneration\\Bugs.bin", FileMode.OpenOrCreate);
            BinaryFormatter binForm = new BinaryFormatter();
            binForm.Serialize(bugsFile, child);
            bugsFile.Close();
        }

        /// <summary>
        /// Создание первого поколения жуков, либо загрузка существующего
        /// </summary>
        public static void CreateCreatures()
        {
            int cellRndX = 0;
            int cellRndY = 0;
            CurrentNumberBugs = 0;
            FileStream bugsFile = new FileStream("SaveGeneration\\Bugs.bin", FileMode.OpenOrCreate);
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
        }

        /// <summary>
        /// Ход жуков
        /// </summary>
        public static void Run()
        {
            for (int index = 0; index < MaxNumberBugs; index++)
            {
                if (Bugs[index].PublicLife > 0)
                {
                    int maxSteps = 128;
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
                        switch (true)
                        {
                            case true when Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 8:
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
                                break;
                            }
                            case true when Bugs[index].Genom[Bugs[index].PublicGenomSelected] > 7 &
                                           Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 16:
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
                                break;
                            }
                            case true when Bugs[index].Genom[Bugs[index].PublicGenomSelected] > 15 &
                                           Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 24:
                            {
                                Bugs[index].See(Field[cellBugY, cellBugX].PublicContent);
                                break;
                            }
                            case true when Bugs[index].Genom[Bugs[index].PublicGenomSelected] > 23 &
                                           Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 32:
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
                                break;
                            }
                            case true when Bugs[index].Genom[Bugs[index].PublicGenomSelected] > 31 &
                                           Bugs[index].Genom[Bugs[index].PublicGenomSelected] < 64:
                            {
                                Bugs[index].PublicGenomSelected +=
                                    Bugs[index].Genom[Bugs[index].PublicGenomSelected];
                                break;
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
                        int placeY = bugY + (int) Math.Round(Math.Cos(i * 45 / 180.0 * Math.PI),
                                         MidpointRounding.AwayFromZero);
                        int placeX = bugX + (int) Math.Round(Math.Sin(i * 45 / 180.0 * Math.PI),
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
                        int placeY = bugY + (int) Math.Round(Math.Cos(i * 45 / 180.0 * Math.PI),
                                         MidpointRounding.AwayFromZero);
                        int placeX = bugX + (int) Math.Round(Math.Sin(i * 45 / 180.0 * Math.PI),
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
                        FileStream bugsFile = new FileStream("SaveGeneration\\Bugs.bin", FileMode.OpenOrCreate);
                        BinaryFormatter binForm = new BinaryFormatter();
                        binForm.Serialize(bugsFile, Bugs);
                        bugsFile.Close();
                    }
                }

            }
        }
    }
}
