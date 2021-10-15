import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { CategoryCardComponent } from './category/category-card/category-card.component';
import { CategoryDetailComponent } from './category/category-detail/category-detail.component';
import { CategoryEditComponent } from './category/category-edit/category-edit.component';
import { CategoryListComponent } from './category/category-list/category-list.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { CategoriesComponent } from './categories/categories.component';
import { OperationsComponent } from './operations/operations.component';
import { AnalizeComponent } from './analize/analize.component';
import { AccountsComponent } from './accounts/accounts.component';
import { ToastrModule } from 'ngx-toastr';
import { SharedModule } from './_modules/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { RouterModule } from '@angular/router';
import { CategoryAddComponent } from './category/category-add/category-add.component';
import { SelectDropDownModule } from 'ngx-select-dropdown'
import { NgSelectModule } from '@ng-select/ng-select';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppMaterialModule } from "./app.material-module";
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { AccountsDetailsComponent } from './accounts-details/accounts-details.component';
import { AccountsAddComponent } from './accounts-add/accounts-add.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { OperationAddComponent } from './operation-add/operation-add.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    CategoryCardComponent,
    CategoryDetailComponent,
    CategoryEditComponent,
    CategoryListComponent,
    HomeComponent,
    RegisterComponent,
    CategoriesComponent,
    OperationsComponent,
    AnalizeComponent,
    AccountsComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    TextInputComponent,
    CategoryAddComponent,
    AccountsDetailsComponent,
    AccountsAddComponent,
    OperationAddComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    SelectDropDownModule,
    NgSelectModule,
    ModalModule.forRoot(),
    AppMaterialModule,
    MatTableModule,
    MatSortModule,
    NgxSpinnerModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true  },
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
  ],
  exports: [
    MatTableModule,
    MatSortModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { 
  MatTableModule
}
