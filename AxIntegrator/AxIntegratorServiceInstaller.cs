using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace CandyDirect.AxIntegrator
{
    [RunInstaller(true)]
    public class AxIntegratorServiceInstaller : Installer
    {

          private ServiceProcessInstaller processInstaller;
          private ServiceInstaller serviceInstaller;

          public AxIntegratorServiceInstaller()
          {
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "AxIntegratorService"; //must match CronService.ServiceName

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
          } 
    }  
}
