﻿using System;
using System.Data;
using Microsoft.Extensions.Logging;

namespace Model.DataAccess
{

    public class DaoBase : IDaoBase
    {
        private readonly ILogger<IDaoBase> _logger;
        private readonly IDbConnection _connection;
        private static IDbTransaction _transaction;

        public DaoBase(IDbConnection connection)
        {
            _connection = connection;
        }

        public void EnsureTransaction()
        {
            if(_transaction == null) 
            {
                throw new Exception("A transaction is required");
            }
        }

        public TResult WithTransaction<TResult>(Func<TResult> method, ILogger logger = null, bool rollback = false)
        {
            if (_transaction != null) { method(); }

            var result = default(TResult);
            _transaction = _connection.BeginTransaction();
            try
            {
                result = method();

                if (rollback)
                {
                    _transaction.Rollback();
                }
                else
                {
                    _transaction.Commit();
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError($"Unable to complete call to {method.Method.Name}. Rolling back transaction.", ex);
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction = null;
            }
        }

        public void WithTransaction(Action method, ILogger logger = null, bool rollback = false)
        {
            if (_transaction != null) { method(); }

            _transaction = _connection.BeginTransaction();
            try
            {
                method();

                if (rollback)
                {
                    _transaction.Rollback();
                    return;
                }

                _transaction.Commit();
            }
            catch (Exception ex)
            {
                logger.LogError($"Unable to complete call to {method.Method.Name}. Rolling back transaction.", ex);
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction = null;
            }
        }
    }
}