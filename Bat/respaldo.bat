@echo off

REM Comando para realizar el respaldo de SQL Server
sqlcmd -S DESKTOP-4FJOI9V\SQLSERVER2022 -d PuraVidaStore -E -Q "BACKUP DATABASE PuraVidaStore TO DISK = 'C:\Backups\BackupPuraVida.bak'"

REM Mensaje para indicar que el respaldo se completó
echo Respaldado completado.
