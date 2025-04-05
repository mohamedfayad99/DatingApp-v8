export interface Paginatio{
    currentpage:number;
    itemsperpage:number;
    totalpages:number;
    totalitems:number;
}
export class PaginationResult<T>{
    items?:T;
    pagination?:Paginatio; 
}