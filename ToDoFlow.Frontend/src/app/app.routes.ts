import { Routes } from '@angular/router';
import { LayoutComponent } from './shared/layout/layout.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { TaskItemComponent } from './pages/task-item/task-item.component';
import { TaskItemEditComponent } from './pages/task-item/task-item-edit/task-item-edit.component';
import { CategoryComponent } from './pages/category/category.component';
import { TaskItemCreateComponent } from './pages/task-item/task-item-create/task-item-create.component';
import { authGuard } from './core/guards/auth/auth.guard';
import { guestGuard } from './core/guards/guest/guest.guard';

export const routes: Routes = [
  {
    path:'', component: LayoutComponent,
    children: [
      { path:'', redirectTo:'home', pathMatch:'full'},
      { path: 'home', canActivate:[authGuard], component: HomeComponent },
      { path: 'login', canActivate:[guestGuard], component: LoginComponent },
      { path: 'register', canActivate:[guestGuard], component: RegisterComponent },
      { path: 'taskitem', canActivate:[authGuard], component: TaskItemComponent},
      { path: 'taskitem/edit', canActivate:[authGuard], component: TaskItemEditComponent },
      { path: 'taskitem/create', canActivate:[authGuard], component: TaskItemCreateComponent },
      { path: 'category', canActivate:[authGuard], component: CategoryComponent},
    ]
  }
];
