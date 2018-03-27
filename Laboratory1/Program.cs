//Plik główny programu zawierający w sobie metodę Main.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1 {
    class Program {
        static void Main(string[] args) {
			Console.BufferHeight = 1000;
            

            bool UseAdvancedSearch;

			while (true) {
				Console.Write("1. Simple heuristic sudoku \t ...or 4 to get slow\n");
				Console.Write("2. Advanced hs \t\t\t ...or 5 to get slow\n");
				Console.Write("3. Puzzle\n");
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
						break;
					case "4":
						UseAdvancedSearch = true;
						SudokuState.slowdown = true;
						sudoku(UseAdvancedSearch);
						break;
					case "5":
						UseAdvancedSearch = false;
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

				

			}

        }
		static void sudoku(bool UseAdvancedSearch)
		{
			string SudokuPattern = "800030000930007000071520900005010620000050000046080300009076850060100032000040006";
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
			string[] heuristicType = { "Simple", "Advanced" };


			string data = string.Format("{0} heuristic: \nSteps: {1}, \ncreated {2} board objects",
				heuristicType[UseAdvancedSearch ? 0 : 1], solutionPath.Count, SudokuState.counter);

			Console.Write(data);
			Console.Write("\n\nPress key to menu -> ");

			Console.ReadKey();
			Console.Clear();

			SudokuState.counter = 0; // tu zeruje by SudokuState.counter był taki sam za kazdym razem gdy wywołujemy opcje 1 lub 2

			solutionPath = null;
			startState = null;
			searcher = null;
			SudokuState.slowdown = false;
		}
		static void puzzle()
		{

		}
    }
}
