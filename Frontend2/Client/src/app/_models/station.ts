import { Zone } from "./zone"

export interface Station{
    id:number,
    zoneId: number,
    zone?: Zone,
    name: string,
    location: string
    gpsCode: boolean
}