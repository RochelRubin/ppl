﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace People.Data
{
    public class PeopleDb
    {

        private string _connectionString;

        public PeopleDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            conn.Open();
            var reader = cmd.ExecuteReader();
            List<Person> ppl = new List<Person>();
            while (reader.Read())
            {
                ppl.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],

                });
            }

            return ppl;
        }

        public void AddPerson(Person person)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                "VALUES(@f, @l, @a) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@f", person.FirstName);
            cmd.Parameters.AddWithValue("@l", person.LastName);
            cmd.Parameters.AddWithValue("@a", person.Age);
            conn.Open();
            person.Id = (int)(decimal)cmd.ExecuteScalar();
        }

        public void Update(Person person)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE People SET FirstName = @f, LastName = @l, Age = @a WHERE Id = @id";
            cmd.Parameters.AddWithValue("@f", person.FirstName);
            cmd.Parameters.AddWithValue("@l", person.LastName);
            cmd.Parameters.AddWithValue("@a", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM People WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
