@echo off
cd E\ E:\Git\Sudoku.Tools
git rm -r --cached  E:\Git\Sudoku.Tools\.vs\Sudoku.Tools\v16\Server\sqlite3\db.lock
git rm -r --cached  E:\Git\Sudoku.Tools\.vs\Sudoku.Tools\v16\Server\sqlite3\storage.ide
git rm -r --cached  E:\Git\Sudoku.Tools\.vs\Sudoku.Tools\v16\Server\sqlite3\storage.ide-shm
git rm -r --cached  E:\Git\Sudoku.Tools\.vs\Sudoku.Tools\v16\Server\sqlite3\storage.ide-wal

git add .
git commit -m 'updategitignore'
cmd

