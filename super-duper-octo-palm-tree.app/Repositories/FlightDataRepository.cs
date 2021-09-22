using super_duper_octo_palm_tree.app.Repositories.DdContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.Repositories
{
    public static class FlightDataRepository
    {
        public static List<FlightData> FlightDatas { private get; set; }

        public static void InitializeData()
        {
            FlightDatas = new List<FlightData>();
            FlightDatas.Add(new FlightData() { IdFlight = Guid.Parse("bb1cd427-8591-42c2-9ee8-e163d2c333c8").ToString(), DeparturePlace = "DTW", ArrivalPlace = "CDG", BasePrice = 700, AvailableSeats = 700 });
            FlightDatas.Add(new FlightData() { IdFlight = Guid.Parse("bbc85e45-fca0-4e1c-8ed1-3ea88acedea8").ToString(), DeparturePlace = "CDG", ArrivalPlace = "JFK", BasePrice = 1000, AvailableSeats = 1000 });
            FlightDatas.Add(new FlightData() { IdFlight = Guid.Parse("5261397c-2cbc-4d94-96dd-79c25354db09").ToString(), DeparturePlace = "DTW", ArrivalPlace = "JFK", BasePrice = 300, AvailableSeats = 300, AdditionalLuggagePrice = 30 });
        }

        public static List<FlightData> GetFlights() => FlightDatas;
        public static List<FlightData> GetFlight(string idFlight) => FlightDatas.Where(x=> x.IdFlight == idFlight).ToList();

    }
}
