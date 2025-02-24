import { Discount } from "./discount";
import { MyAppUser } from "./my-app-user";

export interface Passenger extends MyAppUser {
    discountID?: number;
    discount?: Discount;
}
