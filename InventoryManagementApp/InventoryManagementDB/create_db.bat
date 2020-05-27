echo off

rem batch file to run a script to create a db
rem 9/5/2019

sqlcmd -S LAPTOP-MATT\SQL2017 -E -i Inventory_DB.sql
rem sqlcmd -S localhost\mssqlserver -E -i Inventory_DB.sql
rem sqlcmd -S localhost\sqlexpress -E -i Inventory_DB.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE
