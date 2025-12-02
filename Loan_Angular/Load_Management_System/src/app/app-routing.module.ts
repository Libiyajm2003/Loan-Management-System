import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { CustomerDashboardComponent } from './customer/customer-dashboard/customer-dashboard.component';
import { OfficerDashboardComponent } from './officer/officer-dashboard/officer-dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { RoleGuard } from './guards/role.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'admin',
    component: AdminDashboardComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Admin'] }
  },
  {
    path: 'customer',
    component: CustomerDashboardComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Customer'] }
  },
  {
    path: 'officer',
    component: OfficerDashboardComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Officer'] }
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}


