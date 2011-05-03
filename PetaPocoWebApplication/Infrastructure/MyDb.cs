using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Web;
using PetaPoco;

namespace PetaPocoWebApplication.Infrastructure
{
    public class MyDb : Database
    {
        public MyDb(IDbConnection connection) : base(connection) { }
        public MyDb(string connectionStringName) : base(connectionStringName) { }
        public MyDb(string connectionString, string providerName) : base(connectionString, providerName) { }
        public MyDb(string connectionString, DbProviderFactory dbProviderFactory) : base(connectionString, dbProviderFactory) { }

        public override void OnException(Exception x)
        {
            // Sql Exception Logging
        }

        Stopwatch sw = new Stopwatch();
        private IDbCommand currentCommand;

        public override void OnExecutingCommand(IDbCommand cmd)
        {
            // Logging
            //File.AppendAllText(HttpContext.Current.Server.MapPath("~/log.txt"), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff") + " - SQL: " + FormatCommand(cmd) + Environment.NewLine);
            currentCommand = cmd;
            sw.Reset();
            sw.Start();
        }

        public override void OnExecutedCommand(IDbCommand cmd)
        {
            sw.Stop();
            if (currentCommand == cmd)
            {
                CurrentRequestTimings.Add(new PetaTiming
                {
                    Time = sw.ElapsedMilliseconds,
                    Sql = FormatCommand(cmd).Replace("\r\n\r\n"," | ").Replace("\r\n",", ")
                });
            }
        }

        public static List<PetaTiming> CurrentRequestTimings
        {
            get
            {
                const string petatimings = "__petaTimings";
                if (HttpContext.Current.Items[petatimings] == null)
                    HttpContext.Current.Items[petatimings] = new List<PetaTiming>();
                return (List<PetaTiming>)HttpContext.Current.Items[petatimings];
            } 
        }

    }

    public class PetaTiming
    {
        public double Time { get; set; }
        public string Sql { get; set; }
    }
}