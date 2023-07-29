// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoinCore
{
    public class CoinInfo
    {
        public string Coin;
        public bool DepositAllEnable;
        public bool WithdrawAllEnable;
        public string Name;
        public string Description;
        public double Free;
        public double Locked;
        public double Freeze;
        public double Withdrawing;
        public string Inpoing;
        public string Inpoable;
        public double Storage;
        public bool IsLegalMoney;
        public bool Trading;
        public double Price;
        public double Balance;


        public void SetPrice(double price)
        {
            this.Price = price;
            this.Balance = Free * price;
        }
    }
}
