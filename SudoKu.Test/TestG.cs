using Autofac;
using Sudoku.Core;
using Sudoku.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace SudoKu.Test
{
   public static  class TestG
    {
        public static List<ISudokuSolveHandler> SolveHandlers
        {
            get
            {
                Assembly[] assemblies = new Assembly[] { typeof(SolverHandlerBase).Assembly };
                var builder = new ContainerBuilder();
                builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
                IContainer container = builder.Build();
                List<ISudokuSolveHandler> solveHandlers = container.Resolve<IEnumerable<ISudokuSolveHandler>>().ToList();
                return solveHandlers;
            }
        }
    }
}
