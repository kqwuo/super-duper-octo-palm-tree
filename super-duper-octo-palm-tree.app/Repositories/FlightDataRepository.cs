using super_duper_octo_palm_tree.app.Repositories.DdContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace super_duper_octo_palm_tree.app.Repositories
{
    public static class FlightDataRepository
    {
        public static List<FlightData> FlightDatas { private get; set; }

        public static void InitializeRepository()
        {
            FlightDatas = new List<FlightData>();
        }

        public static List<FlightData> InitializeData(DateTime date)
        {
            List<FlightData> flightDatas = new List<FlightData>();
            flightDatas.Add(new FlightData() { IdFlight = Guid.NewGuid().ToString(), DeparturePlace = "DTW", ArrivalPlace = "CDG", BasePrice = 700, AvailableSeats = 700, Date = date.Date });
            flightDatas.Add(new FlightData() { IdFlight = Guid.NewGuid().ToString(), DeparturePlace = "CDG", ArrivalPlace = "JFK", BasePrice = 1000, AvailableSeats = 1000, Date = date.Date });
            flightDatas.Add(new FlightData() { IdFlight = Guid.NewGuid().ToString(), DeparturePlace = "DTW", ArrivalPlace = "JFK", BasePrice = 300, AvailableSeats = 300, AdditionalLuggagePrice = 30, Date = date.Date });

            FlightDatas.AddRange(flightDatas);

            return flightDatas;
        }

        public static List<FlightData> GetFlights(DateTime date) {
            List<FlightData> result;

            result = FlightDatas.Where(data => data.Date.Date == date.Date).ToList();

            if (result.Count() == 0)
                result = InitializeData(date);

            return result;
        }

        public static FlightData GetFlight(string idFlight) => FlightDatas.Where(x => x.IdFlight == idFlight).First();

        public static void UpdateFlight(FlightData flightData)
        {
            FlightDatas[FlightDatas.FindIndex(x => x.IdFlight == flightData.IdFlight)].AvailableSeats = flightData.AvailableSeats;
        }

    }
}
