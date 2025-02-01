import { MyAppUser } from "./my-app-user";

export interface Manager extends MyAppUser {
    hireDate: string;
    department: string;
    managerLevel: string;
}
