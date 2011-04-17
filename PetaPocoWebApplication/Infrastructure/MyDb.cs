﻿using System;
using System.Data;
using System.Data.Common;
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

        public override void OnExecutingCommand(IDbCommand cmd)
        {
            // Logging
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/log.txt"), DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff") + " - SQL: " + FormatCommand(cmd) + Environment.NewLine);
        }
    }
}