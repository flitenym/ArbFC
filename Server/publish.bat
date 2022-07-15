cd /D E:\Work\ArbFCProject
del /q *.*

rmdir /s /q E:\Work\ArbFCProject\Modules

cd /D E:\Work\ArbFC\Server\Host
dotnet publish -o E:\Work\ArbFCProject

cd /D ..
cd /D Storage.Module
dotnet publish -o E:\Work\ArbFCProject\Modules\Storage.Module

cd /D ..
cd /D Binance.Api
dotnet publish -o E:\Work\ArbFCProject\Modules\Binance.Api

cd /D ..
cd /D Exchange.Common
dotnet publish -o E:\Work\ArbFCProject\Modules\Exchange.Common

cd /D E:\Work\ArbFCProject
start .

pause