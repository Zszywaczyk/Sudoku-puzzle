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

            bool UseAdvancedSearch = false;

            SudokuState startState = new SudokuState(SudokuPattern);
            SudokuSearch searcher = new SudokuSearch(startState, UseAdvancedSearch);

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
            string[] heuristicType = { "Simple", "Advanced" };


            string data = string.Format("{0} heuristic: \nSteps: {1}, \ncreated {2} board objects", 
                heuristicType[UseAdvancedSearch ? 0 : 1], solutionPath.Count, SudokuState.counter);
            Console.Write(data);

            Console.ReadKey();

        }
    }
}
