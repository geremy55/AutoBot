using AutoBot.Betting.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Controllers
{
    abstract public class BaseBackground
    {  
        protected BackgroundWorker myWorker; 

        public BaseBackground()
        {
            myWorker = new BackgroundWorker();
            myWorker.DoWork += MyWorker_DoWork;
            myWorker.WorkerReportsProgress = true;
            myWorker.ProgressChanged += MyWorker_ProgressChanged;
            myWorker.RunWorkerCompleted += MyWorker_RunWorkerCompleted;
            myWorker.WorkerSupportsCancellation = true;            
        }

        protected virtual void MyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
        }
        protected virtual void MyWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        protected virtual void MyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { 
        }
    }
}
