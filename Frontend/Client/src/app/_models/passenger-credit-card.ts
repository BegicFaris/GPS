import { CreditCard } from "./credit-card";
import { Passenger } from "./passenger";

export interface PassengerCreditCard {
    id: number;
    passengerId: number;
    passenger: Passenger;
    creditCardId: number;
    creditCard: CreditCard;
    savingDate: string;
}
