import { Line } from "./line";
import { MyAppUser } from "./my-app-user";
import { TicketType } from "./ticket-type";
import { Zone } from "./zone";

export interface Ticket {
    id: number;
  userId: number;
  lineId: number;
  zoneId: number;
  zone: Zone;
  ticketTypeId: number;
  createdDate: string;
  expirationDate: string;
  qrCode: string;
}
