using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory1
{
    class PuzzleState : State
    {
        private string id;

        public readonly int GRIDSIZE;
        private int[,] Table;
        private int mixMoveCounter;

        private bool useMH; // Manhattan Heuristc

        public int[] emptyFieldPosition = { 0, 0 };
        //  0   1    2    3 
        public bool[] Directions_LTRB = new bool[4]; // Left-Top-Right-Bot

        public PuzzleState(int gridSize, bool useManhattan = false, int mixMoves = 1000) : base()
        {
            GRIDSIZE = gridSize;
            Table = new int[GRIDSIZE, GRIDSIZE];
            mixMoveCounter = mixMoves;
            useMH = useManhattan;

            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    Table[i, j] = i * GRIDSIZE + j;
                }
            }
            mixPuzzle();

            this.h = ComputeHeuristicGrade();
            this.id = getTableString();
            this.g = 0;
        }

        public PuzzleState(PuzzleState parent, int dir) : base(parent)
        {
            GRIDSIZE = parent.GRIDSIZE;
            Table = new int[GRIDSIZE, GRIDSIZE];
            Array.Copy(parent.Table, this.Table, this.Table.Length);

            emptyFieldPosition = new int[2];
            Array.Copy(parent.emptyFieldPosition, emptyFieldPosition, emptyFieldPosition.Length);

            move(dir);

            this.id = getTableString();

            this.h = ComputeHeuristicGrade();

            this.g = parent.g + 1;
        }

        public override string ID
        {
            get { return this.id; }
        }

        public override double ComputeHeuristicGrade()
        {
            return (useMH == false ? misplacedTilesH() : manhattanH()); 
        }

        private double misplacedTilesH()
        {
            double incorrectNums = 0;
            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    int cCorrect = i * GRIDSIZE + j;
                    if (Table[i, j] != cCorrect)
                    {
                        incorrectNums++;
                    }
                }
            }
            return incorrectNums;
        }

        private double manhattanH()
        {

            return 0.0;
        }

        public void Print()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    builder.Append(Table[i, j] == 0 ? " " : Table[i, j].ToString());
                    builder.Append(Table[i, j] > 9 ? " " : "  ");
                }
                builder.Append("\n");
            }
            builder.Append("\n");
            Console.Write(builder.ToString());
        }

        public void checkDirections()
        {
            // Direction possibilities
            Directions_LTRB[0] = (emptyFieldPosition[0] > 0 ? true : false);
            Directions_LTRB[1] = (emptyFieldPosition[1] > 0 ? true : false);
            Directions_LTRB[2] = (emptyFieldPosition[0] < GRIDSIZE - 1 ? true : false);
            Directions_LTRB[3] = (emptyFieldPosition[1] < GRIDSIZE - 1 ? true : false);
        }

        private void mixPuzzle()
        {
            short dirValue;

            Random rnd = new Random();

            for (int i = 0; i < mixMoveCounter; i++)
            {
                checkDirections();

                // Get possible random direction
                do
                {
                    dirValue = (short)rnd.Next(4);
                } while (!Directions_LTRB[dirValue]);

                move(dirValue);
            }
        }

        private void move(int dirValue)
        {
            int moveX = 0;
            int moveY = 0;
            if (dirValue == 0 || dirValue == 2)
            {
                moveX = dirValue - 1;
            }
            else if (dirValue == 1 || dirValue == 3)
            {
                moveY = dirValue - 2;
            }

            Table[emptyFieldPosition[0], emptyFieldPosition[1]] = Table[emptyFieldPosition[0] + moveX, emptyFieldPosition[1] + moveY];
            Table[emptyFieldPosition[0] + moveX, emptyFieldPosition[1] + moveY] = 0;

            emptyFieldPosition[0] += moveX;
            emptyFieldPosition[1] += moveY;
        }

        private string getTableString()
        {
            StringBuilder idBuilder = new StringBuilder();
            for (int i = 0; i < GRIDSIZE; i++)
            {
                for (int j = 0; j < GRIDSIZE; j++)
                {
                    idBuilder.Append(Table[i, j]);
                }
            }
            return idBuilder.ToString();
        }
    }
}
