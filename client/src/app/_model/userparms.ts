import { User } from "./user";
export class UserParams{
    gender:string;
    orderby:string;
    pageNumber=1;
    pageSize=5;
    constructor(user:User|null){
        this.gender=user?.gender==='female'? 'male':'female',
        this.orderby=user?.OrderBy ==='created'?'created' : 'LastActive'
    }
}