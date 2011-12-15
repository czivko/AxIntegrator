using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CandyDirect.AxIntegrator
{
    public class AxIntegratorService: System.ServiceProcess.ServiceBase
    {
        public AxIntegratorService()
        {
            this.ServiceName = "AxIntegratorService";
            this.CanStop = true;
            this.CanPauseAndContinue = false;
            this.AutoLog = true;
        }
        public void Start()
        {
        	OnStart(null);
        }
        
        protected override void OnStart(string[] args)
        {
        	NLog.LogManager.GetCurrentClassLogger().Info("AxIntegratorService.OnStart()");
 

            //GetNewOrdersFromMagento
            //UpdateOdersThatShippedFromShipWorks
            //push new inventory to stores
            	//new items
            	//inventory levels
            	//turn item on or off
             var workerThread = new Thread(new ThreadStart(GetNewOrdersFromAmazon));
             workerThread.Start();
        }

        protected override void OnStop()
        {
           // TODO: add shutdown stuff
        }

        public void GetNewOrdersFromAmazon()
        {
          do
          {
          	try
      		{
            	NLog.LogManager.GetCurrentClassLogger().Info("AxIntegratorService.GetNewOrdersFromAmazon()");
                //15 minutes = 900 000 milliseconds
                //Thread.Sleep(900000);
                Thread.Sleep(2000);
                //throw new ArgumentNullException("charles says fail");
                
            }
            catch(Exception e)
            {
            	NLog.LogManager.GetCurrentClassLogger().Error(e);
            }
          }
          while (true);
            
        }
    }

}
