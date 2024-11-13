using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace HotelOpgaveBirk {
    public class CRUD {

        public int CreateFacility (SqlConnection connection, Facility facility) 
        {
            Console.WriteLine();
            //laver sql command. Da id selv laves er det kun navnet
            string SQLCommandString = $"INSERT INTO Facility VALUES ('{facility.Name}')";
            Console.WriteLine(SQLCommandString);

            //får den til at køre script, på valgte conneciton

            SqlCommand command = new SqlCommand(SQLCommandString, connection);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                command.Connection.Open();
            }
            int numberOfRowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"linjer der blev opdateret: {numberOfRowsAffected}");
            command.Connection.Close();
            return numberOfRowsAffected;
        }

        public Facility ReadFacility (SqlConnection connection, int Facility_Id)
        {
            //finder specifik facilitet:
            Console.WriteLine();
            string SQLCommandString = $"SELECT * FROM Facility WHERE Facility_Id = {Facility_Id}";
            Console.WriteLine(SQLCommandString);

            SqlCommand command = new SqlCommand(SQLCommandString , connection);

            //da den kun læser data bruge datareadr i stedet for excecutenonquery:
            if(connection.State !=System.Data.ConnectionState.Open) 
            {
                command.Connection.Open();
            }
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                
                Console.WriteLine("Ingen hoteller med id");
                reader.Close();

            }
            Facility facility = null;
            if (reader.Read() )
            {
                facility = new Facility();
                {
                    facility.Facility_Id = reader.GetInt32(0);
                    facility.Name = reader.GetString(1);
                }
            }
            
            reader.Close();
            Console.WriteLine();
            command.Connection.Close();
            return facility;
        }

        public void ReadAllFacilites(SqlConnection connection)
        {
            Console.WriteLine();
            string SQLCommandString = "SELECT * FROM Facility";
            Console.WriteLine(SQLCommandString);
            SqlCommand command = new SqlCommand(SQLCommandString, connection);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                command.Connection.Open();
            }
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                
                Console.WriteLine("Ingen hoteller");
                reader.Close();

                

            }
            List<Facility> facilities = new List<Facility>();
            while (reader.Read())
            {
                Facility Nextfacility = new Facility();
                {
                    Nextfacility.Facility_Id = reader.GetInt32(0);
                    Nextfacility.Name = reader.GetString(1);
                }
                facilities.Add(Nextfacility);
                Console.WriteLine(Nextfacility);
            }
            reader.Close();
            command.Connection.Close();
            Console.WriteLine();
            
        }
        public int UpdateFacility(SqlConnection connection, Facility facility)
        {
            Console.WriteLine();
            //laver sql command. Da id selv laves er det kun navnet
            string SQLCommandString = $"UPDATE Facility SET Name = ('{facility.Name}') WHERE Facility_Id = ({facility.Facility_Id})";
            Console.WriteLine(SQLCommandString);

            //får den til at køre script, på valgte conneciton

            SqlCommand command = new SqlCommand(SQLCommandString, connection);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                command.Connection.Open();
            }
            int numberOfRowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"linjer der blev opdateret: {numberOfRowsAffected}");
            Console.WriteLine(ReadFacility(connection,facility.Facility_Id));
            command.Connection.Close();
            return numberOfRowsAffected;
        }

        public int DeleteFacility (SqlConnection connection, int facility_no)
        {
            Console.WriteLine();
            //laver sql command. Da id selv laves er det kun navnet
            string SQLCommandString = $"DELETE FROM Facility WHERE Facility_Id = ({facility_no})";
            Console.WriteLine(SQLCommandString);

            //får den til at køre script, på valgte conneciton

            SqlCommand command = new SqlCommand(SQLCommandString, connection);
            //hvis connection ikke allerede er åbnet af tidligere kommandoer, åben connection.
            if (connection.State != System.Data.ConnectionState.Open)
            {
                command.Connection.Open();
            }
            int numberOfRowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"linjer der blev opdateret: {numberOfRowsAffected}");
            //luk connection
            command.Connection.Close();
            return numberOfRowsAffected;

        }

        public int AddFacilityToHotel(SqlConnection connection, int facility_no, int hotel_no)
        {
            Console.WriteLine();
            //laver sql command. Da id selv laves er det kun navnet
            string SQLCommandString = $"INSERT INTO HotelFacility VALUES ({hotel_no}, {facility_no})";
            Console.WriteLine(SQLCommandString);

            //får den til at køre script, på valgte conneciton

            SqlCommand command = new SqlCommand(SQLCommandString, connection);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                command.Connection.Open();
            }
            int numberOfRowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"linjer der blev opdateret: {numberOfRowsAffected}");
            command.Connection.Close();
            return numberOfRowsAffected;

        }

        public void Hotelfacilities (SqlConnection connection, int Hotel_No)
        {
            Console.WriteLine();
            //vælger facilitet id og navn samt hotellets navn.
            //Joiner dem sammen så jeg får navnet med når jeg henter id i hotelfacilities.
            //where til at bestemme hvilket hotel det passer til
            string SQLCommandString = $"SELECT Facility.Facility_Id, Facility.Name, Hotel.Name\r\nFROM Facility\r\nINNER Join HotelFacility ON HotelFacility.Facility_Id = Facility.Facility_Id\r\nINNER JOIN Hotel ON Hotel.Hotel_No = HotelFacility.Hotel_No\r\nWHERE HotelFacility.Hotel_No = {Hotel_No};";
            Console.WriteLine(SQLCommandString);

            SqlCommand command = new SqlCommand(SQLCommandString, connection);

            //da den kun læser data bruge datareadr i stedet for excecutenonquery:
            if (connection.State != System.Data.ConnectionState.Open)
            {
                command.Connection.Open();
            }
            SqlDataReader reader = command.ExecuteReader();
            List<Facility> facilities = new List<Facility>();
            //hvis der ingen værdier er med hotel nummeret
            if (!reader.HasRows)
            {
                //End here
                Console.WriteLine("Ingen hoteller med id eller hotellet har ingen faciliteter");
                reader.Close();

                //Return null for 'no hotels found'

            }
            else
            {
                bool HotelNameDisplayed = false;
                while (reader.Read())
                {
                    //viser kun hotel navn en gang
                    if (!HotelNameDisplayed)
                    {
                        Hotel hotel = new Hotel();
                        {
                            Console.WriteLine();
                            hotel.Name = reader.GetString(2);
                        }
                        Console.WriteLine($"hotel: {hotel.Name} has facilities:");
                        HotelNameDisplayed = true;
                    }


                    Facility Nextfacility = new Facility();
                    {
                        Nextfacility.Facility_Id = reader.GetInt32(0);
                        Nextfacility.Name = reader.GetString(1);
                    }
                    facilities.Add(Nextfacility);
                    Console.WriteLine($" Facility Id: {Nextfacility.Facility_Id}, Name: {Nextfacility.Name}");
                    Console.WriteLine();
                }
            }
            reader.Close();
            Console.WriteLine();
            command.Connection.Close();
        }
        public void HotelsWithFacility(SqlConnection connection, int Facility_Id)
        {
            Console.WriteLine();
            //vælger facilitet id og navn samt hotellets navn.
            //Joiner dem sammen så jeg får navnet med når jeg henter id i hotelfacilities.
            //where til at bestemme hvilket hotel det passer til
            string SQLCommandString = $"SELECT Hotel.Name AS HotelName, Facility.Facility_Id, Facility.Name\r\nFROM Facility\r\nINNER Join HotelFacility ON HotelFacility.Facility_Id = Facility.Facility_Id\r\nINNER JOIN Hotel ON Hotel.Hotel_No = HotelFacility.Hotel_No\r\nWHERE HotelFacility.Facility_Id = {Facility_Id};";
            Console.WriteLine(SQLCommandString);

            SqlCommand command = new SqlCommand(SQLCommandString, connection);

            //da den kun læser data bruge datareadr i stedet for excecutenonquery:
            if (connection.State != System.Data.ConnectionState.Open)
            {
                command.Connection.Open();
            }
            SqlDataReader reader = command.ExecuteReader();
            List<Hotel> facilities = new List<Hotel>();
            //hvis der ingen værdier er med hotel nummeret
            if (!reader.HasRows)
            {
                //End here
                Console.WriteLine("Ingen hoteller med id eller ingen hoteller med indtastede facilitet ");
                reader.Close();

                //Return null for 'no hotels found'

            }
            else
            {
                bool FacilityNameDisplayed = false;
                while (reader.Read())
                {
                    //viser kun hotel navn en gang
                    if (!FacilityNameDisplayed)
                    {
                        Facility facility = new Facility();
                        {
                            Console.WriteLine();
                            facility.Name = reader.GetString(2);
                        }
                        Console.WriteLine($"{facility.Name} can be found on following hotels:");
                        FacilityNameDisplayed = true;
                    }


                    Hotel NextHotel = new Hotel();
                    {
                        NextHotel.Name = reader.GetString(0);
                    }
                    facilities.Add(NextHotel);
                    
                    Console.WriteLine($"- {NextHotel.Name}");
                }
            }
            reader.Close();
            Console.WriteLine();
            command.Connection.Close();
        }
    }
}
