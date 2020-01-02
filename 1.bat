@echo off
cd E\ E:\Git\Sudoku.Tools
git rm -r --cached  E:\Git\Sudoku.Tools\SudoKu.Test\obj\Debug\netcoreapp2.1\SudoKu.Test.AssemblyInfo.cs
git rm -r --cached  E:\Git\Sudoku.Tools\SudoKu.Test\obj\Debug\netcoreapp2.1\SudoKu.Test.AssemblyInfoInputs.cache
git rm -r --cached  E:\Git\Sudoku.Tools\SudoKu.Test\obj\Debug\netcoreapp2.1\SudoKu.Test.assets.cache
git rm -r --cached  E:\Git\Sudoku.Tools\SudoKu.Test\obj\Debug\netcoreapp2.1\SudoKu.Test.csproj.CopyComplete
git rm -r --cached  E:\Git\Sudoku.Tools\SudoKu.Test\obj\Debug\netcoreapp2.1\SudoKu.Test.csproj.CoreCompileInputs.cache
git rm -r --cached  E:\Git\Sudoku.Tools\SudoKu.Test\obj\Debug\netcoreapp2.1\SudoKu.Test.Program.cs
git add .
git commit -m 'updategitignore'
cmd