//Plik główny programu zawierający w sobie metodę Main.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1 {
    class Program {
        static void Main(string[] args) {
			Console.BufferHeight = 1000;

            string SudokuPattern = "800030000930007000071520900005010620000050000046080300009076850060100032000040006";

            SudokuState startState = new SudokuState(SudokuPattern);
            SudokuSearch searcher = new SudokuSearch(startState);

            searcher.DoSearch();

            IState state = searcher.Solutions[0];
            List<SudokuState> solutionPath = new List<SudokuState>();

            while(state != null)
            {
                solutionPath.Add((SudokuState)state);
                state = state.Parent;
            }
            solutionPath.Reverse();


            foreach (SudokuState s in solutionPath)
            {
                s.Print();
            }

            Console.Write("Steps count: " + solutionPath.Count);

            Console.ReadKey();

        }
    }
}
