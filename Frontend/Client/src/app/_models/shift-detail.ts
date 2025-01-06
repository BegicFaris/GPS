import { Line } from "./line";
import { Shift } from "./shift";


export interface ShiftDetail {
    id: number;
    shiftId: number;
    shift:Shift;
    lineId: number;
    line:Line;
    shiftDetailStartingTime: string;
    shiftDetailEndingTime: string;
}
