import { Line } from "./line";
import { MyAppUser } from "./my-app-user";
import { NotificationType } from "./notification-type";

export interface Notification {
    id: number;
    title : string;
    description : string;
    image?: string;
    notificationTypeId: number;
    notificationType: NotificationType;
    creationDate: string;
    lineId: number;
    line: Line;
    managerId:number;
    manager:MyAppUser;
}