cd /D E:\Work\ArbFCProject
del /q *.*

rmdir /s /q E:\Work\ArbFCProject\Modules

cd /D E:\Work\ArbFC\Server\Host
dotnet publish -o E:\Work\ArbFCProject
cd /D ..
cd /D Telegram.Module
dotnet publish -o E:\Work\ArbFCProject\Modules\Telegram.Module
cd /D ..
cd /D Storage.Module
dotnet publish -o E:\Work\ArbFCProject\Modules\Storage.Module
cd /D ..
cd /D BinanceApi.Module
dotnet publish -o E:\Work\ArbFCProject\Modules\BinanceApi.Module
cd /D ..
cd /D WorkerService.Module
dotnet publish -o E:\Work\ArbFCProject\Modules\WorkerService.Module

cd /D E:\Work\ArbFCProject
start .

pause