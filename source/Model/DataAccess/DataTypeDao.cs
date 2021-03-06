﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Logging;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;

namespace Model.DataAccess
{
    public static class DataTypeDaoSql
    {
        public static string Insert = "INSERT INTO DataType (Id, Name, ClrType) Values (@Id, @Name, @ClrType);";
        public static string Update = "UPDATE DataType SET (Name = @Name, ClrType = @ClrType) WHERE Id = @Id);";
        public static string Delete = "DELETE FROM DataType WHERE Id = @Id;";
        public static string Get = "SELECT Id, Name, ClrType FROM DataType WHERE Id = @Id;";
        public static string GetAll = "SELECT Id, Name, ClrType FROM DataType;";
    }

    public class DataTypeDao : DaoBase, IDataTypeDao
    {
        public DataTypeDao(IDbConnection connection, ILogger<IDataTypeDao> logger) : base(connection, logger)
        {
            
        }

        public Guid Insert(DataTypeDto dto)
        {
            dto.Id = Guid.NewGuid();

            _connection.Query<int>(DataTypeDaoSql.Insert, new { Name = dto.Name, ClrType = dto.ClrType, Id = dto.Id }, _transaction);

            return dto.Id;
        }

        public void Update(DataTypeDto dto)
        {
            _connection.Query<int>(DataTypeDaoSql.Update, new { Name = dto.Name, ClrType = dto.ClrType }, _transaction);
        }

        public void Delete(DataTypeDto dto)
        {
            _connection.Query<int>(DataTypeDaoSql.Delete, new { Id = dto.Id}, _transaction);
        }

        public DataTypeDto Get(Guid id)
        {
            return _connection.Query<DataTypeDto>(DataTypeDaoSql.Get, new {Id = id}, _transaction).Single();
        }

        public IList<DataTypeDto> GetAll()
        {
            return _connection.Query<DataTypeDto>(DataTypeDaoSql.GetAll, _transaction).ToList();
        }
    }
}
