﻿//Plik główny programu zawierający w sobie metodę Main.


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
                        puzzle();
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
                Console.ReadKey();
                Console.Clear();

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

            // Cleaning
			SudokuState.counter = 0; // tu zeruje by SudokuState.counter był taki sam za kazdym razem gdy wywołujemy opcje 1 lub 2
			solutionPath = null;
			startState = null;
			searcher = null;
			SudokuState.slowdown = false;
		}


		static void puzzle()
		{
            bool useManhattan = true;

            int openAverage = 0;
            int closeAverage = 0;
            int objectsAverage = 0;

            int numOfRandomBoards = 100;

            PuzzleState startState;
            PuzzleSearch searcher;

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < numOfRandomBoards; i++)
            {
                startState = new PuzzleState(3, useManhattan);
                searcher = new PuzzleSearch(startState);

                searcher.DoSearch();

                IState state = searcher.Solutions[0];
                List<PuzzleState> solutionPath = new List<PuzzleState>();

                int openCounter = 0;
                while (state != null)
                {
                    openCounter += state.Children.Count;

                    solutionPath.Add((PuzzleState)state);
                    state = state.Parent;
                }

                openAverage += openCounter;
                closeAverage += solutionPath.Count;

                objectsAverage += PuzzleState.counter;
                PuzzleState.counter = 0;

                solutionPath = null;
                startState = null;
                searcher = null;
            }
            stopwatch.Stop();
            var elapsedTime = stopwatch.ElapsedMilliseconds;

            openAverage /= numOfRandomBoards;
            closeAverage /= numOfRandomBoards;
            objectsAverage /= numOfRandomBoards;


            string[] methods = { "Missplaced tiles", "Manhattan" };

            StringBuilder datas = new StringBuilder();
            datas.Append(String.Format("{0}\n", useManhattan == false ? methods[0] : methods[1]));
            datas.Append(String.Format("Openned: {0}, Closed: {1} \nCreated {2} boards objects\n", openAverage, closeAverage, objectsAverage));
            datas.Append(String.Format("Elapsed time: {0}\n", elapsedTime));

            Console.Write(datas);
        }
    }
}
