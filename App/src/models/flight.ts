import { AdditionalField } from "./additionalField";
import { Airport } from "./airport";
import { FlightOption } from "./flightOption";
import { FlightSource } from "./flightSource.enum";
import { Order } from "./order";

export interface Flight {
  idFlight: string;
  basePrice: number;
  departurePlace: Airport;
  arrivalPlace: Airport;
  availableSeats: number;
  orders: Array<Order>;
  options: Array<FlightOption>;
  flightSource: FlightSource;
  extraData: any;
  additionalFields: Array<AdditionalField>;
}