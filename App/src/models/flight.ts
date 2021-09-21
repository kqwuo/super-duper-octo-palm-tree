import { Airport } from "./airport";
import { Order } from "./order";

export interface Flight {
  idFlight: string
  basePrice: number
  additionalLuggagePrice: number
  departurePlace: Airport
  arrivalPlace: Airport
  availableSeats: number
  orders: Array<Order>
}