import { Line } from './line';
import { MyAppUser } from './my-app-user';
import { TicketType } from './ticket-type';
import { Zone } from './zone';

export interface TicketInfo {
  id: number;
  price: number;
  zoneId: number;
  zone: Zone;
  ticketTypeId: number;
  ticketType: TicketType;
}
