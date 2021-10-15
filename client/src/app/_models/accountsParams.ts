import { User } from "./User";

export class AccountParams {
    pageNumber = 1;
    pageSize = 10;
    orderBy = 'date';
    pagination = true;
    constructor(user: User){
    }
}