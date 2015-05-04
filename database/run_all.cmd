ECHO %USERNAME% started the batch process at %TIME%

for %%f in (*.sql) do sqlcmd.exe  -S .\SQLSERVER2014 -E   -d TaskFlamingo -i %%f