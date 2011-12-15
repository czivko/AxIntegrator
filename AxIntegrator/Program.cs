using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CandyDirect.AxIntegrator
{
    static class Program
    {
        static void Main()
        {
#if(DEBUG)  
            AxIntegratorService svc = new AxIntegratorService();
            svc.Start();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            System.ServiceProcess.ServiceBase.Run(new AxIntegratorService());
#endif    
        }
    }

}
