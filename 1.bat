@echo off
cd E\ E:\Git\Sudoku.Tools
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Console\obj\Debug\netcoreapp2.1\Sudoku.Console.deps.json
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Console\obj\Debug\netcoreapp2.1\Sudoku.Console.dll
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Console\obj\Debug\netcoreapp2.1\Sudoku.Console.pdb
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Console\obj\Debug\netcoreapp2.1\Sudoku.Console.runtimeconfig.dev.json
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Console\obj\Debug\netcoreapp2.1\Sudoku.Console.runtimeconfig.json
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Console\obj\Debug\netcoreapp2.1\Sudoku.Core.dll
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Console\obj\Debug\netcoreapp2.1\Sudoku.Core.pdb
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Console\obj\Debug\netcoreapp2.1\Sudoku.Tools.dll
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Console\obj\Debug\netcoreapp2.1\Sudoku.Tools.pdb
git add .
git commit -m 'updategitignore'
cmd