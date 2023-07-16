import { UserDTO } from "./userDTO";

export interface UserDTOWithToken {
    user: UserDTO;
    token: string;
}