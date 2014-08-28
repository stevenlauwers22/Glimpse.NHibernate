using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using Glimpse.Ado.AlternateType;
using NHibernate.Driver;
using NHibernate.Engine;
using NHibernate.Impl;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;

namespace Glimpse.NHibernate.AlternateType
{
    public class GlimpseDbDriverNh1214000 
        : IGlimpseDbDriver, IDriver, ISqlParameterFormatter
    {
        private IDriver _innerDriver;

        public void Wrap(object innerDriver)
        {
            if (innerDriver == null)
                throw new ArgumentNullException("innerDriver");

            _innerDriver = (IDriver)innerDriver;
        }

        public void Configure(IDictionary settings)
        {
            _innerDriver.Configure(settings);
        }

        public IDbConnection CreateConnection()
        {
            var innerConnection = _innerDriver.CreateConnection();
            if (innerConnection is GlimpseDbConnection)
                return innerConnection;

            var connection = new GlimpseDbConnection(innerConnection as DbConnection);
            return connection;
        }

        public IDbCommand GenerateCommand(CommandType type, SqlString sqlString, SqlType[] parameterTypes)
        {
            var innerCommand = _innerDriver.GenerateCommand(type, sqlString, parameterTypes);
            if (innerCommand is GlimpseDbCommand)
                return innerCommand;

            var command = new GlimpseDbCommand(innerCommand as DbCommand);
            return command;
        }

        public void PrepareCommand(IDbCommand command)
        {
            _innerDriver.PrepareCommand(command);
        }

        public bool SupportsMultipleOpenReaders
        {
            get { return _innerDriver.SupportsMultipleOpenReaders; }
        }

        public bool SupportsMultipleQueries
        {
            get { return _innerDriver.SupportsMultipleQueries; }
        }

        public IBatcher CreateBatcher(ConnectionManager connectionManager)
        {
            var batcher = _innerDriver.CreateBatcher(connectionManager);
            return batcher;
        }

        public string GetParameterName(int index)
        {
            var innerSqlParameterFormatter = _innerDriver as ISqlParameterFormatter;
            return innerSqlParameterFormatter != null ? innerSqlParameterFormatter.GetParameterName(index) : null;
        }
    }
}