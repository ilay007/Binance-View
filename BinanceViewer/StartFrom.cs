// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Windows.Forms;
using BinanceAcountViewer;
using BinanceAcountViewer.Properties;

namespace AcountViewer
{
    public partial class StartFrom : Form
    {
       
        public StartFrom()
        {
            InitializeComponent();
            Credentials.InitCredentials(Settings.Default.PathToFileWithKeys);
           
        }

        private void InitKeys()
        {
           
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var credentials=Credentials.GetCredentials();
            var user1Form = new TradingInfo(credentials[0].Key, credentials[0].Value);
            user1Form.IsMdiContainer = true;
            user1Form.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var credentials = Credentials.GetCredentials();
            var user1Form = new BotViewer(credentials[1].Key, credentials[1].Value);
            user1Form.IsMdiContainer = true;
            user1Form.Show();
        }

       
    }
}
