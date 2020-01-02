@echo off
cd E\ E:\Git\Sudoku.Tools
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Console\obj\Debug\netcoreapp2.1\*
git rm -r --cached  E:\Git\Sudoku.Tools\SudoKu.Test\bin\Debug\netcoreapp2.1\*
git rm -r --cached  E:\Git\Sudoku.Tools\SudoKu.Test\obj\Debug\netcoreapp2.1\*
git rm -r --cached  E:\Git\Sudoku.Tools\SudoKu.Tools\bin\Debug\netcoreapp2.1\*
git rm -r --cached  E:\Git\Sudoku.Tools\SudoKu.Tools\obj\Debug\netcoreapp2.1\*
git add .
git commit -m 'updategitignore'
cmd

