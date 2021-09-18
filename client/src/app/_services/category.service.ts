import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UrlSerializer } from '@angular/router';
import { of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AddCategoryModel } from '../_models/AddCategoryModel';
import { Category } from '../_models/CategoryModel';
import { User } from '../_models/User';
import { UserParams } from '../_models/UserParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './PaginationHelper';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  baseUrl = environment.apiUrl;
  categories: Category[] = [];
  categoriesCache = new Map();
  user: User;
  userParams: UserParams;
  
  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user =>{
      this.user = user;
      this.userParams = new UserParams(user);
    })
   }

   getUserParams(){
    return this.userParams;
  }

  resetUserParams(){
    this.userParams = new UserParams(this.user);
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  getCategory(id: string){
    var NumberId = Number(id);
    const category =[...this.categoriesCache.values()]
    .reduce((arr, elem) => arr.concat(elem.result), [])
    .find((category: Category) => category.id === NumberId);
  
    if(category) {
      return of(category);
    }
    return this.http.get<Category>(this.baseUrl + 'category/' + id);
  }


  getCategories(userParams: UserParams)
  {
    var response = this.categoriesCache.get(Object.values(userParams).join('-'));
    if (response){
      return of(response);
    }
    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);
    params = params.append('orderBy', userParams.orderBy);
    return getPaginatedResult<Category[]>(this.baseUrl + 'category/pagination', params, this.http).pipe(map(response => {
    this.categoriesCache.set(Object.values(userParams).join('-'), response);

    return response;
    }))
    }

    addCategory(addCategoryModel: AddCategoryModel) {
      return this.http.post<Category>(this.baseUrl + 'category', addCategoryModel);
    }

    getCategoryList() {
      return this.http.get<Category[]>(this.baseUrl + 'category');
    }

    UpdateCategory(category: Category) {
      return this.http.put<Category>(this.baseUrl + 'category/categoryId', category).pipe(
        map( () => {
          const index = this.categories.indexOf(category);
          this.categories[index] = category;
        })
      )
    }

    deleteCategory(categoryId: number) {
      return this.http.delete(this.baseUrl + 'category/deleteCategoryId=' + categoryId).pipe(
        map( () => {

        })
      )
    }

}
