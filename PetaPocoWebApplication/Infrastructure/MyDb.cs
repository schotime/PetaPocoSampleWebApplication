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

        Stopwatch sw = new Stopwatch();
        private IDbCommand currentCommand;

        public override void OnExecutingCommand(IDbCommand cmd)
        {
            // Logging
            //File.AppendAllText(HttpContext.Current.Server.MapPath("~/log.txt"), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff") + " - SQL: " + FormatCommand(cmd) + Environment.NewLine);
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                currentCommand = cmd;
                sw.Reset();
                sw.Start();
            }
        }

        public override void OnExecutedCommand(IDbCommand cmd)
        {
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                sw.Stop();
                if (currentCommand == cmd)
                {
                    CurrentRequestTimings.Add(new PetaTuning
                    {
                        Time = sw.ElapsedTicks/10000d,
                        FormattedSql = FormatCommand(cmd),
                        Sql = cmd.CommandText,
                        Parameters = cmd.Parameters
                    });
                }
            }
        }

        public static List<PetaTuning> CurrentRequestTimings
        {
            get
            {
                const string petatimings = "__petaTunings";
                if (HttpContext.Current.Items[petatimings] == null)
                    HttpContext.Current.Items[petatimings] = new List<PetaTuning>();
                return (List<PetaTuning>)HttpContext.Current.Items[petatimings];
            } 
        }

    }

    public class PetaTuning
    {
        public double Time { get; set; }
        public string FormattedSql { get; set; }
        public string Sql { get; set; }
        public IDataParameterCollection Parameters { get; set; }
    }
}