import { MyAppUser } from "./my-app-user";

export interface Driver extends MyAppUser {
    license: string;
    driversLicenseNumber: string;
    hireDate: string;
    workingHoursInAWeek?: number;
}
