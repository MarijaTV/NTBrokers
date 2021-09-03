using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NTBrokers.Models;

namespace NTBrokers.Services
{
    public class CompanyDBService
    {
        private SqlConnection _connection;

        public CompanyDBService(SqlConnection connection)
        {
            _connection = connection;
        }
        public List<CompanyModel> AllCompanies()
        {
            // Reading from db
            List<CompanyModel> companies = new();
            _connection.Open();

            using var command = new SqlCommand($@"SELECT * FROM Companies", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                companies.Add(new CompanyModel()
                {
                    CompanyId = reader.GetInt32(0),
                    CompanyName = reader.GetString(1),
                    City = reader.GetString(2),
                    Street = reader.GetString(3),
                    Address = reader.GetString(4),
                    
                });
                // do something with 'value'
            }

            _connection.Close();
            return companies;
        }

        public void AddCompany(CompanyModel company)
        {
            _connection.Open();

            string insertText = $"insert into" +
                $" dbo.Companies (CompanyName, City, Street, Adress) " +
                $" values ('{ company.CompanyName}', '{ company.City}', '{ company.Street}', '{ company.Address}')";

            SqlCommand command = new SqlCommand(insertText, _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        public void AddCompany(RealEstateModel model)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand($" insert into Companies (CompanyName, City, Street, Address) " +
                $" values ('{model.Companies[0].CompanyName}', '{model.Companies[0].City}', '{model.Companies[0].Street}', '{model.Companies[0].Address}')", _connection);
            
            int companyId = Convert.ToInt32(command.ExecuteScalar());
            
            _connection.Close();

            //int companyId = GetCompanyId(model);

            _connection.Open();
            foreach (var brokers in model.BrokersIds)
            {
                SqlCommand command2 = new SqlCommand($" insert into CompanyBrokers (CompanyID, BrokerID) " +
                    $" values ('{companyId}', '{brokers}')", _connection);
                command2.ExecuteNonQuery();
            }
            _connection.Close();
        }
        public int GetCompanyId(RealEstateModel model)
        {
            _connection.Open();
            SqlCommand command = new($@"SELECT MAX(CompanyID) FROM Companies
                                                WHERE CompanyName = '{model.Companies[0].CompanyName}'", _connection);
            int companyId = (Int32)command.ExecuteScalar();
            _connection.Close();
            return companyId;
        }

        public void UpdateCompany(RealEstateModel realEstate)
        {
            _connection.Open();
            
            SqlCommand command = new SqlCommand(($@"update dbo.Companies set CompanyName = N'{ realEstate.Company.CompanyName}', City = N'{ realEstate.Company.City}', Street = N'{ realEstate.Company.Street}', Address = N'{ realEstate.Company.Address}' where CompanyID = '{realEstate.Company.CompanyId}'"), _connection);
            command.ExecuteNonQuery();

            _connection.Close();

            _connection.Open();
            
                SqlCommand command2 = new SqlCommand($" delete from CompanyBrokers where CompanyID = '{realEstate.Company.CompanyId}'", _connection);
                command2.ExecuteNonQuery();
            
            _connection.Close();
            _connection.Open();
            foreach (var brokers in realEstate.BrokersIds)
            {
                SqlCommand command3 = new SqlCommand($" insert into CompanyBrokers (CompanyID, BrokerID) " +
                    $" values ('{realEstate.Company.CompanyId}', '{brokers}')", _connection);
                command3.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
