import { MyAppUser } from "./my-app-user";

export interface Feedback {
    id: number;
    userId: number;
    user: MyAppUser;
    comment?: string;
    rating: number;
    date: string;
    picture?: string;
}
