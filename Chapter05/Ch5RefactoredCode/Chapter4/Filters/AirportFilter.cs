﻿namespace Packt.CloudySkiesAir.Chapter4.Filters;

public class AirportFilter : FlightFilterBase {
  public bool IsDeparture { get; set; }
  public Airport Airport { get; set; }

  public override bool ShouldInclude(IFlightInfo flight) {
    if (IsDeparture) {
      return flight.Departure.Location == Airport;
    }
    return flight.Arrival.Location == Airport;
  }
}
