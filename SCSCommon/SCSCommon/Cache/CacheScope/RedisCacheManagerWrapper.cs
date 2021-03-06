﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SCSCommon.Configuration;
using StackExchange.Redis;

namespace SCSCommon.Cache.ExpireCacheScope
{
   public  class RedisConnectionWrapper : IRedisCacheWrapper
    {
        private readonly RedisSection _config;
        private readonly Lazy<string> _connectionString;

        private volatile ConnectionMultiplexer _connection;
        private readonly object _lock = new object();

        public RedisConnectionWrapper(RedisSection config)
        {
            this._config = config;
            this._connectionString = new Lazy<string>(GetConnectionString);
        }

        private string GetConnectionString()
        {
            return string.Empty;
            //return _config.RedisConfigSection.CurrentConfiguration.ConnectionStrings;
        }

        private ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;

            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                if (_connection != null)
                {
                    //Connection disconnected. Disposing connection...
                    _connection.Dispose();
                }

                //Creating new instance of Redis Connection
                _connection = ConnectionMultiplexer.Connect(_connectionString.Value);
            }

            return _connection;
        }

        public IDatabase Database(int? db = null)
        {
            return GetConnection().GetDatabase(db ?? -1); //_settings.DefaultDb);
        }

        public IServer Server(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }

        public EndPoint[] GetEndpoints()
        {
            return GetConnection().GetEndPoints();
        }

        public void FlushDb(int? db = null)
        {
            var endPoints = GetEndpoints();

            foreach (var endPoint in endPoints)
            {
                Server(endPoint).FlushDatabase(db ?? -1); //_settings.DefaultDb);
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
