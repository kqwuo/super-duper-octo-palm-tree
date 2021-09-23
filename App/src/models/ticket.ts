import { AdditionalField } from "./additionalField";
import { FlightOption } from "./flightOption";
import { UserType } from "./usertype";

export interface Ticket {
  firstName: string;
  lastName: string;
  userType: UserType;
  basePrice: number;
  paidTotal: number;
  basePriceDiscount: number;
  discountedBasePrice: number;
  options: Array<FlightOption>;
  additionalFields: Array<AdditionalField>;
}