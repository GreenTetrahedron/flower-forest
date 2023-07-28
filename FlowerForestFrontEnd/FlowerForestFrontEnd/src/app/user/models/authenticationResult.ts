import { User } from "./user";

export interface AuthenticationResult {
    authenticationSuccess: boolean;
    user: User;
}