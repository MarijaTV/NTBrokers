using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NTBrokers.Models;

namespace NTBrokers.Services
{
    public class ApartmentDBService
    {
        private SqlConnection _connection;

        public ApartmentDBService(SqlConnection connection)
        {
            _connection = connection;
        }
        public List<ApartmentModel> AllApartments()
        {
            // Reading from db
            _connection.Open();

            List<ApartmentModel> apartments = new();

            using var command = new SqlCommand("SELECT * FROM Apartment_company;", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                apartments.Add(new ApartmentModel()
                {
                    ApartmentId = reader.GetInt32(0),
                    ApartmentType = reader.GetString(1),
                    City = reader.GetString(2),
                    Street = reader.GetString(3),
                    HouseNo = reader.GetInt32(4),
                    FloorOf = reader.GetInt32(5),
                    Floors = reader.GetInt32(6),
                    Company = reader.IsDBNull(10) ? "" : reader.GetString(10),
                    Broker = reader.IsDBNull(13) ? "" : reader.GetString(13) ,
                    Area = reader.GetDecimal(9)
                });
                // do something with 'value'
            }

            _connection.Close();
            return apartments;
        }


        public void AddApartment(ApartmentModel apartment)
        {
            _connection.Open();

            string insertText = $"insert into" +
                $" dbo.Apartments ( ApartmentType, City, Street, HouseNo, FloorOf, Floors, Area, Company ) " +
                $" values ('{apartment.ApartmentType}', '{apartment.City}', '{apartment.Street}', '{apartment.HouseNo}', '{apartment.FloorOf}', '{apartment.Floors}', '{apartment.Area}', '{apartment.CompanyId}')";

            SqlCommand command = new SqlCommand(insertText, _connection);
            command.ExecuteNonQuery();

            _connection.Close();
        }
        public ApartmentModel GetApartment(int ApartmentId)
        {
            // Reading from db
            _connection.Open();

            using var command = new SqlCommand($"SELECT * FROM Apartment_company WHERE ApartmentsId = '{ApartmentId}';", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                return new ApartmentModel() { 
                
                    ApartmentId = reader.GetInt32(0),
                    ApartmentType = reader.GetString(1),
                    City = reader.GetString(2),
                    Street = reader.GetString(3),
                    HouseNo = reader.GetInt32(4),
                    FloorOf = reader.GetInt32(5),
                    Floors = reader.GetInt32(6),
                    CompanyId = int.Parse(reader.GetString(7)),
                    Company = reader.IsDBNull(10) ? "" : reader.GetString(10),
                    Broker = reader.IsDBNull(8) ? "" : reader.GetString(8),
                    Area = Convert.ToInt32(reader.GetDecimal(9)),
                };
                // do something with 'value'
            }

            _connection.Close();
            return null;
        }
        public void UpdateApartment(ApartmentModel apartment)
        {
            _connection.Open();

            SqlCommand command = new SqlCommand(($@"update dbo.Apartments set ApartmentType = '{ apartment.ApartmentType }', City = N'{ apartment.City }', Street = N'{ apartment.Street}', HouseNo = '{ apartment.HouseNo }', FloorOf = '{ apartment.FloorOf }', Floors = '{ apartment.Floors }', Area = '{ apartment.Area }', Broker = '{apartment.Broker}', Company = '{ apartment.CompanyId}' where ApartmentsId = '{apartment.ApartmentId}'"), _connection);

            command.ExecuteNonQuery();

            _connection.Close();
        }
        public List<ApartmentModel> BrokerApartments(int BrokerId)
        {
            _connection.Open();

            List<ApartmentModel> apartments = new();

            using var command = new SqlCommand($"SELECT * FROM Apartment_company where Broker = '{BrokerId}';", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                apartments.Add(new ApartmentModel()
                {
                    ApartmentId = reader.GetInt32(0),
                    ApartmentType = reader.GetString(1),
                    City = reader.GetString(2),
                    Street = reader.GetString(3),
                    HouseNo = reader.GetInt32(4),
                    FloorOf = reader.GetInt32(5),
                    Floors = reader.GetInt32(6),
                    Company = reader.IsDBNull(10) ? "" : reader.GetString(10),
                    Broker = reader.IsDBNull(8) ? "" : reader.GetString(8),
                    Area = reader.GetDecimal(9)
                });
                // do something with 'value'
            }

            _connection.Close();
            return apartments;
        }

        public List<ApartmentModel> AllEmptyApartments (int Brokerid)
        {
            _connection.Open();
            List<ApartmentModel> apartments = new();
            using var command = new SqlCommand($"SELECT * FROM Apartments_company_broker WHERE Id = '{Brokerid}';", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                apartments.Add (new ApartmentModel()
                {    
                    ApartmentId = reader.GetInt32(0),
                    ApartmentType = reader.GetString(1),
                    City = reader.GetString(2),
                    Street = reader.GetString(3),
                    HouseNo = reader.GetInt32(4),
                    Company = reader.IsDBNull(6) ? "" : reader.GetString(6),
                });
                // do something with 'value'
            }
            _connection.Close();
            return apartments;
        }
        public void UpdateBrokerApartment(BrokerDetailsModel apartment)
        {
            _connection.Open();
            foreach (var apartmentsIds in apartment.BrokersApartmentsIds)
            {
                SqlCommand command = new SqlCommand(($@"UPDATE Apartments SET Apartments.Broker = '{ apartment.BrokerId }' WHERE ApartmentsID = '{ apartmentsIds}'"), _connection);

                command.ExecuteNonQuery();
            }
            _connection.Close();
        }

    }
}
