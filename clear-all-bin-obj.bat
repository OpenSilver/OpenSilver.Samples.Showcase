@echo off
echo Clean-Build v3.0 - Removes bin and obj directories
echo ---------------------------------------------------
setlocal enabledelayedexpansion
set BIN_FOUND=0
set OBJ_FOUND=0

rem Check and delete bin in current directory
if exist "bin" (
    set BIN_FOUND=1
    echo Deleting: bin in current directory
    RMDIR /S /Q "bin"
)

rem Check and delete obj in current directory
if exist "obj" (
    set OBJ_FOUND=1
    echo Deleting: obj in current directory
    RMDIR /S /Q "obj"
)

echo Searching and deleting bin directories in subdirectories...
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S bin 2^>nul') DO (
    set BIN_FOUND=1
    echo Deleting: %%G
    RMDIR /S /Q "%%G"
)

echo Searching and deleting obj directories in subdirectories...
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S obj 2^>nul') DO (
    set OBJ_FOUND=1
    echo Deleting: %%G
    RMDIR /S /Q "%%G"
)

if !BIN_FOUND! EQU 0 (
    echo No bin directories found.
)
if !OBJ_FOUND! EQU 0 (
    echo No obj directories found.
)
endlocal