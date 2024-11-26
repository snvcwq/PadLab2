@echo off

cd C:\Users\denis\OneDrive\Desktop\PadLab2\FoodDeliveryAPI\bin\Debug\net6.0

start "" dotnet FoodDeliveryAPI.dll --urls http://localhost:5182;http://localhost:5183 --environment Development1
start "" dotnet FoodDeliveryAPI.dll --urls http://localhost:5184;http://localhost:5185 --environment "Development2"
