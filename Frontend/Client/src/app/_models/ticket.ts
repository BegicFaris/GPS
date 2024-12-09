import { Line } from "./line";
import { MyAppUser } from "./my-app-user";
import { TicketType } from "./ticket-type";

export interface Ticket {
    id: number;
    userId: number;
    user: MyAppUser;
    lineId: number;
    line: Line;
    zoneId: number;
    zone: Zone;
    ticketTypeId: number;
    ticketType: TicketType;
    createdDate: string;
    expirationDate: string;
    qrCode: string;
}
