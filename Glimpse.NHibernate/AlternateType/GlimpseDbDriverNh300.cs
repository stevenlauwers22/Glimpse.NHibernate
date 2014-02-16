using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Glimpse.Ado.AlternateType;
using NHibernate.AdoNet;
using NHibernate.Driver;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;

namespace Glimpse.NHibernate.AlternateType
{
    public class GlimpseDbDriverNh300 
        : IGlimpseDbDriver, IDriver, IEmbeddedBatcherFactoryProvider, ISqlParameterFormatter
    {
        private IDriver _innerDriver;

        public void Wrap(object innerDriver)
        {
            if (innerDriver == null)
                throw new ArgumentNullException("innerDriver");

            _innerDriver = (IDriver)innerDriver;
        }

        public void Configure(IDictionary<string, string> settings)
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

        public IDbDataParameter GenerateParameter(IDbCommand command, string name, SqlType sqlType)
        {
            var parameter = _innerDriver.GenerateParameter(command, name, sqlType);
            return parameter;
        }

        public void ExpandQueryParameters(IDbCommand cmd, SqlString sqlString)
        {
            _innerDriver.ExpandQueryParameters(cmd, sqlString);
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

        public string MultipleQueriesSeparator
        {
            get { return _innerDriver.MultipleQueriesSeparator; }
        }

        public Type BatcherFactoryClass
        {
            get
            {
                var innerBatcherFactoryProvider = _innerDriver as IEmbeddedBatcherFactoryProvider;
                return innerBatcherFactoryProvider != null ? innerBatcherFactoryProvider.BatcherFactoryClass : null;
            }
        }

        public string GetParameterName(int index)
        {
            var innerSqlParameterFormatter = _innerDriver as ISqlParameterFormatter;
            return innerSqlParameterFormatter != null ? innerSqlParameterFormatter.GetParameterName(index) : null;
        }
    }
}