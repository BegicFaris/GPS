import { Line } from "./line";
import { Station } from "./station";

export interface Route {
    id: number;
    lineId: number;
    line: Line;
    stationId: number;
    station: Station;
    distanceFromTheNextStation: string;
    order: number;
}
