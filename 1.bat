@echo off
cd E\ E:\Git\Sudoku.Tools
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Tools\obj\Debug\netcoreapp2.1\Sudoku.Tools.assets.cache
git rm -r --cached  E:\Git\Sudoku.Tools\Sudoku.Tools\obj\Debug\netcoreapp2.1\Sudoku.Tools.csproj.FileListAbsolute.txt
git add .
git commit -m 'updategitignore'
cmd

