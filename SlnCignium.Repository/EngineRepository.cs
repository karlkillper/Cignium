using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using SlnCignium.Entities;
using System.IO;
using System.Runtime.Serialization;
using SlnCignium.Utilities;

namespace SlnCignium.Repository
{
    public class EngineRepository
    {
        string ruta = "";
        BeanAnswer ent = new BeanAnswer();

        public BeanAnswer QuerySearchEngine(BeanSent snt)
        {
            try
            {
                //LOG.registrarLog("Controlador.CONTROLLERComunidad ipServerPublico -> :" + ipServerPublico);
                ruta = snt.MainPath;
                string data = snt.Message;
                ent = wGet(ruta, snt.Operation);
            }
            catch (Exception e)
            {
                //LOG.registrarLog("Controlador.CONTROLLERComunidad wpost -> ERROR:" + e.Message);
                //LOG.registrarLog("Controlador.CONTROLLERComunidad wpost -> ERROR(stacktrace):" + e.StackTrace);
            }
            return ent;
        }
        public static BeanAnswer wGet(string ruta, int Operation)
        {
            //LOG.registrarLog("Controller.ControllerComunidad.HttpPostWA2 -> Ingreso(ruta):" + ruta);
            BeanAnswer ent = new BeanAnswer();
            try
            {

                var url = ruta;
                var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);

                using (var response = webrequest.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    int position = 0;
                    if (Operation == ConfigurationEN.GoogleSearch)
                    {
                        position = result.IndexOf("class=\"sd\"");
                        if (position > 0)
                        {
                            ent.Message = result.Substring(position, ConfigurationEN.LenSearchCountGoogle);
                        }
                        else {
                            ent.Message = "0";
                        }
                        
                    }
                    else if (Operation == ConfigurationEN.BingSearch)
                    {
                        position = result.IndexOf("class=\"sb_count\"");
                        if (position > 0) {
                            ent.Message = result.Substring(position, ConfigurationEN.LenSearchCountBing);
                        }
                        else
                        {
                            ent.Message = "0";
                        }
                    }
                    else
                    {
                        ent.Message = "0";
                    }

                    
                }
                return ent;
            }
            catch (Exception e)
            {
                //LOG.registrarLog("Controller.ControllerComunidad.HttpPostWA2 -> ERROR(stacktrace):" + e.StackTrace);
                //LOG.registrarLog("Controller.ControllerComunidad.HttpPostWA2 -> ERROR:" + e.Message);
                return ent;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }
    }
}
