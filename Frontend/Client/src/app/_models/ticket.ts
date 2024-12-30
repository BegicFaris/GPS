import { Line } from './line';
import { MyAppUser } from './my-app-user';
import { TicketInfo } from './ticket-info';
import { TicketType } from './ticket-type';
import { Zone } from './zone';

export interface Ticket {
  id: number;
  userId: number;
  ticketInfoId: number;
  ticketInfo: TicketInfo;
  createdDate: string;
  expirationDate: string;
  qrCode: string;
}
