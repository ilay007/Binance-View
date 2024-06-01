# Table of contents:
## 1. Purpose
## 2. About free bot
## 3. How create own bot
## 4. About interface of the application
## 5. Getting started

## 1. Purpose
The purpose of this application is to provide a toolkit that helps you trade in real-time mode. 
    It heps to:
   - make your trading more effective
   - show how profitably you trade
   - help to create your own bot
   - replay recorded historical data to derive feature sets for training

     
     
 ## 2. About bot
   There is a simple bot (free bot) that trades approximately so:  
![image_bot4h_logic](https://github.com/ilay007/Binance-View/assets/44927371/03f866a6-55ca-4ce6-9427-c5fd850ac53b)
This bot can be profitable while the market is fluctuating, but I recommend you to create your own bot.

## 3. How create your own bot.
   - To create your own bot you need to create your own class which initialized interface IStrategist.
   - To enforce your class to work you need to realize just two methods:
     **AddData(CurveBundle curveBundle, KLine point)** - this method supplies the bot with data, you need to process it.
     **MakePrediction(double point)** - this method makes predictions: buy,sell, nothing.
     
 ## 4. About interface of the application
This window shows the history of your trading. There are two buttons on this window in case you have two accounts on Binance.

 ![TradingInfo](https://github.com/ilay007/Binance-View/assets/44927371/9fd7a273-2787-46c4-839e-67ebdaf08cda)
 If click on the button "StartRealTrading", you will see this picture.
 
 ![2024-06-01_12-21-00](https://github.com/ilay007/Binance-View/assets/44927371/c7148815-2f68-4622-9963-8bbf0c8e4f23)

  

     
   
   

# How to start project
To connect to Binance I used the project https://github.com/binance/binance-connector-dotnet.git.
First, create an account on Binance, then configure Binance API for your bot.

Don't forget to mark:
 - Enable Spot & Margin Trading
 - Enable Symbol Whitelist
 - Restrict access to trusted IPs only (Recommended) (Add your Ip)
 - 
 - 
Before you start the project:
1) Sync system time on your computer
2) Create txt file and write there: api key;and api secret. Then write path to this file into value PathToFileWithKeys witch is located in Bot-Viewr/App.config 



The project is in developing mode.


This projcet helps you: 
1) - to count and view your income on the Binance for some coin
2) - to view on the same page for some coin graphics for different time stamps
3) - esasy to write your own robot for trading on the Binance
4) - to check your trading robot on historical data for some coin

       Start
     
For using this project you need to have Binance credential data: ApiKey, SecretKey.
Create keys.txt file and write there: ApiKey;SecretKey.
Find in BinanceIncomeViewer file Settings.settings and find there settings with name PathToFileWithKeys and write there path to file keys.txt.

