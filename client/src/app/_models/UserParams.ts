import { User } from "./User";

export class UserParams {
    pageNumber = 1;
    pageSize = 12;
    orderBy = 'created';
    constructor(user: User){}
}