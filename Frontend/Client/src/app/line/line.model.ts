export interface Line{
    id: number;
    name: string;
    startingStationId: number;
    //startingStation: Station
    endingStationId: number;
    //endingStation: Station
    completeDistance: string;
    isActive: boolean;
}