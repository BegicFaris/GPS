import { Bus } from "./bus";
import { Driver } from "./driver";

export interface Shift {
    id: number;
    busId: number;
    bus: Bus;
    driverId: number;
    driver: Driver;
    shiftDate: string;
    shiftStartingTime: string;
    shiftEndingTime: string;
}
