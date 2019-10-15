using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace NoMasAccidentes.App_Start
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                // Creamos el demonio
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // Iniciamos el demonio
                scheduler.Start();


                //........................................................//


                // Definimos un trabajo y lo asociamos a la clase MyJobs
                IJobDetail every24hJob = JobBuilder.Create<Job>()
                    .WithIdentity("generarFactura", "app")
                    .Build();

                // Lanzamos el job todos los días a las 
                ITrigger every24hTrigger = TriggerBuilder.Create()
                    .WithIdentity("generarFactura", "app")
                    .StartAt(DateBuilder.DateOf(02, 17, 0))
                    .WithSimpleSchedule(s => s
                        .WithIntervalInHours(24)
                        .RepeatForever())
                    .Build();

                // Asociamos el job y el trigger al demonio
                scheduler.ScheduleJob(every24hJob, every24hTrigger);



            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }
        }
    }
}