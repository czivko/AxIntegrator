using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using CandyDirect.AppServices;

namespace CandyDirect.AxIntegrator
{
    public class AxIntegratorService: System.ServiceProcess.ServiceBase
    {
    	OrderService orderService;
    	int checkForNewOrdersInterval;
        public AxIntegratorService()
        {
            this.ServiceName = "AxIntegratorService";
            this.CanStop = true;
            this.CanPauseAndContinue = false;
            this.AutoLog = true;
            var val = System.Configuration.ConfigurationManager.AppSettings["CheckForNewOrdersInterval"];
           checkForNewOrdersInterval = val != null ? int.Parse(val) :  900000; //15 minutes = 900 000 milliseconds
            
            NLog.LogManager.GetCurrentClassLogger().Info(" checkForNewOrdersInterval => {0}", checkForNewOrdersInterval);
        }
        public void Start()
        {
        	OnStart(null);
        }
        
        protected override void OnStart(string[] args)
        {
        	NLog.LogManager.GetCurrentClassLogger().Info("AxIntegratorService.OnStart()");
        	orderService = new OrderService();

            //GetNewOrdersFromMagento
            //UpdateOdersThatShippedFromShipWorks
            //push new inventory to stores
            	//new items
            	//inventory levels
            	//turn item on or off
             var workerThread = new Thread(new ThreadStart(GetNewOrders));
             workerThread.Start();
        }

        protected override void OnStop()
        {
           // TODO: add shutdown stuff
        }

        public void GetNewOrders()
        {
          do
          {
          	try
      		{
            	NLog.LogManager.GetCurrentClassLogger().Info("AxIntegratorService.GetNewOrders()");
            	orderService.ProcessNewOrders();
                
                Thread.Sleep(checkForNewOrdersInterval);                
                
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
