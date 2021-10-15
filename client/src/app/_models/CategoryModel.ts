export interface Category {
    id: number;
    appUserId : number;
    parentCategoryId: number | null;
    operationTypeId: number;
    name: string;
}