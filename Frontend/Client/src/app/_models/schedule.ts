import { Line } from "./line";

export interface Schedule {
    id: number;
    lineId: number;
    line: Line;
    departureTime: string;
}
