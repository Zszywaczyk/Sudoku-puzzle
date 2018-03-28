using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory1
{
	public class SudokuState : State
	{
        #region public variables
        public const int SMALL_GRID_SIZE = 3;
        public const int GRID_SIZE = SMALL_GRID_SIZE * SMALL_GRID_SIZE;
        public static int counter = 0;
		public static bool slowdown = false;
        #endregion


        #region private variables
        private string id;
        private int[,] table;

        #endregion

        #region constructors
        public SudokuState(string sudokuPattern) : base()
        {
            if (sudokuPattern.Length != (GRID_SIZE * GRID_SIZE))
            {
                throw new Exception("Incorrect length of sudoku string pattern.");
            }

            this.id = sudokuPattern;
            this.table = new int[GRID_SIZE, GRID_SIZE];

            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    this.table[i, j] = sudokuPattern[(i * GRID_SIZE) + j] - 48;
                }
            }

            this.h = ComputeHeuristicGrade();
        }

        public SudokuState(SudokuState parent, int newValue, int x, int y) : base(parent)
        {
            counter++;

            this.table = new int[GRID_SIZE, GRID_SIZE];
            Array.Copy(parent.table, this.table, this.table.Length);
            this.table[x, y] = newValue;

            StringBuilder builder = new StringBuilder(parent.id);
            builder[x * GRID_SIZE + y] = (char)(newValue + 48);
            this.id = builder.ToString();

            this.h = ComputeHeuristicGrade();
        }
        #endregion




        #region public methods
        public int[,] Table {
			get { return this.table; }
			set { this.table = value; }
        }

		public override string ID
		{
			get { return this.id; }
		}

		public override double ComputeHeuristicGrade()
		{
            double emptyFields = 0.0;
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    if (Table[i, j] == 0)
                    {
                        emptyFields++;
                    }
                }
            }
            return emptyFields;
		}

        public bool isNumberCorrect(int Value, int x, int y)
        {
            bool isCorrect = true;

            // SMALL GRID Index on board
            int sgIdx = (x == 0 ? 0 : (int)(x / SMALL_GRID_SIZE));
            int sgIdy = (y == 0 ? 0 : (int)(y / SMALL_GRID_SIZE));

            // Small grid index of TOP LEFT corner
            int sgBeginX = sgIdx * SMALL_GRID_SIZE;
            int sgBeginY = sgIdy * SMALL_GRID_SIZE;

            for (int i = 0; i < SudokuState.GRID_SIZE; i++)
            {
                // ROW
                if (Table[x, i] == Value)
                {
                    isCorrect = false;
                    break;
                }

                // COL
                if (Table[i, y] == Value)
                {
                    isCorrect = false;
                    break;
                }

                // Current index for element of small grid (=sg) on Table:`     i  0  1 2 3 4 5  6  7  8  9
                int cIdx = sgBeginX + (i / SudokuState.SMALL_GRID_SIZE); // sgX + 0, 0, 0, 1, 1, 1, 2, 2, 2
                int cIdy = sgBeginY + (i % SudokuState.SMALL_GRID_SIZE); // sgY + 0, 1, 2, 0, 1, 2, 0, 1, 2

                if (Table[cIdx, cIdy] == Value)
                {
                    isCorrect = false;
                    break;
                }
            }
            return isCorrect;
        }

        public void Print() {
            StringBuilder builder = new StringBuilder();
            //string output = "";

            for (int i = 0; i < GRID_SIZE; i++)
            {
                builder.Append(getRowFormated(i));

                builder.Append(i+1 % 3 != 0 ? "" : "\n");
            }
            builder.Append("\n");

            Console.Write(builder.ToString());
			if (slowdown == true)
			{
				Console.ReadKey();
				Console.Clear();
			}
		}

        private string getRowFormated(int rowID)
        {
            if (rowID >= GRID_SIZE)
            {
                throw new Exception("Incorrect rowID (SudokuState::getRowFormated)");
            }

            StringBuilder builder = new StringBuilder("\n|");
            int beginIdx = rowID * GRID_SIZE;

            for (int i = 0; i < SMALL_GRID_SIZE; i++)
            {
                int cIdx = beginIdx + (i * SMALL_GRID_SIZE);
                builder.Append(string.Format(" {0} {1} {2} |", id[cIdx], id[cIdx + 1], id[cIdx + 2]));
            }
            
            return builder.ToString().Replace("0", " ");
        }

        #endregion
    }
}
