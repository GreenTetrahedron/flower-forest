import { User } from "./user";

export interface UserWithToken {
    user: User;
    token: string;
}