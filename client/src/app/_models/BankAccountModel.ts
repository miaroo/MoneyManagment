import { Operation } from "./OperationModel";

export interface BankAccount{
    Id: number;
    AppUserId: number;
    LastActive: Date;
    Name: string;
    Operations?: Operation[];
}