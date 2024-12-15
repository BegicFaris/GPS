import { Line } from "./line";
import { NotificationType } from "./notification-type";

export interface Notification {
    description : string;
    id: number;
    notificationTypeId: number;
    notificationType: NotificationType;
    duration: string;
    date: string;
    isActive: boolean;
    lineId: number;
    line: Line;
}
