using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NTBrokers.Models;

namespace NTBrokers.Services
{
    public class BrokerDBService
    {
        private SqlConnection _connection;

        public BrokerDBService(SqlConnection connection)
        {
            _connection = connection;
        }
        public List<BrokerModel> AllBrokers()
        {
            // Reading from db
            _connection.Open();

            List<BrokerModel> brokers = new();

            using var command = new SqlCommand("SELECT * FROM Brokers;", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                brokers.Add(new BrokerModel()
                {
                    FirstName = reader.GetString(0),
                    LastName = reader.GetString(1),
                    BrokerId = reader.GetInt32(2)
                });
                // do something with 'value'
            }

            _connection.Close();
            return brokers;
        }

        public void AddBroker(BrokerModel broker)
        {
            _connection.Open();

            string insertText = $"insert into" +
                $" dbo.Brokers (FirstName, LastName) " +
                $" values ('{ broker.FirstName}', '{ broker.LastName}')";
           
            SqlCommand command = new SqlCommand(insertText, _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        public void UpdateBroker(BrokerModel broker)
        {
            _connection.Open();

            string insertText = ($@"update dbo.Brokers set FirstName = N'{ broker.FirstName}', LastName = N'{ broker.LastName}' where ID = '{broker.BrokerId}';");
            SqlCommand command = new SqlCommand(insertText, _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        public List<BrokerModel> GetBrokersByCompany(int companyId)
        {
            _connection.Open();

            List<BrokerModel> brokers = new();

            using var command = new SqlCommand($@"SELECT Brokers.* from((Companies INNER JOIN CompanyBrokers on Companies.CompanyID = CompanyBrokers.CompanyID) INNER JOIN Brokers on Brokers.ID = CompanyBrokers.BrokerID) WHERE Companies.CompanyID = '{companyId}'; ", _connection);
            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                brokers.Add(new BrokerModel()
                {
                    FirstName = reader.GetString(0),
                    LastName = reader.GetString(1),
                    BrokerId = reader.GetInt32(2)
                });
                // do something with 'value'
            }

            _connection.Close();
            return brokers;
        }
        


    }
}
