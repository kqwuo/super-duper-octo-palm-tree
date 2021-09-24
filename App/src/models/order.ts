import { Ticket } from "./ticket";
import { User } from "./user";
import { Currency, CurrencyType } from "./currency";
import { FlightSource } from "./flightSource.enum";

export interface Order {
  user: User
  nbBought: number
  ticketList: Array<Ticket>
  date: string
  totalBasePrice: number
  totalAdditionalPrice: number
  totalDiscountedBasePrice: number
  totalPrice: number
  usedCurrency: CurrencyType
  exchangeRate: number
  isPaid: boolean
  flightSource: FlightSource;
  extraData?: any;
}