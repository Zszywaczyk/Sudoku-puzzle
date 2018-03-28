//Plik główny programu zawierający w sobie metodę Main.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Laboratory1 {
    class Program {
        static void Main(string[] args) {
			Console.BufferHeight = 1000;
            

            bool UseAdvancedSearch;

			while (true) {
                Console.Write("1. Simple heuristic sudoku \t ...or 4 to get slow\n");
				Console.Write("2. Advanced hs \t\t\t ...or 5 to get slow\n");
				Console.Write("3. Puzzle Missplaced tiles\n");
				Console.Write("4. Puzzle Manhattan\n");
				Console.Write("\nq. Exit\n");
				Console.Write("\n\nYour choice: ");
				string choice = Console.ReadLine();

				switch (choice) {

					case "1":
						UseAdvancedSearch = false;     //tutaj musiales sie pomylic bo przy true jest podstawowy a przy false adv
						sudoku(UseAdvancedSearch);
						break;
					case "2":
						UseAdvancedSearch = true;						
						sudoku(UseAdvancedSearch);
						break;
					case "3":
						puzzle(false);
						break;
					case "4":
						puzzle(true);
						break;
					case "5":
						UseAdvancedSearch = false;
						SudokuState.slowdown = true;
						sudoku(UseAdvancedSearch);
						break;
					case "6":
						UseAdvancedSearch = true;
						SudokuState.slowdown = true;
						sudoku(UseAdvancedSearch);
						break;
					case "q":
						Environment.Exit(0);
						break;
					default:
						Console.Clear();
						Console.WriteLine("Choose between 1-3");
						break;
				}
                Console.ReadKey();
                Console.Clear();

            }

        }

		static void sudoku(bool UseAdvancedSearch)
		{
			string[] heuristicType = { "Simple", "Advanced" };
			string[] hash = { "800030000930007000071520900005010620000050000046080300009076850060100032000040006", "000000600600700001401005700005900000072140000000000080326000010000006842040002930", "457682193600000007100000004200000006584716239300000008800000002700000005926835471", "000012034000056017000000000000000000480000051270000048000000000350061000760035000", "000700800000040030000009001600500000010030040005001007500200600030080090007000002", "100040002050000090008000300000509000700080003000706000007000500090000040600020001", "600040003010000070005000800000502000300090002000103000008000900070000050200030004", "000000000000003085001020000000507000004000100090000000500000073002010000000040009", "000040700080000000010000020000800006700000050400000200302070000000000000000006018" };
			string SudokuPattern;

			int m = getInt("Choice Sudoku: ");
			SudokuPattern = hash[(m > hash.Length ? hash.Length - 1 : m)];

			SudokuState startState = new SudokuState(SudokuPattern);
			SudokuSearch searcher = new SudokuSearch(startState, UseAdvancedSearch);

			searcher.DoSearch();

			IState state = searcher.Solutions[0];
			List<SudokuState> solutionPath = new List<SudokuState>();

			while (state != null)
			{
				solutionPath.Add((SudokuState)state);
				state = state.Parent;
			}
			solutionPath.Reverse();


			foreach (SudokuState s in solutionPath)
			{
				s.Print();
			}

			string data = string.Format("{0} heuristic: \nSteps: {1}, \ncreated {2} board objects",
				heuristicType[UseAdvancedSearch ? 0 : 1], solutionPath.Count, SudokuState.counter);

			Console.Write(data);
			Console.Write("\nOpen: "+searcher.Open.Count);
			Console.Write("\nClose: " + searcher.Closed.Count);
			Console.Write("\n\nPress key to menu -> ");

            // Cleaning
			SudokuState.counter = 0;
			SudokuState.slowdown = false;
		}

		static int getInt(string text, int defValue = 0)
		{
			Console.Write(text);
			int temp = defValue;

			try
			{
				temp = Convert.ToInt32(Console.ReadLine());
			}
			catch (FormatException)
			{
				Console.Write("To nie zadziala \n");
			}

			return temp;
		}

		static void puzzle(bool useManhattan)
		{
			int N = getInt("NxN N=", 3);
			int mix = getInt("How many mix: ", 1000);
			int numOfRandomBoards = getInt("How many boards: ", 100);

			

			int openAverage = 0;
            int closeAverage = 0;
            int objectsAverage = 0;


            PuzzleState startState;
            PuzzleSearch searcher;

            var stopwatch = new Stopwatch();
			var elapsedTime = new Stopwatch();
			var allElapsedTime = new Stopwatch();
			
			for (int i = 0; i < numOfRandomBoards; i++)
            {
				
				stopwatch.Start();
				startState = new PuzzleState(N, useManhattan, mix);
                searcher = new PuzzleSearch(startState);
				allElapsedTime.Start();
				searcher.DoSearch();
				allElapsedTime.Stop();
				//Console.Write(searcher.Open.Count);
				//Console.ReadKey();
				IState state = searcher.Solutions[0];
                List<PuzzleState> solutionPath = new List<PuzzleState>();

				stopwatch.Stop();
				
				int openCounter = 0;
                while (state != null)
                {
                    openCounter += state.Children.Count;

                    solutionPath.Add((PuzzleState)state);
                    state = state.Parent;
                }
				solutionPath.Reverse();

                openAverage += searcher.Open.Count;
                closeAverage += searcher.Closed.Count;

                objectsAverage += PuzzleState.counter;
                PuzzleState.counter = 0;

				if (N < 5 && i == 0)
				{
					foreach (PuzzleState s in solutionPath)
					{
						s.Print();
					}
					Console.Write("\nComputing");
				}
				if (i % 10 == 0)
				{
					Console.Write(".");
				}

				solutionPath = null;
			}
            openAverage /= numOfRandomBoards;
            closeAverage /= numOfRandomBoards;
            objectsAverage /= numOfRandomBoards;

			

			string[] methods = { "Missplaced tiles", "Manhattan" };

            StringBuilder datas = new StringBuilder();
            datas.Append(String.Format("\n{0}\n", useManhattan == false ? methods[0] : methods[1]));
            datas.Append(String.Format("Openned: {0}, Closed: {1} \nCreated {2} boards objects\n", openAverage, closeAverage, objectsAverage));
            datas.Append(String.Format("Average time: {0}\n", allElapsedTime.ElapsedMilliseconds/numOfRandomBoards));

            Console.Write(datas);
		}
    }
}
