import { Station } from "./station"

export interface Line {
    id:number,
    name: string,
    completeDistance: string
    isActive: boolean
}