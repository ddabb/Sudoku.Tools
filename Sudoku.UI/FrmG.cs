using Autofac;
using Sudoku.Core;
using Sudoku.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sudoku.UI
{
    /// <summary>
    /// 窗体的全局类
    /// </summary>
    public class FrmG
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
