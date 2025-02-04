﻿using Packt.CloudySkiesAir.Chapter10;

LoggingDictionary<string, BoardingPass> passDict = new();
LoggingDictionary<string, FlightInfo> flightDict = new();

List<BoardingPass> passes = PassGenerator.Generate();
foreach (BoardingPass pass in passes) {
    string message = pass switch { 
        {
            Flight.Status: FlightStatus.Pending,
            Group: 1 or 2 or 3
        }
         => $"{pass.passenger} board now", 
        { Flight.Status: not  FlightStatus.Active or FlightStatus.Completed } 
            => $"{pass.passenger} flight missed",
        _ => $"{pass.passenger} please wait",
    };
    Console.WriteLine(message);
}

const string apiKey = "RefactoringWithCSharpBook";
CloudySkiesFlightProvider cloudySkies = new();

IEnumerable<FlightInfo> flights = cloudySkies.GetFlightsByStatus(null, apiKey)
  .OrderBy(f => f.DepartureTime)
  .ThenBy(f => f.ArrivalTime);

foreach (FlightInfo flight in flights) {
    Console.WriteLine(flight);
}

Console.WriteLine("Enter a flight #: ");
string id = Console.ReadLine()!;

FlightInfo? myFlight = cloudySkies.GetFlight(id, apiKey);
if (myFlight != null) {
    Console.WriteLine($"Found flight {id}");
} else {
    Console.WriteLine($"Could not find flight {id}");
}


