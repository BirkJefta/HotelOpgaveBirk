using System.Data.SqlClient;

namespace HotelOpgaveBirk {
    internal class Program {
        static void Main(string[] args)
        {
            string connectionString = @"";
            SqlConnection connection = new SqlConnection(connectionString);


            Facility facility = new Facility();
            facility.Name = "Tennis";
            CRUD crud = new CRUD();
            //Create facility (C)
            //crud.CreateFacility(connection, facility);

            //readfacility (R)
            //Console.WriteLine( crud.ReadFacility(connection, 6));
            //read all facilities
            //crud.ReadAllFacilites(connection);

            //facility.Name = "Tennis med bold";
            //facility.Facility_Id = 6;
            ////Update facilities (U)
            //crud.UpdateFacility(connection, facility);

            //DELETE facility (D)

            //crud.DeleteFacility(connection, 6);


            //tilføj facilitet til et hotel:
            //crud.AddFacilityToHotel(connection, 4, 7);

            //se hvilke faciliteter et hotel har:
            //crud.Hotelfacilities(connection, 3);

            //Se hvilke hoteller har en swimmingpool

            //crud.HotelsWithFacility(connection, 4);
        }
    }
}
