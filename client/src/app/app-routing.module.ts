import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountsAddComponent } from './accounts-add/accounts-add.component';
import { AccountsDetailsComponent } from './accounts-details/accounts-details.component';
import { AccountsComponent } from './accounts/accounts.component';
import { AnalizeComponent } from './analize/analize.component';
import { CategoryAddComponent } from './category/category-add/category-add.component';
import { CategoryDetailComponent } from './category/category-detail/category-detail.component';
import { CategoryListComponent } from './category/category-list/category-list.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { OperationAddComponent } from './operation-add/operation-add.component';
import { OperationsComponent } from './operations/operations.component';
import { AuthGuard } from './_guards/auth.guard';


const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'categories', component: CategoryListComponent, canActivate: [AuthGuard]},
      {path: 'categories/:id', component: CategoryDetailComponent},
      {path: 'addCategory', component: CategoryAddComponent},
      {path: 'accounts', component: AccountsComponent},
      {path: 'operations', component: OperationsComponent},
      {path: 'addOperation', component: OperationAddComponent},
      {path: 'analize', component: AnalizeComponent},
      {path: 'accounts/:id', component: AccountsDetailsComponent},
      {path: 'addAccount', component: AccountsAddComponent},
      
      
    ]
  },
  {path: 'errors', component: TestErrorsComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: HomeComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
