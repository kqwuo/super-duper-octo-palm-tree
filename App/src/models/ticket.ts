import { UserType } from "./usertype";

export interface Ticket {
  nbAdditionalLuggage: number
  firstName: string
  lastName: string
  userType: UserType
  basePrice: number
  additionalPrice: number
  paidTotal: number
  basePriceDiscount: number
  discountedBasePrice: number
}