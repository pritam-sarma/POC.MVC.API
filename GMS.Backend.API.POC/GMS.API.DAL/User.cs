using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace GMS.Backend.API.POC.GMS.API.DAL
{
    public class User
    {
        //create database connection to store user details

        public void connectToDB()
        {
            //string connectionString = "connectionString";
            //SqlConnection connection = new SqlConnection(connectionString);
            //connection.Open();

            //SqlCommand cmd = new SqlCommand();

        }

        public bool Authenticate(string userName, string password)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = $"select UserName from tblUser where UserName = '{userName}' and Password ='{password}' ";
                    var dr = cmd.ExecuteReader();
                    string _userName = null;

                    if (dr.Read())
                    {
                        _userName = dr["UserName"].ToString();
                    }
                    dr.Close();
                    return _userName == userName;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void CreateUserTokens()
        {

        }

    }
}