import { Line } from "./line";
import { MyAppUser } from "./my-app-user";


export interface FavoriteLine {
    id:number,
    userId: number,
    user : MyAppUser,
    lineId: number,
    line: Line,

}