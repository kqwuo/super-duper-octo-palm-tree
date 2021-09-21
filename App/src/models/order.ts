import { Ticket } from "./ticket";
import { User } from "./user";
import { Currency } from "./currency";

export interface Order {
  user: User
  nbBought: number
  ticketList: Array<Ticket>
  totalBasePrice: number
  totalAdditionalPrice: number
  totalDiscountedBasePrice: number
  totalPrice: number
  usedCurrency: Currency
  exchangeRate: number
  isPaid: boolean
}