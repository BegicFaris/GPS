export interface Line {
    id:number,
    name: string,
    startingStationId: number,
    startingStation: any,
    endingStationId: number,
    endingStation: any,
    completeDistance: string
    isActive: boolean
}